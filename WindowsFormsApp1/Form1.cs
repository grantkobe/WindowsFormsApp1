using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    //test by terry
    enum enSortType { enSortByMonth, enSortByDay };

    public partial class Form1 : Form
    {

       
        enSortType m_enSortType;
        private void ThreadFunction()
        {
            Thread.Sleep(1000);
            EXTClassify_FileByDate();

        }


        void _ChooseSortType(out enSortType etype)
        {
            if (true == radioButton_month.Checked)
            {
                etype = enSortType.enSortByMonth;
            } else
            {
                etype= enSortType.enSortByDay;
            }

        }

        public Form1()
        {
            InitializeComponent();
        }



        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*
            // Create an Image object. 
            Image image = new Bitmap(@"d:\FakePhoto.jpg");

            // Get the PropertyItems property from image.
            PropertyItem[] propItems = image.PropertyItems;

            // Set up the display.
            Font font = new Font("Arial", 12);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            int X = 0;
            int Y = 0;

            // For each PropertyItem in the array, display the ID, type, and 
            // length.
            int count = 0;
            string csContent = "" ;
            foreach (PropertyItem propItem in propItems)
            {
                switch(propItem.Type)
                {
                    case 2:
                        {
                            csContent = Convert.ToString( propItem.Value);
                            csContent = System.Text.Encoding.Default.GetString(propItem.Value);
                            break;
                        }
                }
                e.Graphics.DrawString(
                "Property Item " + count.ToString(),
                font,
                blackBrush,
                X, Y);

                Y += font.Height;
                  
                e.Graphics.DrawString(
                   "   iD: 0x" + propItem.Id.ToString("x"),
                   font,
                   blackBrush,
                   X, Y);

                Y += font.Height;

                e.Graphics.DrawString(
                   "   type: " + propItem.Type.ToString(),
                   font,
                   blackBrush,
                   X, Y);

                Y += font.Height;

                e.Graphics.DrawString(
                   "   length: " + propItem.Len.ToString() + " bytes" + "(" + csContent +")",
                   font,
                   blackBrush,
                   X, Y);

                Y += font.Height;

                count++;
            }
            // Convert the value of the second property to a string, and display 
            // it.
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string manufacturer = encoding.GetString(propItems[1].Value);

            e.Graphics.DrawString(
               "The equipment make is " + manufacturer + ".",
               font,
               blackBrush,
               X, Y);
               */
        }

        ThreadStart myRun; 
        Thread myThread;
        int mi_jpeg_fileNum = 0;
        int mi_mov_filenum = 0;
        int mi_png_filenum = 0;
        int mi_mp4_filenum = 0;
        int m_Thread_global_left_files = 0;



        private int FindFiles(out String[] FileCollection, String FilePath, string search_type)
        {
            int fileNum = 0;
            FileInfo theFileInfo;
            FileCollection = Directory.GetFiles(FilePath);

            for (int i = 0; i < FileCollection.Length; i++)
            {

                theFileInfo = new FileInfo(FileCollection[i]);
                if (theFileInfo.Extension.ToLower() == search_type.ToLower())
                {
                    fileNum++;
                }
                //Response.Write(theFileInfo.Name.ToString() + "<BR>");  123
            }
            return fileNum;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_curDir.Text = System.Environment.CurrentDirectory;
          
            List<string> dirs;
            String[] FileCollection;
            //
            {

                try
                {
                    //  dirs
                    {
                        string dirPath = textBox_curDir.Text;

                        dirs = new List<string>(Directory.EnumerateDirectories(dirPath));








                        foreach (var dir in dirs)
                        {

                            Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));
                        }

                        Console.WriteLine("{0} directories found.", dirs.Count);
                    }

                    //files
                    string search_type = ".jpg";
                    String FilePath = textBox_curDir.Text;
                    mi_jpeg_fileNum = 0;
                    mi_jpeg_fileNum = FindFiles(out FileCollection, FilePath, search_type);

                    mi_mov_filenum = 0;
                    mi_mov_filenum = FindFiles(out FileCollection, FilePath, ".mov");

                    mi_png_filenum = 0;
                    mi_png_filenum = FindFiles(out FileCollection, FilePath, ".png");

                    mi_mp4_filenum = 0;
                    mi_mp4_filenum = FindFiles(out FileCollection, FilePath, ".mp4");




                    textBox_ouput.Text = "當前目錄:" + dirs.Count.ToString() + " 檔案:" + FileCollection.Length.ToString() +"jpg :" 
                        + mi_jpeg_fileNum + "mov:" + mi_mov_filenum + "png:" + mi_png_filenum + "mp4:" + mi_mp4_filenum ;
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    Console.WriteLine(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    Console.WriteLine(PathEx.Message);
                }

            }



        }

        private int _MoveByModifiedTime(FileInfo theFileInfo, ref Dictionary<string, string> MonthMap, ref Dictionary<string, string> DayMap, ref int erleft_files)
        {


            var LastWriteTime = theFileInfo.LastWriteTimeUtc;
            string copyDir_fullPath;
            string Moth;

            string Year_fullPath = textBox_curDir.Text + "\\" + LastWriteTime.Year;
            if (!System.IO.Directory.Exists(Year_fullPath))
            {
                System.IO.Directory.CreateDirectory(Year_fullPath);
            }

            //rename Month to chinese 
            {


                MonthMap.TryGetValue(LastWriteTime.Month.ToString(), out Moth);

            }
            string Month_fullPath = Year_fullPath + "\\" + Moth;


            if (!System.IO.Directory.Exists(Month_fullPath))
            {
                System.IO.Directory.CreateDirectory(Month_fullPath);
            }


            if (enSortType.enSortByDay == m_enSortType)//add another sub dir
            {
                string strDay = ""+ LastWriteTime.Day;
                {
                    DayMap.TryGetValue(strDay, out strDay);
                }
                string Day_fullPath = Month_fullPath += "\\" + strDay;

                if (!System.IO.Directory.Exists(Day_fullPath))
                {
                    System.IO.Directory.CreateDirectory(Day_fullPath);
                }
                copyDir_fullPath = Day_fullPath + "\\" + theFileInfo.Name;
            }
            else
            {
                copyDir_fullPath = Month_fullPath + "\\" + theFileInfo.Name;
            }

            
            //move to the directory
            try
            {


                if (false == System.IO.File.Exists(copyDir_fullPath))
                {
                    System.IO.File.Move(theFileInfo.FullName, copyDir_fullPath);
                }
                else//identical file, we rename
                {
                    string DstFileName;
                     string[] file = theFileInfo.Name.Split('.');
                    //  DstFileName = file[0] + "_" + dirName[dirName.Length - 1] + "." + file[1];
                    int j = 0;
                    for (j = 0; j < 99; j++)
                    {
                        DstFileName = file[0] + "_" + j + "." + file[1];
                      
                        copyDir_fullPath = Month_fullPath + "\\" + DstFileName;
                        if (false == File.Exists(copyDir_fullPath))
                        {
                            System.IO.File.Move(theFileInfo.FullName, copyDir_fullPath);
                            break;
                        }
                    }

                }
            }
            catch (Exception j)
            {
                text_Error_file.Text += theFileInfo.Name + ":" + j.Message + "\n";
                int g1 = 0;
                g1++;
            }
            erleft_files--;
            textBox_ouput.Text = "剩下檔案:" + erleft_files; return erleft_files;
        }

     


        private void button1_Click(object sender, EventArgs e)
        {
#if true //thread version
            Form.CheckForIllegalCrossThreadCalls = false;
            // Enable timer.  
            m_Thread_global_left_files = mi_jpeg_fileNum + mi_mov_filenum + mi_png_filenum;
         //   Timer_UI.Enabled = true;
            myRun = new ThreadStart(ThreadFunction);
            myThread = new Thread(myRun);
            myThread.Start();
            bt_cancel.Enabled = true;
           // bt_pause.Enabled = true;


#else
            EXTClassify_FileByDate();
#endif
        }
        private void EXTClassify_FileByDate()
        {
            {
                _ChooseSortType(out m_enSortType);
                // List<string> dirs;
                String[] FileCollection;

                //
                {

                    try
                    {
                        var MonthMap = new Dictionary<string, string>();
                        var DayMap = new Dictionary<string, string>();

                        DayMap.Add("01", "01");
                        DayMap.Add("1", "01");
                        DayMap.Add("02", "02");
                        DayMap.Add("2", "02");
                        DayMap.Add("03", "03");
                        DayMap.Add("3", "03");
                        DayMap.Add("04", "04");
                        DayMap.Add("4", "04");
                        DayMap.Add("05", "05");
                        DayMap.Add("5", "05");
                        DayMap.Add("06", "06");
                        DayMap.Add("6", "06");
                        DayMap.Add("07", "07");
                        DayMap.Add("7", "07");
                        DayMap.Add("08", "08");
                        DayMap.Add("8", "08");
                        DayMap.Add("09", "09");
                        DayMap.Add("9", "09");
                        DayMap.Add("10", "10");
                        DayMap.Add("11", "11");
                        DayMap.Add("12", "12");
                        DayMap.Add("13", "13");
                        DayMap.Add("14", "14");
                        DayMap.Add("15", "15");
                        DayMap.Add("16", "16");
                        DayMap.Add("17", "17");
                        DayMap.Add("18", "18");
                        DayMap.Add("19", "19");
                        DayMap.Add("20", "20");
                        DayMap.Add("21", "21");
                        DayMap.Add("22", "22");
                        DayMap.Add("23", "23");
                        DayMap.Add("24", "24");
                        DayMap.Add("25", "25");
                        DayMap.Add("26", "26");
                        DayMap.Add("27", "27");
                        DayMap.Add("28", "28");
                        DayMap.Add("29", "29");
                        DayMap.Add("30", "30");
                        DayMap.Add("31", "31");

                        MonthMap.Add("01", "一月");
                        MonthMap.Add("1", "一月");
                        MonthMap.Add("02", "二月");
                        MonthMap.Add("2", "二月");
                        MonthMap.Add("03", "三月");
                        MonthMap.Add("3", "三月");
                        MonthMap.Add("04", "四月");
                        MonthMap.Add("4", "四月");
                        MonthMap.Add("05", "五月");
                        MonthMap.Add("5", "五月");
                        MonthMap.Add("06", "六月");
                        MonthMap.Add("6", "六月");
                        MonthMap.Add("07", "七月");
                        MonthMap.Add("7", "七月");
                        MonthMap.Add("08", "八月");
                        MonthMap.Add("8", "八月");
                        MonthMap.Add("09", "九月");
                        MonthMap.Add("9", "九月");
                        MonthMap.Add("10", "十月");
                        MonthMap.Add("11", "十一月");
                        MonthMap.Add("12", "十二月");

                        string search_type = ".jpg";
                        String FilePath = textBox_curDir.Text;
                        mi_jpeg_fileNum = FindFiles(out FileCollection, FilePath, search_type);
                        mi_mov_filenum = FindFiles(out FileCollection, FilePath, ".mov");
                        mi_png_filenum = FindFiles(out FileCollection, FilePath, ".png");

                        m_Thread_global_left_files = mi_jpeg_fileNum + mi_mov_filenum + mi_png_filenum;
                        //files
                        {
                            FilePath = textBox_curDir.Text;
                            FileInfo theFileInfo;
                            FileCollection = Directory.GetFiles(FilePath);


                            //handle mov && png, which choose by modified time
                            {
                                for (int i = 0; i < FileCollection.Length; i++)
                                {
                                    bool bProcessFile = false;

                                    theFileInfo = new FileInfo(FileCollection[i]);
                                    if (theFileInfo.Extension.ToLower() == ".mov".ToLower())
                                    {
                                        bProcessFile = true;
                                    }
                                    else if (theFileInfo.Extension.ToLower() == ".png".ToLower())
                                    {
                                        bProcessFile = true;
                                    }
                                    else if (theFileInfo.Extension.ToLower() == ".mp4".ToLower())
                                    {
                                        bProcessFile = true;
                                    }

                                    if (false == bProcessFile) continue;

                                    {
                                        _MoveByModifiedTime(theFileInfo, ref MonthMap, ref DayMap, ref m_Thread_global_left_files);

                                    }



                                }
                            }
                            //handle jpeg, choosed by picture time
                            for (int i = 0; i < FileCollection.Length; i++)
                            {
                                string Year = "";
                                string Day_hour = "";
                                string Moth = "";
                                string Day = "";

                                theFileInfo = new FileInfo(FileCollection[i]);
                                if (theFileInfo.Extension.ToLower() == ".jpg")
                                    try
                                    {

                                        // Create an Image object. 
                                        Image image = new Bitmap(theFileInfo.FullName);

                                        //                                System.IO.File.Move("C:\\Users\\terry\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\bin\\Debug\\FakePhoto.jpg", "C:\\Users\\terry\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\bin\\Debug\\2018\\二月\\FakePhoto.jpg");

                                        // Get the PropertyItems property from image.
                                        PropertyItem[] propItems = image.PropertyItems;



                                        // For each PropertyItem in the array, display the ID, type, and 
                                        // length.

                                        string csPhtoTime = "";
                                        bool bIfFoundPhotoTime = false;


                                        foreach (PropertyItem propItem in propItems)
                                        {
                                            switch (propItem.Id)
                                            {
                                                case 0x9003://or 9004
                                                    {
                                                        //csPhtoTime = Convert.ToString(propItem.Value);
                                                        csPhtoTime = System.Text.Encoding.Default.GetString(propItem.Value);
                                                        string[] tokens = csPhtoTime.Split(':');
                                                        Year = tokens[0];
                                                        Moth = tokens[1];

                                                        Day_hour = tokens[2];

                                                        tokens = Day_hour.Split(' ');
                                                        Day = tokens[0];
                                                        bIfFoundPhotoTime = true;
                                                        break;
                                                    }
                                            }
                                        }
                                        image.Dispose();
                                        propItems = null;

                                        //choose by modified time
                                        if (false == bIfFoundPhotoTime)
                                        {
                                            _MoveByModifiedTime(theFileInfo, ref MonthMap, ref DayMap, ref m_Thread_global_left_files);
                                            continue;
                                        }
                                        else
                                        {
                                            string copyDir_fullPath = "";
                                            string Year_fullPath = textBox_curDir.Text + "\\" + Year;
                                            if (!System.IO.Directory.Exists(Year_fullPath))
                                            {
                                                System.IO.Directory.CreateDirectory(Year_fullPath);
                                            }

                                            //rename Month to chinese 
                                            {


                                                MonthMap.TryGetValue(Moth, out Moth);

                                            }
                                            string Month_fullPath = Year_fullPath + "\\" + Moth;
                                            if (!System.IO.Directory.Exists(Month_fullPath))
                                            {
                                                System.IO.Directory.CreateDirectory(Month_fullPath);
                                            }

                                            if (enSortType.enSortByDay == m_enSortType)//add another sub dir
                                            {
                                                string Day_fullPath = Month_fullPath += "\\" + Day;
                                                {
                                                    DayMap.TryGetValue(Day, out Day);
                                                }
                                                if (!System.IO.Directory.Exists(Day_fullPath))
                                                {
                                                    System.IO.Directory.CreateDirectory(Day_fullPath);
                                                }
                                                copyDir_fullPath = Day_fullPath + "\\" + theFileInfo.Name;
                                            }
                                            else
                                            {
                                                copyDir_fullPath = Month_fullPath + "\\" + theFileInfo.Name;
                                            }


                                            string src_path = theFileInfo.FullName;
                                            string src_name = theFileInfo.Name;
                                            //move to the directory
                                            try
                                            {



                                                if (false == System.IO.File.Exists(copyDir_fullPath))
                                                {

                                                    // theFileInfo = null;
                                                    System.IO.File.Move(src_path, copyDir_fullPath);
                                                }
                                                else
                                                {
                                                    string DstFileName;
                                                    string[] file = theFileInfo.Name.Split('.');
                                                    //  DstFileName = file[0] + "_" + dirName[dirName.Length - 1] + "." + file[1];
                                                    int j = 0;
                                                    for (j = 0; j < 99; j++)
                                                    {
                                                        DstFileName = file[0] + "_" + j + "." + file[1];

                                                        copyDir_fullPath = Month_fullPath + "\\" + DstFileName;
                                                        if (false == File.Exists(copyDir_fullPath))
                                                        {
                                                            System.IO.File.Move(theFileInfo.FullName, copyDir_fullPath);
                                                            break;
                                                        }
                                                    }

                                                }
                                            }
                                            catch (Exception j)
                                            {
                                                text_Error_file.Text += src_name + ":" + j.Message + "\n";
                                                int g1 = 0;
                                                g1++;
                                            }
                                            m_Thread_global_left_files--;
                                            textBox_ouput.Text = "剩下檔案:" + m_Thread_global_left_files;

                                        }


                                        int g = 0;
                                        g++;

                                    }
                                    //Response.Write(theFileInfo.Name.ToString() + "<BR>");
                                    catch (ArgumentException e1)
                                    {
                                        int g = 0;

                                        text_Error_file.Text += theFileInfo.FullName + ":" + e1.Message + "\n";

                                        m_Thread_global_left_files--;
                                        textBox_ouput.Text = "剩下檔案:" + m_Thread_global_left_files;
                                    }
                            }
                        }


                    }
                    catch (UnauthorizedAccessException UAEx)
                    {
                        Console.WriteLine(UAEx.Message);
                    }
                    catch (PathTooLongException PathEx)
                    {
                        Console.WriteLine(PathEx.Message);
                    }

                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ThreadStart myRun = new ThreadStart(ThreadFunction);
            Thread myThread = new Thread(myRun);
            myThread.Start();
        }

        private void Timer_UI_Tick(object sender, EventArgs e)
        {
            // Set the caption to the current time.  
            string str = DateTime.Now.ToString();
            str += ": 剩下檔案-" + m_Thread_global_left_files;

            textBox_ouput.Text = str;
            if (0 == m_Thread_global_left_files)
            {
                Timer_UI.Enabled = false;
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            myThread.Abort();
            bt_cancel.Enabled = false;
        }
    }
}
