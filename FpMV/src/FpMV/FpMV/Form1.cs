/*******************************************************************************

License:
This software and/or related materials was developed at the National Institute
of Standards and Technology (NIST) by employees of the Federal Government
in the course of their official duties. Pursuant to title 17 Section 105
of the United States Code, this software is not subject to copyright
protection and is in the public domain.

This software and/or related materials have been determined to be not subject
to the EAR (see Part 734.3 of the EAR for exact details) because it is
a publicly available technology and software, and is freely distributed
to any interested party with no licensing requirements.  Therefore, it is
permissible to distribute this software as a free download from the internet.

Disclaimer:
This software and/or related materials was developed to promote biometric
standards and biometric technology testing for the Federal Government
in accordance with the USA PATRIOT Act and the Enhanced Border Security
and Visa Entry Reform Act. Specific hardware and software products identified
in this software were used in order to perform the software development.
In no case does such identification imply recommendation or endorsement
by the National Institute of Standards and Technology, nor does it imply that
the products and equipment identified are necessarily the best available
for the purpose.

This software and/or related materials are provided "AS-IS" without warranty
of any kind including NO WARRANTY OF PERFORMANCE, MERCHANTABILITY,
NO WARRANTY OF NON-INFRINGEMENT OF ANY 3RD PARTY INTELLECTUAL PROPERTY
or FITNESS FOR A PARTICULAR PURPOSE or for any purpose whatsoever, for the
licensed product, however used. In no event shall NIST be liable for any
damages and/or costs, including but not limited to incidental or consequential
damages of any kind, including economic damage or injury to property and lost
profits, regardless of whether NIST shall be advised, have reason to know,
or in fact shall know of the possibility.

By using this software, you agree to bear all risk relating to quality,
use and performance of the software and/or related materials.  You agree
to hold the Government harmless from any claim arising from your use
of the software.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Threading;


namespace FpMV
{
    public partial class Form1 : Form
    {
        string verion = "FpMV (Fingerprint Minutiae Viewer) - BETA V3.0";
        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        string FpMVPath;
        bool isFirstTimeStatus = true;
        //bool isMFirstTimeStatus = true;
        bool isImageStatus = false;
        bool veiwStatus = false;
        bool isRawWsqStatus = true;
        bool isEightBitStatus = true;
        bool workerRunningStatus = false;
        string chosenFile = "";
        string previousChosenFile = "";
        string imageFile = "";
        string orgImageFile = "";
        string fullFileName ="";
        string fileNamePrefix = "";
        string fileExt = "";
        string[] tempStr;
        string argv1 = "";
        string argv2 = "";
        string argv3 = "";
        string arguments = "";
        int width = 0;
        int height = 0;
        int previousWidth = 0;
        int previousHeight = 0;
        int totalMinutiae = 0;
        int displayMinutiae = 0;
        string curline = "";
        string lines = "";
        static int maxArrLenght = 10000;
        string[] temparrXY_IAFIS = new string[2];
        string[] arrXY_IAFIS = new string[maxArrLenght];
        int[] arrX_IAFIS = new int[maxArrLenght];
        int[] arrY_IAFIS = new int[maxArrLenght];
        double[] arrDIR_IAFIS = new double[maxArrLenght];
        double[] arrREL_IAFIS = new double[maxArrLenght];
        string[] arrTYP_IAFIS = new string[maxArrLenght];
        double relScalar = 100;
        float contrastMax = 4.0f;
        float contrastMin = -1.0f;
        float contrastValue = 1.0f;
        float brightnessValue = 0.0f;
        float brightnessMax = 1.0f;
        float brightnessMin = -1.0f;
        int count;
        int index = 0;

        int minWidth = 8;
        int minHeight = 8;
        int maxWidth = 4000;
        int maxHeight = 4000;

        int imgWidth;
        int imgHeight;

        ColorMatrix colormatrix;
        ImageAttributes imgAttribute;
        Pen opaquePenRed;
        Pen opaquePenBlue;
        Pen opaquePenGreen;
        Pen opaqueShadowPenRed;
        Pen opaqueShadowPenBlue;
        Pen opaqueShadowPenGreen;

        bool drag;
        float ScaleFactor = 1;
        float ScaleRange = 2.0F;
        System.Drawing.Point basePoint;
        Image image = null;
        int x, y;
        
        Form2 SetWidthHeightForm;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set Title
            this.Text = verion;

            // Enable application exit handler
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            // Enable drag and drop
            this.AllowDrop = true;

            // Set FormBorderStyle to not sizeable
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            // Add event handlers for PictureBox1 
            this.pictureBox1.Resize += new EventHandler(pictureBox1_Resize);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox1_Paint);

            // Add event handlers for the drag & drop functionality
            this.DragEnter += new System.Windows.Forms.DragEventHandler(Form1_DragEnter);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(Form1_DragDrop);

            // Add event handler for the mouse wheel functionality
            this.pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
            this.pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            this.pictureBox1.MouseHover += new EventHandler(pictureBox1_MouseHover);

            // Create directory using timestap as filename
            Stopwatch Stopwatch = Stopwatch.StartNew();
            String timeStamp = Stopwatch.GetTimestamp().ToString();
            FpMVPath = GetTempDir() + "FpMV\\" + timeStamp + "\\";


            // Create FpMVPath
            if (!Directory.Exists(FpMVPath))
            {
                System.IO.Directory.CreateDirectory(FpMVPath);
                if (!Directory.Exists(FpMVPath))
                {
                    MessageBox.Show("Can't create directory: " + FpMVPath, "Error Creating Directory!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isImageStatus == true)
            {
                if (e.Delta > 0)
                {
                    this.ZoomIn();
                }

                if (e.Delta < 0)
                {
                    this.ZoomOut();
                }
                base.OnMouseWheel(e);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isImageStatus == true)
            {
                if (e.Button == MouseButtons.Left)
                    drag = false;
                base.OnMouseUp(e);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isImageStatus == true)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.drag = true;
                        x = e.X;
                        y = e.Y;
                        break;
                    default:
                        break;
                }
                base.OnMouseDown(e);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                LimitBasePoint(basePoint.X + e.X - x, basePoint.Y + e.Y - y);
                x = e.X;
                y = e.Y;
                this.pictureBox1.Invalidate();
            }

            base.OnMouseMove(e);
        }

        private void ZoomExtents()
        {
            if (isImageStatus != false)
                this.ScaleFactor = (float)Math.Min((double)this.pictureBox1.Width / imgWidth, (double)this.pictureBox1.Height / imgHeight);
        }

        private void ZoomIn()
        {
            if (ScaleFactor < 10)
            {
                int x = (int)((this.pictureBox1.Width / ScaleRange - basePoint.X) / ScaleFactor);
                int y = (int)((this.pictureBox1.Height / ScaleRange - basePoint.Y) / ScaleFactor);
                ScaleFactor *= ScaleRange;
                LimitBasePoint((int)(this.pictureBox1.Width / ScaleRange - x * ScaleFactor), (int)(this.pictureBox1.Height / ScaleRange - y * ScaleFactor));
                this.pictureBox1.Invalidate();
            }
        }

        private void ZoomOut()
        {
            if (ScaleFactor > .1)
            {
                int x = (int)((this.pictureBox1.Width / ScaleRange - basePoint.X) / ScaleFactor);
                int y = (int)((this.pictureBox1.Height / ScaleRange - basePoint.Y) / ScaleFactor);
                ScaleFactor /= ScaleRange;
                LimitBasePoint((int)(this.pictureBox1.Width / ScaleRange - x * ScaleFactor), (int)(this.pictureBox1.Height / ScaleRange - y * ScaleFactor));
                this.pictureBox1.Invalidate();
            }
        }

        private void LimitBasePoint(int x, int y)
        {
            if (isImageStatus == false)
                return;

            int width = this.pictureBox1.Width - (int)(imgWidth * ScaleFactor);
            int height = this.pictureBox1.Height - (int)(imgHeight * ScaleFactor);

            if (width < 0)
            {
                x = Math.Max(Math.Min(x, 0), width);
            }
            else
            {
                x = (int)(width / 2);
            }
            if (height < 0)
            {
                y = Math.Max(Math.Min(y, 0), height);
            }
            else
            {
                y = (int)(height / 2);
            }
            basePoint = new System.Drawing.Point(x, y);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            int x = (int)((this.pictureBox1.Width / ScaleRange - basePoint.X) / ScaleFactor);
            int y = (int)((this.pictureBox1.Height / ScaleRange - basePoint.Y) / ScaleFactor);
            LimitBasePoint((int)(this.pictureBox1.Width / ScaleRange - x * ScaleFactor), (int)(this.pictureBox1.Height / ScaleRange - y * ScaleFactor));

            if (this.WindowState == FormWindowState.Maximized)
            {
                //MessageBox.Show("MAX ", "MAX!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.pictureBox1.Invalidate();
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                //MessageBox.Show("MIN", "MIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.pictureBox1.Invalidate();
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                //MessageBox.Show("Norm ", "Norm!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.pictureBox1.Invalidate();
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fileNamePrefix = "";
            //isMFirstTimeStatus = true;

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "fingerImages";
            openFileDialog1.Title = "Open a Image File (*.bmp, *.gif, *.jpg, *.png, *.raw, *.tif and *.wsq)";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "(*.bmp, *.gif, *.jpg, *.png, *.raw, *tif, *.wsq)|*.bmp;*.gif;*.jpg;*.png;*.raw;*.tif;*.wsq";
           
            // Save previous image filename
            if (isImageStatus == true)
            {
                previousChosenFile = chosenFile;
            }

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                // Set image file
                chosenFile = openFileDialog1.FileName;
                imageFile = chosenFile;

                // Run MINDTCT - IAFIS
                executeMindtctIAFIS();

                if (veiwStatus == true)
                {
                    populate_IAFIS(imageFile, true);
                    printImageMinutiaeInfo();
                    this.pictureBox1.Invalidate();
                }
                else
                {
                    if (isEightBitStatus == false && isRawWsqStatus == false)
                    {
                        isRawWsqStatus = true;
                        masterReset();
                        this.pictureBox1.Invalidate();
                    }
                    else if (isEightBitStatus == true && isRawWsqStatus == false)
                    {
                        if (isImageStatus == true)
                        {
                            populate_IAFIS(imageFile, true);
                            printImageMinutiaeInfo();
                            this.pictureBox1.Invalidate();
                        }
                    }
                    else if (isEightBitStatus == true && isRawWsqStatus == true)
                    {
                        populate_IAFIS(imageFile, true);
                        printImageMinutiaeInfo();
                        this.pictureBox1.Invalidate();
                    }
                    else if (isEightBitStatus == false && isRawWsqStatus == true)
                    {
                        masterReset();
                        this.pictureBox1.Invalidate();
                    }
                }
            }
            else
            {
                textBox1.Text = "No Image File Selected!";
            }
        }

        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        private void Form1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy; // Okay
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None; // Unknown data, ignore it
        }

        // Occurs when the user releases the mouse over the drop target 
        private void Form1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            int x = this.PointToClient(new System.Drawing.Point(e.X, e.Y)).X;
            int y = this.PointToClient(new System.Drawing.Point(e.X, e.Y)).Y;

            if (x >= pictureBox1.Location.X && x <= pictureBox1.Location.X + pictureBox1.Width &&
                y >= pictureBox1.Location.Y && y <= pictureBox1.Location.Y + pictureBox1.Height)
            {
                //isMFirstTimeStatus = true;

                // Extract the data from the DataObject-Container into a string list
                string[] FileList = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);

                string chosenFileTemp = FileList[0];
                string ext = Path.GetExtension(chosenFileTemp);

                if (checkImageType(ext))
                {
                    // Set input file 
                    if (isImageStatus == true)
                    {
                        previousChosenFile = chosenFile;
                        chosenFile = FileList[0];
                        imageFile = FileList[0];
                    }
                    else
                    {
                        chosenFile = FileList[0];
                        imageFile = FileList[0];
                    }

                    // Run MINDTCT - IAFIS
                    executeMindtctIAFIS();

                    if (veiwStatus == true)
                    {
                        populate_IAFIS(imageFile, true);
                        printImageMinutiaeInfo();
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        if (isEightBitStatus == false && isRawWsqStatus == false)
                        {
                            isRawWsqStatus = true;
                            masterReset();
                            this.pictureBox1.Invalidate();
                        }
                        else if (isEightBitStatus == true && isRawWsqStatus == false)
                        {
                            if (isImageStatus == true)
                            {
                                populate_IAFIS(imageFile, true);
                                printImageMinutiaeInfo();
                                this.pictureBox1.Invalidate();
                            }
                        }
                        else if (isEightBitStatus == true && isRawWsqStatus == true)
                        {
                            populate_IAFIS(imageFile, true);
                            printImageMinutiaeInfo();
                            this.pictureBox1.Invalidate();
                        }
                        else if (isEightBitStatus == false && isRawWsqStatus == true)
                        {
                            masterReset();
                            this.pictureBox1.Invalidate();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Image Type! \r\nThis application only support: *.bmp, *.gif, *.jpg, *.png, *.raw, *.tif and *.wsq", "Error Image Type!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void getFileNameAndExt()
        {
            fullFileName = Path.GetFileName(imageFile);
            tempStr = fullFileName.Split('.');
            fileNamePrefix = tempStr[0];
            fileExt = tempStr[1];
        }

        private void executeMindtctIAFIS()
        {
            // Initialize text box 
            nfiqScoreTextBox.Text = "";

            // Extract file information
            getFileNameAndExt();
            
            // Initialize filename
            string rawFileName = fileNamePrefix + ".raw";
            string pngFileName = fileNamePrefix + ".png";

            if (fileExt == "wsq")
            {
                // Covert WSQ to PNG
                convertWsq2Png(fileNamePrefix);

                if (veiwStatus == false)
                {
                    getFileNameAndExt();
                    if (fileExt == "wsq")
                    {
                        convertWsq2Png(fileNamePrefix);
                    }
                    else if (fileExt == "raw")
                    {
                        rawFileName = fileNamePrefix + ".raw";
                        pngFileName = fileNamePrefix + ".png";

                        // Copy source image file to FpMVPath
                        File.Copy(imageFile, FpMVPath + rawFileName, true);

                        // Covert RAW to PNG
                        convertRaw2Png(rawFileName, pngFileName, "raw");
                    }
                }
            }
            else if (fileExt == "raw")
            {
                // Get the Raw dimension from Form2
                getRawImageDimension(imageFile, fileNamePrefix);
                
                // Set the width and height 
                width = SetWidthHeightForm.width;
                height = SetWidthHeightForm.height;

                //dispose the Form2
                SetWidthHeightForm.Dispose();
                
                // Case takecare if form return W and H both 0
                if (width == 0 || height == 0)
                {
                    chosenFile = previousChosenFile;
                    imageFile = chosenFile;
                    getFileNameAndExt();
                    if (fileExt == "raw")
                    {
                        rawFileName = fileNamePrefix + ".raw";
                        pngFileName = fileNamePrefix + ".png";

                        width = previousWidth;
                        height = previousHeight;

                        // Copy source image file to FpMVPath
                        File.Copy(imageFile, FpMVPath + rawFileName, true);

                        // Covert RAW to PNG
                        convertRaw2Png(rawFileName, pngFileName, "raw");
                    }
                    veiwStatus = false;
                    isRawWsqStatus = false;
                }
                else
                {
                    // Copy source image file to FpMVPath
                    File.Copy(imageFile, FpMVPath + rawFileName, true);

                    // Covert RAW to PNG
                    convertRaw2Png(rawFileName, pngFileName, "raw");
                }

                previousWidth = width;
                previousHeight = height;
            }
            else
            {
                // Convert input image to PNG
                convertImage2Png();
            }

            if (veiwStatus == true)
            {
                callMindtct(imageFile, fileExt, true);
            }
        }

        // Extract Minutiae
        private void callMindtct(string fileIn, string fExt, bool resetTrackbar)
        {
            string str1 = Path.GetFileName(fileIn);
            string[] strArr1 = str1.Split('.');
            string filePrefix = strArr1[0];

            // Prep and call mindtct - IAFIS
            ProcessStartInfo startInfo_IAFIS = new ProcessStartInfo();
            startInfo_IAFIS.CreateNoWindow = true;
            startInfo_IAFIS.UseShellExecute = false;
            startInfo_IAFIS.FileName = appPath + "\\mindtct.exe";
            startInfo_IAFIS.WindowStyle = ProcessWindowStyle.Hidden;
            if (fExt == "jpg" || fExt == "png" || fExt == "wsq")
            {
                argv1 = "\"" + fileIn + "\"";
            }
            else if (fExt == "gif" || fExt == "tif" || fExt == "raw" || fExt == "bmp")
            {
                argv1 = "\"" + fileIn + "\"";
            }
            argv2 = "\"" + FpMVPath + filePrefix + "_iafis" + "\"";
            arguments = argv1 + " " + argv2;
            startInfo_IAFIS.Arguments = arguments;

            try
            {
                // Start the process with the info we specified.
                using (Process exeProcess = Process.Start(startInfo_IAFIS))
                {
                    exeProcess.WaitForExit();
                    veiwStatus = true;
                    if (resetTrackbar == true)
                    {
                        resetAllTrackBars();
                    }
                }
            }
            catch
            {
                veiwStatus = false;

                // Log error.
                MessageBox.Show("Failed to locate MINDTCT executable!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get the Raw Image Dimension
        private void getRawImageDimension(string chosenFile, string fileNamePrefix)
        {
            SetWidthHeightForm = new Form2(chosenFile, fileNamePrefix);
            SetWidthHeightForm.Activate();
            SetWidthHeightForm.ShowDialog();
        }

        // Convert input WSQ to PNG
        private void convertWsq2Png(string fileNamePrefix)
        {
            // Copy source image file to FpMVPath
            File.Copy(chosenFile, FpMVPath + fileNamePrefix + ".wsq", true);

            // Prep and call dwsq
            ProcessStartInfo startInfo_WSQ = new ProcessStartInfo();
            startInfo_WSQ.CreateNoWindow = true;
            startInfo_WSQ.UseShellExecute = false;
            startInfo_WSQ.FileName = appPath + "\\dwsq.exe";
            startInfo_WSQ.WindowStyle = ProcessWindowStyle.Hidden;
            argv1 = "raw";
            argv2 = "\"" + FpMVPath + fileNamePrefix + ".wsq" + "\"";
            argv3 = "-r";
            arguments = argv1 + " " + argv2 + " " + argv3;
            startInfo_WSQ.Arguments = arguments;
 
            string rawFileName = fileNamePrefix + ".raw";
            string ncmFileName = fileNamePrefix + ".ncm";
            string pngFileName = fileNamePrefix + ".png";
            
            veiwStatus = true;

            try
            {
                // Start the process with the info we specified.
                using (Process exeProcess = Process.Start(startInfo_WSQ))
                {
                    exeProcess.WaitForExit();
                    
                    if (!(File.Exists(FpMVPath + ncmFileName)))
                    {
                        veiwStatus = false;
                        isRawWsqStatus = false;
                        chosenFile = previousChosenFile;
                        imageFile = chosenFile;
                        getFileNameAndExt();
                        MessageBox.Show("Failed to decode WSQ file!\nPotentially bad or corrupted WSQ file!", "Error Decoding WSQ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        veiwStatus = true;
                        isRawWsqStatus = true;
                    }
                }
            }
            catch
            {
                veiwStatus = false;
                isRawWsqStatus = false;
                chosenFile = previousChosenFile;
                imageFile = chosenFile;
                getFileNameAndExt();
                // Log error.
                MessageBox.Show("Failed to locate the DWSQ executable!", "Error Executing DWSQ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Convert Raw to Png
            if (veiwStatus == true)
            {
                readWidthHeightFromNCM(ncmFileName);
                convertRaw2Png(rawFileName, pngFileName, "wsq");
            }
        }

        // Read Width and Height from ncm file
        private void readWidthHeightFromNCM(string ncmFileName)
        {
            // Read the file and display it line by line.
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(FpMVPath + ncmFileName);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("PIX_WIDTH"))
                {
                    string[] tempStr = line.Split(' ');
                    width = Convert.ToInt32(tempStr[1]);
                }
                if (line.Contains("PIX_HEIGHT"))
                {
                    string[] tempStr = line.Split(' ');
                    height = Convert.ToInt32(tempStr[1]);
                }
            }
            file.Close();
        }

        // Convert from RAW to PNG
        private void convertRaw2Png(string rawFileName, string pngFileName, string format)
        {
            int stride = width;
            byte[] byteArray = new byte[height * stride];

            try
            {
                using (FileStream fsSource = new FileStream(FpMVPath + rawFileName,
                    FileMode.Open, FileAccess.Read))
                {
                    // Read the source file into a byte array. 
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead. 
                        int n = fsSource.Read(byteArray, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached. 
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesToRead = byteArray.Length;
                    fsSource.Close();
                }
                // Define the image palette
                BitmapPalette myPalette = BitmapPalettes.Gray256;

                // Creates a new empty image with the pre-defined palette
                BitmapSource image = BitmapSource.Create(
                    width,
                    height,
                    500,
                    500,
                    System.Windows.Media.PixelFormats.Indexed8,
                    myPalette,
                    byteArray,
                    stride);

                FileStream stream = new FileStream(FpMVPath + pngFileName, FileMode.Create);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Interlace = PngInterlaceOption.On;
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                stream.Dispose();
                
                imageFile = FpMVPath + pngFileName;
                fileExt = format;
                convertImage2Png();
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
                MessageBox.Show("Failed to read file", "Error Read!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        // Convert input image to PNG 
        private void convertImage2Png()
        {
            try
            {
                Image image2 = System.Drawing.Image.FromFile(imageFile);

                if (image2.PixelFormat.ToString() == "Format8bppIndexed" ||
                    image2.PixelFormat.ToString() == "Format32bppArgb")
                {
                    string fname = "";
                    isEightBitStatus = true;

                    if (Path.GetExtension(imageFile) == ".png")
                    {
                        if (!imageFile.Contains(FpMVPath))
                        {
                            fname = FpMVPath + fileNamePrefix + ".png";
                            File.Copy(imageFile, fname, true);
                        }
                    }
                    else
                    {
                        fname = FpMVPath + fileNamePrefix + "." + fileExt;
                        File.Copy(imageFile, fname, true);
                        fname = FpMVPath + fileNamePrefix + ".png";
                        image2.Save(fname, ImageFormat.Png);
                        imageFile = fname;
                    }

                    imgWidth = image2.Width;
                    imgHeight = image2.Height;

                    if (imgWidth >= 8 && imgHeight >= 8 && imgWidth <= 4000 && imgHeight <= 4000)
                    {
                        isImageStatus = true;
                        veiwStatus = true;
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                    }

                    if (imgWidth < minWidth || imgHeight < minHeight)
                    {
                        MessageBox.Show("Image Width or Height can't be less than 8 pixels!\nNo minuitae will be detected!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isImageStatus = false;
                        veiwStatus = false;
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                    }

                    if (imgWidth > maxWidth || imgHeight > maxHeight)
                    {
                        MessageBox.Show("Image Width or Height can't be larger than 4000 pixels!\nNo minuitae will be detected!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isImageStatus = false;
                        veiwStatus = false;
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                    }
                }
                else
                {
                    isEightBitStatus = false;
                    imageFile = chosenFile;
                    isImageStatus = true;
                    veiwStatus = false;
                    this.FormBorderStyle = FormBorderStyle.Sizable;

                    imgWidth = image2.Width;
                    imgHeight = image2.Height;
                    totalMinutiae = 0;
                    resetAllTrackBars();
                    masterReset();
                    printImageAndToolInfo();

                    this.ZoomExtents();
                    this.LimitBasePoint(basePoint.X, basePoint.Y);
                    this.pictureBox1.Invalidate();

                    MessageBox.Show("Image is not 8-bit grey scale!", "Image Depth Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                image2.Dispose();
            }
            catch
            {
                veiwStatus = false;
                isRawWsqStatus = false;
                chosenFile = previousChosenFile;
                imageFile = chosenFile;
                MessageBox.Show("Failed to decode file!\nPotential bad file format!", "Decoding Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Populate the minutiea from min file
        private void populate_IAFIS(string fileIn, bool resetZoom)
        {
            // Extract file information
            string str1 = Path.GetFileName(fileIn);
            string[] strArr1 = str1.Split('.');
            string filePrefix = strArr1[0];

            // Set the min file name
            string path = FpMVPath + filePrefix + "_iafis" + ".min";
            StreamReader file = new StreamReader(path);

            // Initializing
            resetAllArrays();

            // Retieve image infomation
            curline = file.ReadLine();
            tempStr = curline.Split(' ');
            width = Convert.ToInt32(tempStr[2]);
            height = Convert.ToInt32(tempStr[3]);
            file.ReadLine();
            curline = file.ReadLine();
            tempStr = curline.Split(' ');
            totalMinutiae = Convert.ToInt32(tempStr[0]);
            file.ReadLine();

            // Print image infomation
            printImageAndToolInfo();

            // Get all minutiae to arrays
            while (((curline = file.ReadLine()) != null) && (count < maxArrLenght))
            {
                //lines = lines + curline + "\r\n";
                Array.Clear(tempStr, 0, tempStr.Length);
                tempStr = curline.Split(':');
                arrXY_IAFIS[count] = tempStr[1].Trim();
                temparrXY_IAFIS = arrXY_IAFIS[count].Split(',');
                arrX_IAFIS[count] = Convert.ToInt32(temparrXY_IAFIS[0]);
                arrY_IAFIS[count] = Convert.ToInt32(temparrXY_IAFIS[1]);
                arrDIR_IAFIS[count] = Convert.ToDouble(tempStr[2]);
                arrREL_IAFIS[count] = Convert.ToDouble(tempStr[3]);
                arrTYP_IAFIS[count] = tempStr[4].Trim();
                count++;
            }
            file.Close();

            if (resetZoom == true)
            {
                this.ZoomExtents();
                this.LimitBasePoint(basePoint.X, basePoint.Y);
            }
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        // Print the Minintiae information
        private void printImageMinutiaeInfo()
        {
            // Print XYTQ in Textbox
            int ilen;
            lines = "";

            for (int i = 0; i < count; i++)
            {
                string str0 = i.ToString();
                ilen = str0.Length;
                lines = lines + str0 + addSpaces(ilen, 5);

                string str1 = "-> (" + arrX_IAFIS[i] + "," + arrY_IAFIS[i] + ")";
                ilen = str1.Length;
                lines = lines + str1 + addSpaces(ilen, 16);

                string str2 = "Direction: " + arrDIR_IAFIS[i];
                ilen = str2.Length;
                lines = lines + str2 + addSpaces(ilen, 16);

                string str3 = "Quality: " + arrREL_IAFIS[i];
                ilen = str3.Length;
                lines = lines + str3 + addSpaces(ilen, 17);

                string str4 = "Type: " + arrTYP_IAFIS[i];
                lines = lines + str4 + "\r\n";

                //lines = lines + i + " -> (" + arrX_IAFIS[i] + "," + arrY_IAFIS[i] + ")  Direction: " +
                //    arrDIR_IAFIS[i] + "  Quality: " + arrREL_IAFIS[i] + "  Type: " + arrTYP_IAFIS[i] + "\r\n";
            }

            // Print all minutiae information to textbox
            textBox6.Text = fileNamePrefix + "." + fileExt;
            textBox1.Text = lines;
        }

        private string addSpaces(int ilen, int dlen)
        {
            string c_spaces = "";
            int index = dlen - ilen;

            for (int i = 0; i < index; i++)
            {
                c_spaces = c_spaces  + " ";
            }
                
            return c_spaces;
        }

        private void calDisplayMinutiae()
        {
            displayMinutiae = 0;

            for (int i = 0; i < count; i++)
            {
                if ((arrREL_IAFIS[i] * relScalar) > trackBar1.Value)
                {
                    displayMinutiae++;
                }
            }
        }

        // Paint the image and overlay the minutiae points
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            double x1;
            double y1;
            double x2;
            double y2;
            double angle = 0;
            int radius = 4;
            int pixels = 14;
            int offset = 1;
            displayMinutiae = 0;
            
            Graphics g = e.Graphics;

            if (isFirstTimeStatus == false && isImageStatus == true)
            {
                image = Image.FromFile(imageFile);

                // Display image as pixel (no smoothing apply)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Set opaque image
                colormatrix = new ColorMatrix();
                colormatrix.Matrix33 = (float)fpOpacityTrackBar.Value / 100;
                
                // Set image contrast
                calContrastValue();
                colormatrix.Matrix00 = contrastValue;
                colormatrix.Matrix11 = contrastValue;
                colormatrix.Matrix22 = contrastValue;

                // Set image brightness5
                calBrightnesstValue();
                colormatrix.Matrix40 = brightnessValue;
                colormatrix.Matrix41 = brightnessValue;
                colormatrix.Matrix42 = brightnessValue;

                // Set image attribute
                imgAttribute = new ImageAttributes();
                imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Create opaque Pen
                int opaquePenTranValue = (int)(MinOpacityTrackBar.Value * (255 / 100));
                opaquePenRed = new Pen(Color.FromArgb(opaquePenTranValue, 255, 0, 0), 2);
                opaquePenGreen = new Pen(Color.FromArgb(opaquePenTranValue, 0, 255, 0), 2);
                opaquePenBlue = new Pen(Color.FromArgb(opaquePenTranValue, 0, 0, 255), 2);
                opaqueShadowPenRed = new Pen(Color.FromArgb(opaquePenTranValue/2, 100, 0, 0), 2);
                opaqueShadowPenGreen = new Pen(Color.FromArgb(opaquePenTranValue / 2, 0, 100, 0), 2);
                opaqueShadowPenBlue = new Pen(Color.FromArgb(opaquePenTranValue / 2, 0, 0, 100), 2);

                // Draw Image 
                Rectangle dst = new Rectangle(basePoint.X, basePoint.Y, (int)(imgWidth * ScaleFactor), (int)(imgHeight * ScaleFactor));
                g.DrawImage(image, dst, 0, 0, imgWidth, imgHeight, GraphicsUnit.Pixel, imgAttribute);
           
                // TransForm and scale minutieas on image 
                g.TranslateTransform(basePoint.X, basePoint.Y);
                g.ScaleTransform(ScaleFactor, ScaleFactor);

                if (veiwStatus == true || isImageStatus == true)
                {
                    for (int i = 0; i < count; i++)
                    {
                        // Center of minutiea 
                        x1 = arrX_IAFIS[i];
                        y1 = arrY_IAFIS[i];

                        // IAFIS or M1
                        if (radioButton_IAFIS.Checked)
                        {
                            angle = arrDIR_IAFIS[i] * 11.25 - 90;
                        }
                        else if (radioButton_M1.Checked)
                        {
                            angle = (arrDIR_IAFIS[i] * 11.25 - 270);
                        }

                        // Calculate line and direction
                        x2 = Math.Round(x1 + (Math.Cos(angle / 180 * Math.PI) * pixels), 0, MidpointRounding.ToEven);
                        y2 = Math.Round(y1 + (Math.Sin(angle / 180 * Math.PI) * pixels), 0, MidpointRounding.ToEven);

                        if (arrTYP_IAFIS[i].Equals("RIG"))
                        {
                            if ((arrREL_IAFIS[i] * relScalar) > trackBar1.Value)
                            {
                                g.DrawEllipse(opaquePenRed, new Rectangle((int)(x1 - radius), (int)(y1 - radius), (int)(radius * 2), (int)(radius * 2)));
                                g.DrawLine(opaquePenRed, new System.Drawing.Point((int)x1, (int)y1), new System.Drawing.Point((int)x2, (int)y2));
                                if (opaquePenTranValue < 200)
                                {
                                    g.DrawEllipse(opaqueShadowPenRed, new Rectangle((int)(x1 - radius + offset), (int)(y1 - radius + 1), (int)(radius * 2), (int)(radius * 2)));
                                    g.DrawLine(opaqueShadowPenRed, new System.Drawing.Point((int)x1 + offset, (int)y1 + offset), new System.Drawing.Point((int)x2 + offset, (int)y2 + offset));
                                }
                                displayMinutiae++;
                            }
                        }
                        else if (arrTYP_IAFIS[i].Equals("BIF"))
                        {
                            if ((arrREL_IAFIS[i] * relScalar) > trackBar1.Value)
                            {
                                g.DrawRectangle(opaquePenGreen, new Rectangle((int)(x1 - radius), (int)(y1 - radius), (int)(radius * 2), (int)(radius * 2)));
                                g.DrawLine(opaquePenGreen, new System.Drawing.Point((int)x1, (int)y1), new System.Drawing.Point((int)x2, (int)y2));
                                if (opaquePenTranValue < 200)
                                {
                                    g.DrawRectangle(opaqueShadowPenGreen, new Rectangle((int)(x1 - radius + offset), (int)(y1 - radius + offset), (int)(radius * 2), (int)(radius * 2)));
                                    g.DrawLine(opaqueShadowPenGreen, new System.Drawing.Point((int)x1 + offset, (int)y1 + offset), new System.Drawing.Point((int)x2 + offset, (int)y2 + offset));
                                }
                                displayMinutiae++;
                            }
                        }
                        else
                        {
                            if ((arrREL_IAFIS[i] * relScalar) > trackBar1.Value)
                            {
                                g.DrawEllipse(opaquePenBlue, new Rectangle((int)(x1 - radius), (int)(y1 - radius), (int)(radius * 2), (int)(radius * 2)));
                                g.DrawLine(opaquePenBlue, new System.Drawing.Point((int)x1, (int)y1), new System.Drawing.Point((int)x2, (int)y2));
                                if (opaquePenTranValue < 200)
                                {
                                    g.DrawEllipse(opaqueShadowPenBlue, new Rectangle((int)(x1 - radius + offset), (int)(y1 - radius + offset), (int)(radius * 2), (int)(radius * 2)));
                                    g.DrawLine(opaqueShadowPenBlue, new System.Drawing.Point((int)x1 + offset, (int)y1 + offset), new System.Drawing.Point((int)x2 + offset, (int)y2 + offset));
                                }
                                displayMinutiae++;
                            }
                        }
                    }
                }

                displayedMinTextBox.Text = Convert.ToString(displayMinutiae);

                imgObjDispose();
            }
            else
            {
                isFirstTimeStatus = false;
            }
        }

        // Calculate NFIQ Score Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (!(File.Exists(appPath + "\\nfiq.exe")))
            {
                MessageBox.Show("Failed to locate NFIQ executable!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (veiwStatus == true)
                {
                    bool status = false;
                    string outFile = FpMVPath + "xcnfiqtemp_" + index + ".png";

                    // Call function to create grayscale image
                    CreateGraysacleToFile(outFile);

                    // Create Compiler Object
                    Process compiler = new Process();
                    compiler.StartInfo.FileName = appPath + "\\nfiq.exe";

                    status = true;
                    compiler.StartInfo.Arguments = outFile;

                    if (status == true)
                    {
                        compiler.StartInfo.CreateNoWindow = true;
                        compiler.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        compiler.StartInfo.UseShellExecute = false;
                        compiler.StartInfo.RedirectStandardOutput = true;
                        compiler.Start();

                        nfiqScoreTextBox.Text = compiler.StandardOutput.ReadToEnd();
                        Console.WriteLine(compiler.StandardOutput.ReadToEnd());

                        compiler.WaitForExit();
                    }
                    else
                    {
                        nfiqScoreTextBox.Text = "N/A";
                        MessageBox.Show("Invalid image format!\r\nNFIQ only support *jpg, *.png, *raw and *wsq image formats!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    nfiqScoreTextBox.Text = "N/A";
                    MessageBox.Show("Invalid image format!\r\nNFIQ only support *jpg, *png, *raw and *wsq image formats!", "Error Executing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static String GetTempDir()
        {
            return System.IO.Path.GetTempPath();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.Filter = "(*.bmp)|*.bmp|(*.gif)|*.gif|(*.jpg)|*.jpg|(*.png)|*.png|(*.tif)|*.tif|(*.wsq)|*.wsq";
                saveFileDialog1.FilterIndex = 6;
                saveFileDialog1.CheckPathExists = true;
                ImageFormat format = ImageFormat.Png;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                    switch (ext)
                    {
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        case ".tif":
                            format = ImageFormat.Tiff;
                            break;
                        case ".wsq":
                            //format = ImageFormat.Wsq;
                            break;
                    }
                    
                    pictureBox1.Image.Save(saveFileDialog1.FileName, format);
                }
            }
            else
            {
                MessageBox.Show("No image is avaliable for save! ", "Error Saving Image!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reset Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                //if (isMFirstTimeStatus == false)
                //{
                //    imageFile = orgImageFile;
                //    isMFirstTimeStatus = true;
                //}

                int origTotalMin = totalMinutiae;

                if (workerRunningStatus == false)
                {
                    // reset everything
                    contrastValue = 1.0f;
                    brightnessValue = 0.0f;
                    resetAllArrays();

                    // recalculate Minutiae 
                    callMindtct(imageFile, fileExt, true);
                    populate_IAFIS(imageFile, true);
                    printImageAndToolInfo();
                    printImageMinutiaeInfo();

                    // display
                    this.ZoomExtents();
                    this.LimitBasePoint(basePoint.X, basePoint.Y);
                    this.pictureBox1.Invalidate();

                    // Flash Minutiea related textbox
                    if (totalMinutiae != origTotalMin)
                    {

                        //flashMinTextBox();
                        workerRunningStatus = true;
                        backgroundWorker1.RunWorkerAsync();
                    }
                }
            }
        }
       
        // Min. Detect Button
        private void button3_Click(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                // Initialize
                index++;

                //if (isMFirstTimeStatus == true)
                //{
                //    orgImageFile = imageFile;
                //    isMFirstTimeStatus = false;
                //}

                int origTotalMin = totalMinutiae;
                string outFile = FpMVPath + "xctemp_" + index + ".png";
                
                // Call function to create grayscale image
                CreateGraysacleToFile(outFile);

                // set image file
                //imageFile = outFile;

                if (workerRunningStatus == false)
                {
                    // Reset
                    resetAllArrays();

                    // process Mindect and IAFIS
                    callMindtct(outFile, "png", false);
                    populate_IAFIS(outFile, false);
                    this.pictureBox1.Invalidate();
                    printImageAndToolInfo();
                    printImageMinutiaeInfo();

                    // Flash Minutiea related textbox
                    if (totalMinutiae != origTotalMin)
                    {
                        //flashMinTextBox();
                        workerRunningStatus = true;
                        backgroundWorker1.RunWorkerAsync();
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int msec = 500;
            int flashCount = 3;
            int i = 0;

            for (i = 0; i < flashCount; i++)
            {
                totalMinTextBox.ForeColor = Color.Red;
                displayedMinTextBox.ForeColor = Color.Red;
                Thread.Sleep(msec);

                totalMinTextBox.ForeColor = Color.Black;
                displayedMinTextBox.ForeColor = Color.Black;
                Thread.Sleep(msec);
            }
            workerRunningStatus = false;
        }

        void flashMinTextBox()
        {
            int msec = 500;
            int flashCount = 3;
            int i = 0;

            resetButton.Enabled = false;
            minDetectButton.Enabled = false;
            nfiqButton.Enabled = false;

            for (i = 0; i < flashCount; i++)
            {
                totalMinTextBox.ForeColor = Color.Red;
                displayedMinTextBox.ForeColor = Color.Red;
                totalMinTextBox.Refresh();
                displayedMinTextBox.Refresh();
                Thread.Sleep(msec);

                totalMinTextBox.ForeColor = Color.Black;
                displayedMinTextBox.ForeColor = Color.Black;
                totalMinTextBox.Refresh();
                displayedMinTextBox.Refresh();
                Thread.Sleep(msec);
            }

            resetButton.Enabled = true;
            minDetectButton.Enabled = true;
            nfiqButton.Enabled = true;
        }

          
        public void CreateGraysacleToFile(string outFile)
        {
            ColorMatrix cMatrix;
            ImageAttributes imgAttr;

            Image image1 = Image.FromFile(imageFile);
            Image image2 = new Bitmap(image1);
            Graphics a = Graphics.FromImage(image2);

            // Display image as pixel (no smoothing apply)
            a.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            a.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Set image contrast and brightness
            cMatrix = new ColorMatrix();
            calContrastValue();
            cMatrix.Matrix00 = contrastValue;
            cMatrix.Matrix11 = contrastValue;
            cMatrix.Matrix22 = contrastValue;
            calBrightnesstValue();
            cMatrix.Matrix40 = brightnessValue;
            cMatrix.Matrix41 = brightnessValue;
            cMatrix.Matrix42 = brightnessValue;

            // Set image attribute
            imgAttr = new ImageAttributes();
            imgAttr.SetColorMatrix(cMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            // Draw Image 
            Rectangle dst = new Rectangle(0, 0, image2.Width, image2.Height);
            a.DrawImage(image2, dst, 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imgAttr);

            // Convert to gray scale
            Bitmap bmp = new Bitmap(image2);
            Bitmap gray;
            gray = ColorToGrayscale(bmp);
            gray.Save(outFile, ImageFormat.Png);

            // Clean up
            image1.Dispose();
            image2.Dispose();
            bmp.Dispose();
            gray.Dispose();
            a.Dispose();
        }

        /// <summary>
        /// http://social.msdn.microsoft.com/Forums/vstudio/en-US/1a856813-a746-4c1a-ac15-a4314f6eb349/quick-image-conversion-to-8bpp
        /// Converts a bitmap into an 8-bit grayscale bitmap
        /// </summary>
        public Bitmap ColorToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width,
                h = bmp.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            PixelFormat pfIn = bmp.PixelFormat;
            ColorPalette palette;
            Bitmap output;
            BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new Bitmap(w, h, PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                Color tmp = Color.FromArgb(255, i, i, i);
                palette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == PixelFormat.Format8bppIndexed)
            {
                output = (Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel
            switch (pfIn)
            {
                case PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly,
                                   pfIn);
            outputData = output.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly,
                                         PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)(bmpPtr[r * bmpStride + ic + 1]);
                }
                else //bytesPerPixel == 4
                {
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)(bmpPtr[r * bmpStride + ic + 1]); 

                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }

        private void imgObjDispose()
        {
            if (image != null)
            {
                image.Dispose();
            }
        }

        private void cleanUpFiles()
        {
            // cleanup all the files and objects
            if (isFirstTimeStatus == false)
            {
                imgObjDispose();
                if (Directory.Exists(FpMVPath))
                {
                    try
                    {
                        Directory.Delete(FpMVPath, true);
                    }
                    catch (Exception e)
                    {
                        string errStr = "Failed to delete directory: " + FpMVPath;
                        Console.WriteLine(errStr, e.Message);
                    }
                }
            }
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // cleanup and exit 
            cleanUpFiles();
            Application.Exit();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the 
            // user file and close it.
            cleanUpFiles();
        }

        private bool checkImageType(string ext)
        {
            bool status;

            if (ext == ".bmp" || ext == ".gif" || ext == ".jpg" ||
                ext == ".png" || ext == ".tif" || ext == ".wsq" ||
                ext == ".raw")
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                double trackbarValue = (double)trackBar1.Value / relScalar;
                qualityTextBox.Text = trackbarValue.ToString();
                this.pictureBox1.Invalidate();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int orgValueI = trackBar1.Value;
            double newValueD = 0;

            try
            {
                newValueD = Convert.ToDouble(qualityTextBox.Text);
                if (isImageStatus == true && isFirstTimeStatus == false)
                {
                    if (newValueD >= 0 && newValueD <= 0.99)
                    {
                        int newValueI = (int)(newValueD * relScalar);
                        trackBar1.Value = newValueI;
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        trackBar1.Value = orgValueI;
                        MessageBox.Show("Invalid Range(1)! \r\nPlease input a value in the range of (0 - 0.99).", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                trackBar1.Value = orgValueI;
                //MessageBox.Show("Invalid Range(2)! \r\nPlease input range (0 - 0.99).", "Error Invalid Range!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                fpOpacityTextBox.Text = fpOpacityTrackBar.Value.ToString();
                this.pictureBox1.Invalidate();
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            int orgValueI = fpOpacityTrackBar.Value;
            int newValueI = 0;

            try
            {
                newValueI = Convert.ToInt32(fpOpacityTextBox.Text);
                if (isImageStatus == true && isFirstTimeStatus == false)
                {
                    if (newValueI >= 0 && newValueI <= 100)
                    {
                        fpOpacityTrackBar.Value = newValueI;
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        fpOpacityTrackBar.Value = orgValueI;
                        MessageBox.Show("Invalid Range(2)! \r\nlease input a value in the range of (0 - 100).", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                fpOpacityTrackBar.Value = orgValueI;
                //MessageBox.Show("Invalid Range(2)! \r\nPlease input range (0 - 100).", "Error Invalid Range!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                MinOpacityTextBox.Text = MinOpacityTrackBar.Value.ToString();
                this.pictureBox1.Invalidate();
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            int orgValueI = MinOpacityTrackBar.Value;
            int newValueI = 0;

            try
            {
                newValueI = Convert.ToInt32(MinOpacityTextBox.Text);
                if (isImageStatus == true && isFirstTimeStatus == false)
                {
                    if (newValueI >= 0 && newValueI <= 100)
                    { 
                        MinOpacityTrackBar.Value = newValueI;
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        MinOpacityTrackBar.Value = orgValueI;
                        MessageBox.Show("Invalid Range(3)! \r\nlease input a value in the range of (0 - 100).", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                fpOpacityTrackBar.Value = orgValueI;
                //MessageBox.Show("Invalid Range(2)! \r\nPlease input range (0 - 100).", "Error Invalid Range!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calContrastValue()
        {
            contrastValue = 1.0f;
            float trackbarValue = (float)contrastTrackBar.Value;

            if (trackbarValue == 0)
            {
                contrastValue = 1.0f;
            }
            else if (trackbarValue > 0)
            {
                contrastValue = contrastValue + (contrastMax * (trackbarValue / 100));
            }
            else if (trackbarValue < 0)
            {
                contrastValue = contrastValue - (contrastMin * (trackbarValue / 100));
            }
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                fpContrastTextBox.Text = contrastTrackBar.Value.ToString();
                this.pictureBox1.Invalidate();
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            int orgValueI = contrastTrackBar.Value;
            int newValueI = 0;

            try
            {
                newValueI = Convert.ToInt32(fpContrastTextBox.Text);
                if (isImageStatus == true && isFirstTimeStatus == false)
                {
                    if (newValueI >= -100 && newValueI <= 100)
                    {
                        contrastTrackBar.Value = newValueI;
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        contrastTrackBar.Value = orgValueI;
                        MessageBox.Show("Invalid Range(4)! \r\nlease input a value in the range of (-100 - 100).", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                contrastTrackBar.Value = orgValueI;
                //MessageBox.Show("Invalid Range(2)! \r\nPlease input range (0 - 100).", "Error Invalid Range!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calBrightnesstValue()
        {
            brightnessValue = 0.0f;
            float trackbarValue = (float)brightnessTrackBar.Value;

            if (trackbarValue == 0)
            {
                brightnessValue = 0.0f;
            }
            else if (trackbarValue > 0)
            {
                brightnessValue = brightnessValue + (brightnessMax * (trackbarValue / 100));
            }
            else if (trackbarValue < 0)
            {
                brightnessValue = brightnessValue - (brightnessMin * (trackbarValue / 100));
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int orgValueI = brightnessTrackBar.Value;
            int newValueI = 0;

            try
            {
                newValueI = Convert.ToInt32(fpBrightnessTextBox.Text);
                if (isImageStatus == true && isFirstTimeStatus == false)
                {
                    if (newValueI >= -100 && newValueI <= 100)
                    {
                        brightnessTrackBar.Value = newValueI;
                        this.pictureBox1.Invalidate();
                    }
                    else
                    {
                        brightnessTrackBar.Value = orgValueI;
                        MessageBox.Show("Invalid Range(5)! \r\nlease input a value in the range of (-100 - 100).", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                brightnessTrackBar.Value = orgValueI;
                MessageBox.Show("Invalid Range(6)! \r\nPlease input range (0 - 100).", "Error Invalid Range!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                fpBrightnessTextBox.Text = brightnessTrackBar.Value.ToString();
                this.pictureBox1.Invalidate();
            }
        }

        private void radioButton_IAFIS_CheckedChanged(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                if (radioButton_IAFIS.Checked)
                {
                    printImageMinutiaeInfo();
                    this.pictureBox1.Invalidate();
                }
            }
        }

        private void radioButton_M1_CheckedChanged(object sender, EventArgs e)
        {
            if (isImageStatus == true)
            {
                if (radioButton_M1.Checked)
                {
                    printImageMinutiaeInfo();
                    this.pictureBox1.Invalidate();
                }
            }
        }

        

        private void masterReset()
        {
            contrastValue = 1.0f;
            brightnessValue = 0.0f;
            resetAllArrays();
            resetAllTrackBars();
            printImageAndToolInfo();
            printImageMinutiaeInfo();
            this.ZoomExtents();
            this.LimitBasePoint(basePoint.X, basePoint.Y);
            this.pictureBox1.Invalidate();
        }

        private void resetAllTrackBars()
        {
            trackBar1.Value = 0;
            fpOpacityTrackBar.Value = 100;
            MinOpacityTrackBar.Value = 100;
            contrastTrackBar.Value = 0;
            brightnessTrackBar.Value = 0;
            radioButton_IAFIS.Checked = true;
            printImageAndToolInfo();
        }

        private void resetAllArrays()
        {
            count = 0;
            totalMinutiae = 0;
            Array.Clear(tempStr, 0, tempStr.Length);
            Array.Clear(arrXY_IAFIS, 0, arrXY_IAFIS.Length);
            Array.Clear(temparrXY_IAFIS, 0, temparrXY_IAFIS.Length);
            Array.Clear(arrX_IAFIS, 0, arrX_IAFIS.Length);
            Array.Clear(arrY_IAFIS, 0, arrY_IAFIS.Length);
            Array.Clear(arrDIR_IAFIS, 0, arrDIR_IAFIS.Length);
            Array.Clear(arrREL_IAFIS, 0, arrREL_IAFIS.Length);
            Array.Clear(arrTYP_IAFIS, 0, arrTYP_IAFIS.Length);
        }

        private void printImageAndToolInfo()
        {
            widthTextBox.Text = Convert.ToString(imgWidth);
            heightTextBox.Text = Convert.ToString(imgHeight);
            totalMinTextBox.Text = Convert.ToString(totalMinutiae);
            displayedMinTextBox.Text = Convert.ToString(displayMinutiae);
            qualityTextBox.Text = Convert.ToString(trackBar1.Value / relScalar);
            fpOpacityTextBox.Text = Convert.ToString(fpOpacityTrackBar.Value);
            MinOpacityTextBox.Text = Convert.ToString(MinOpacityTrackBar.Value);
            fpContrastTextBox.Text = Convert.ToString(contrastTrackBar.Value);
            fpBrightnessTextBox.Text = Convert.ToString(brightnessTrackBar.Value);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void totalMinutiaeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


    }
}
 