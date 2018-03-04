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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
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
        int ijpeg_fileNum = 0;
        int imov_filenum = 0;
        int ipng_filenum = 0;
      

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
                    int ijpeg_fileNum = 0;
                    ijpeg_fileNum = FindFiles(out FileCollection, FilePath, search_type);

                    int imov_filenum = 0;
                    imov_filenum = FindFiles(out FileCollection, FilePath, ".mov");

                    int ipng_filenum = 0;
                    ipng_filenum = FindFiles(out FileCollection, FilePath, ".png");




                    textBox_ouput.Text = "當前目錄:" + dirs.Count.ToString() + " 檔案:" + FileCollection.Length.ToString() +"jpg :" + ijpeg_fileNum + "mov:" + imov_filenum + "png:" + ipng_filenum;
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

        private int MoveByModifiedTime(FileInfo theFileInfo, ref Dictionary<string, string> MonthMap, ref int left_files)
        {


            var LastWriteTime = theFileInfo.LastAccessTimeUtc;
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
            copyDir_fullPath = Month_fullPath + "\\" + theFileInfo.Name;
            //move to the directory
            try
            {


                if (false == System.IO.File.Exists(copyDir_fullPath))
                {
                    System.IO.File.Copy(theFileInfo.FullName, copyDir_fullPath);
                }
            }
            catch (Exception j)
            {
                text_Error_file.Text += theFileInfo.Name + ":" + j.Message + "\n";
                int g1 = 0;
                g1++;
            }
            left_files--;
            textBox_ouput.Text = "剩下檔案:" + left_files; return left_files;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<string> dirs;
            String[] FileCollection;
            //
            {

                try
                {
                    var MonthMap = new Dictionary<string, string>();
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

                    int left_files = ijpeg_fileNum + imov_filenum + ipng_filenum;
                    //files
                    {
                        String FilePath = textBox_curDir.Text;
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
                                } else if (theFileInfo.Extension.ToLower() == ".png".ToLower())
                                {
                                    bProcessFile = true;
                                }

                                if (false == bProcessFile) continue;

                                {
                                     MoveByModifiedTime(theFileInfo, ref MonthMap, left_files);

                                }



                            }
                        }
                        //handle jpeg, choosed by picture time
                        for (int i = 0; i < FileCollection.Length; i++)
                        {
                            string Year = "";
                            string Day_hour = "" ;
                            string Moth = "";
                            string Day = "";

                            theFileInfo = new FileInfo(FileCollection[i]);
                            if(theFileInfo.Extension.ToLower() == ".jpg")
                            {
                              

                                // Create an Image object. 
                                Image image = new Bitmap(theFileInfo.FullName);

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

                                //choose by modified time
                                if (false ==  bIfFoundPhotoTime )
                                {
                                    return;
                                }
                                else {
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
                                    copyDir_fullPath = Month_fullPath + "\\"+ theFileInfo.Name;
                                    //move to the directory
                                    try
                                    {
                                     
                                        image = null;
                                        if (false == System.IO.File.Exists(copyDir_fullPath))
                                        {
                                            System.IO.File.Copy(theFileInfo.FullName, copyDir_fullPath);
                                        }
                                    }catch(Exception j)
                                    {
                                        text_Error_file.Text += theFileInfo.Name +":"+ j.Message + "\n";
                                        int g1 = 0;
                                        g1++;
                                    }
                                    left_files--;
                                    textBox_ouput.Text = "剩下檔案:" + left_files;

                                }

                                    
                                int g = 0;
                                g++;

                            }
                            //Response.Write(theFileInfo.Name.ToString() + "<BR>");
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
}
