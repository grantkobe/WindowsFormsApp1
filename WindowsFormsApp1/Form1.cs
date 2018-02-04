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

        int m_jpegFiles;
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
                    {
                        String FilePath = textBox_curDir.Text;
                        FileInfo theFileInfo;
                        FileCollection = Directory.GetFiles(FilePath);

                        for (int i = 0; i < FileCollection.Length; i++)
                        {

                            theFileInfo = new FileInfo(FileCollection[i]);
                            if(theFileInfo.Extension.ToLower() == ".jpg")
                            {
                                m_jpegFiles++;
                            }
                            //Response.Write(theFileInfo.Name.ToString() + "<BR>");  123
                        }
                    }

                    textBox_ouput.Text = "當前目錄:" + dirs.Count.ToString() + " 檔案:" + FileCollection.Length.ToString() +"jpg :" + m_jpegFiles;
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

        private void button1_Click(object sender, EventArgs e)
        {

            List<string> dirs;
            String[] FileCollection;
            //
            {

                try
                {
                    //find how many jpeg files 
                    {
                        m_jpegFiles = 0;
                        //files
                        {
                            String FilePath = textBox_curDir.Text;
                            FileInfo theFileInfo;
                            FileCollection = Directory.GetFiles(FilePath);

                            for (int i = 0; i < FileCollection.Length; i++)
                            {

                                theFileInfo = new FileInfo(FileCollection[i]);
                                if (theFileInfo.Extension.ToLower() == ".jpg")
                                {
                                    m_jpegFiles++;
                                }
                                //Response.Write(theFileInfo.Name.ToString() + "<BR>");
                            }
                        }

                    }

                    int left_files = m_jpegFiles;
                    //files
                    {
                        String FilePath = textBox_curDir.Text;
                        FileInfo theFileInfo;
                        FileCollection = Directory.GetFiles(FilePath);

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
                                int count = 0;
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
                                       var MonthMap = new Dictionary<string, string>();
                                        MonthMap.Add("01", "一月");
                                        MonthMap.Add("02", "二月");
                                        MonthMap.Add("03", "三月");
                                        MonthMap.Add("04", "四月");
                                        MonthMap.Add("05", "五月");
                                        MonthMap.Add("06", "六月");
                                        MonthMap.Add("07", "七月");
                                        MonthMap.Add("08", "八月");
                                        MonthMap.Add("09", "九月");
                                        MonthMap.Add("10", "十月");
                                        MonthMap.Add("11", "十一月");
                                        MonthMap.Add("12", "十二月");
                                        
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
