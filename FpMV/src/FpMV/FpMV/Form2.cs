using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace FpMV
{
    public partial class Form2 : Form
    {
        public int width;
        public int height;
        public int fileSize;
        public string chosenFile;
        public string fileNamePrefix;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string chosenFile, string fileNamePrefix)
        {
            InitializeComponent();
            this.fileNamePrefix = fileNamePrefix;
            this.chosenFile = chosenFile;
            praseWidthHeightFromFileName();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.MouseHover += new EventHandler(Form2_MouseHover);
        }

        private void Form2_MouseHover(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void getFileSize()
        {
            this.fileSize = (int)new System.IO.FileInfo(this.chosenFile).Length;   
        }

        private void praseWidthHeightFromFileName()
        {
            string[] arr1 = this.fileNamePrefix.Split('_');
            if (arr1.Length == 2)
            {
                string[] arr2 = arr1[1].Split('x');
                if (arr2.Length == 2)
                {
                    imgWidthTextBox.Text = arr2[0].ToString();
                    imgHeightTextBox.Text = arr2[1].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num;

            getFileSize();

            if (imgWidthTextBox.Text == "" || imgHeightTextBox.Text == "")
            {
                MessageBox.Show("Image width and/or image height can't be empty!", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!(int.TryParse(imgWidthTextBox.Text, out num)) || !(int.TryParse(imgHeightTextBox.Text, out num)))
            {
                MessageBox.Show("Image width and/or image height need to be numeric!", "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((Convert.ToInt32(imgWidthTextBox.Text) * Convert.ToInt32(imgHeightTextBox.Text)).CompareTo(this.fileSize) != 0)
            {
                int w = Convert.ToInt32(imgWidthTextBox.Text);
                int h = Convert.ToInt32(imgHeightTextBox.Text);
                int dim = w * h;

                String errMsg = "Incorrect values for width and height given! Width multiplied by height must equal the size of the image file (number of bytes)!\r\n" + w + " X " + h + " does not equal " + this.fileSize.ToString() +"!";
                
                MessageBox.Show(errMsg, "Invalid Range Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.width = Convert.ToInt32(imgWidthTextBox.Text);
                this.height = Convert.ToInt32(imgHeightTextBox.Text);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
