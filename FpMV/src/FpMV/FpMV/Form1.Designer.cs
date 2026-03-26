namespace FpMV
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.minDetectButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.nfiqButton = new System.Windows.Forms.Button();
            this.nfiqScoreTextBox = new System.Windows.Forms.TextBox();
            this.radioButton_IAFIS = new System.Windows.Forms.RadioButton();
            this.radioButton_M1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.qualityLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.MinOpacityTextBox = new System.Windows.Forms.TextBox();
            this.qualityTextBox = new System.Windows.Forms.TextBox();
            this.minOpacityLabel = new System.Windows.Forms.Label();
            this.fpPpacityLabel = new System.Windows.Forms.Label();
            this.MinOpacityTrackBar = new System.Windows.Forms.TrackBar();
            this.fpOpacityTextBox = new System.Windows.Forms.TextBox();
            this.fpOpacityTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fpBrightnessLabel = new System.Windows.Forms.Label();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.fpContrastLabel = new System.Windows.Forms.Label();
            this.fpBrightnessTextBox = new System.Windows.Forms.TextBox();
            this.fpContrastTextBox = new System.Windows.Forms.TextBox();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.displayedMinTextBox = new System.Windows.Forms.TextBox();
            this.displayedMinLabel = new System.Windows.Forms.Label();
            this.totalMinTextBox = new System.Windows.Forms.TextBox();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.totalMinDetectedLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinOpacityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOpacityTrackBar)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(781, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.quitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Visible = false;
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // quitToolStripMenuItem1
            // 
            this.quitToolStripMenuItem1.Name = "quitToolStripMenuItem1";
            this.quitToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.quitToolStripMenuItem1.Text = "Quit";
            this.quitToolStripMenuItem1.Click += new System.EventHandler(this.quitToolStripMenuItem1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CheckFileExists = true;
            this.saveFileDialog1.FilterIndex = 2;
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(156, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(625, 603);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(159, 632);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(619, 173);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // textBox6
            // 
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(159, 606);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(619, 26);
            this.textBox6.TabIndex = 9;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.minDetectButton);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.resetButton);
            this.groupBox2.Controls.Add(this.nfiqButton);
            this.groupBox2.Controls.Add(this.nfiqScoreTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 632);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 173);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // minDetectButton
            // 
            this.minDetectButton.Location = new System.Drawing.Point(31, 19);
            this.minDetectButton.Name = "minDetectButton";
            this.minDetectButton.Size = new System.Drawing.Size(75, 23);
            this.minDetectButton.TabIndex = 0;
            this.minDetectButton.Text = "Min. Detect";
            this.minDetectButton.UseVisualStyleBackColor = true;
            this.minDetectButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 12;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(30, 126);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(76, 20);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // nfiqButton
            // 
            this.nfiqButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nfiqButton.Location = new System.Drawing.Point(31, 63);
            this.nfiqButton.Name = "nfiqButton";
            this.nfiqButton.Size = new System.Drawing.Size(75, 24);
            this.nfiqButton.TabIndex = 9;
            this.nfiqButton.Text = "NFIQ Score";
            this.nfiqButton.UseVisualStyleBackColor = true;
            this.nfiqButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // nfiqScoreTextBox
            // 
            this.nfiqScoreTextBox.Location = new System.Drawing.Point(53, 93);
            this.nfiqScoreTextBox.Name = "nfiqScoreTextBox";
            this.nfiqScoreTextBox.ReadOnly = true;
            this.nfiqScoreTextBox.Size = new System.Drawing.Size(30, 20);
            this.nfiqScoreTextBox.TabIndex = 14;
            this.nfiqScoreTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nfiqScoreTextBox.TextChanged += new System.EventHandler(this.textBox15_TextChanged);
            // 
            // radioButton_IAFIS
            // 
            this.radioButton_IAFIS.AutoSize = true;
            this.radioButton_IAFIS.Checked = true;
            this.radioButton_IAFIS.Location = new System.Drawing.Point(20, 1);
            this.radioButton_IAFIS.Name = "radioButton_IAFIS";
            this.radioButton_IAFIS.Size = new System.Drawing.Size(51, 17);
            this.radioButton_IAFIS.TabIndex = 9;
            this.radioButton_IAFIS.TabStop = true;
            this.radioButton_IAFIS.Text = "IAFIS";
            this.radioButton_IAFIS.UseVisualStyleBackColor = true;
            this.radioButton_IAFIS.CheckedChanged += new System.EventHandler(this.radioButton_IAFIS_CheckedChanged);
            // 
            // radioButton_M1
            // 
            this.radioButton_M1.AutoSize = true;
            this.radioButton_M1.Location = new System.Drawing.Point(80, 1);
            this.radioButton_M1.Name = "radioButton_M1";
            this.radioButton_M1.Size = new System.Drawing.Size(40, 17);
            this.radioButton_M1.TabIndex = 10;
            this.radioButton_M1.Text = "M1";
            this.radioButton_M1.UseVisualStyleBackColor = true;
            this.radioButton_M1.CheckedChanged += new System.EventHandler(this.radioButton_M1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.displayedMinTextBox);
            this.groupBox1.Controls.Add(this.displayedMinLabel);
            this.groupBox1.Controls.Add(this.totalMinTextBox);
            this.groupBox1.Controls.Add(this.heightTextBox);
            this.groupBox1.Controls.Add(this.widthTextBox);
            this.groupBox1.Controls.Add(this.widthLabel);
            this.groupBox1.Controls.Add(this.totalMinDetectedLabel);
            this.groupBox1.Controls.Add(this.heightLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 597);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Panel";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.qualityLabel);
            this.groupBox5.Controls.Add(this.trackBar1);
            this.groupBox5.Controls.Add(this.MinOpacityTextBox);
            this.groupBox5.Controls.Add(this.qualityTextBox);
            this.groupBox5.Controls.Add(this.minOpacityLabel);
            this.groupBox5.Controls.Add(this.fpPpacityLabel);
            this.groupBox5.Controls.Add(this.MinOpacityTrackBar);
            this.groupBox5.Controls.Add(this.fpOpacityTextBox);
            this.groupBox5.Controls.Add(this.fpOpacityTrackBar);
            this.groupBox5.Location = new System.Drawing.Point(3, 192);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(149, 235);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Display Setting";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // qualityLabel
            // 
            this.qualityLabel.AutoSize = true;
            this.qualityLabel.Location = new System.Drawing.Point(6, 29);
            this.qualityLabel.Name = "qualityLabel";
            this.qualityLabel.Size = new System.Drawing.Size(94, 13);
            this.qualityLabel.TabIndex = 1;
            this.qualityLabel.Text = "Quality (Unit)      > ";
            this.qualityLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 52);
            this.trackBar1.Maximum = 99;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeftLayout = true;
            this.trackBar1.Size = new System.Drawing.Size(146, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // MinOpacityTextBox
            // 
            this.MinOpacityTextBox.AcceptsReturn = true;
            this.MinOpacityTextBox.Location = new System.Drawing.Point(106, 170);
            this.MinOpacityTextBox.MaxLength = 4;
            this.MinOpacityTextBox.Name = "MinOpacityTextBox";
            this.MinOpacityTextBox.Size = new System.Drawing.Size(39, 20);
            this.MinOpacityTextBox.TabIndex = 7;
            this.MinOpacityTextBox.TextChanged += new System.EventHandler(this.textBox14_TextChanged);
            // 
            // qualityTextBox
            // 
            this.qualityTextBox.AcceptsReturn = true;
            this.qualityTextBox.Location = new System.Drawing.Point(105, 26);
            this.qualityTextBox.MaxLength = 4;
            this.qualityTextBox.Name = "qualityTextBox";
            this.qualityTextBox.Size = new System.Drawing.Size(39, 20);
            this.qualityTextBox.TabIndex = 1;
            this.qualityTextBox.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // minOpacityLabel
            // 
            this.minOpacityLabel.AutoSize = true;
            this.minOpacityLabel.Location = new System.Drawing.Point(7, 173);
            this.minOpacityLabel.Name = "minOpacityLabel";
            this.minOpacityLabel.Size = new System.Drawing.Size(95, 13);
            this.minOpacityLabel.TabIndex = 17;
            this.minOpacityLabel.Text = "Min Opacity (%)  > ";
            // 
            // fpPpacityLabel
            // 
            this.fpPpacityLabel.AutoSize = true;
            this.fpPpacityLabel.Location = new System.Drawing.Point(7, 100);
            this.fpPpacityLabel.Name = "fpPpacityLabel";
            this.fpPpacityLabel.Size = new System.Drawing.Size(94, 13);
            this.fpPpacityLabel.TabIndex = 13;
            this.fpPpacityLabel.Text = "FP Opacity (%)   > ";
            this.fpPpacityLabel.Click += new System.EventHandler(this.label7_Click);
            // 
            // MinOpacityTrackBar
            // 
            this.MinOpacityTrackBar.Location = new System.Drawing.Point(1, 197);
            this.MinOpacityTrackBar.Maximum = 100;
            this.MinOpacityTrackBar.Name = "MinOpacityTrackBar";
            this.MinOpacityTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MinOpacityTrackBar.RightToLeftLayout = true;
            this.MinOpacityTrackBar.Size = new System.Drawing.Size(146, 45);
            this.MinOpacityTrackBar.TabIndex = 8;
            this.MinOpacityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.MinOpacityTrackBar.Value = 1;
            this.MinOpacityTrackBar.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // fpOpacityTextBox
            // 
            this.fpOpacityTextBox.AcceptsReturn = true;
            this.fpOpacityTextBox.Location = new System.Drawing.Point(105, 97);
            this.fpOpacityTextBox.MaxLength = 4;
            this.fpOpacityTextBox.Name = "fpOpacityTextBox";
            this.fpOpacityTextBox.Size = new System.Drawing.Size(39, 20);
            this.fpOpacityTextBox.TabIndex = 5;
            this.fpOpacityTextBox.TextChanged += new System.EventHandler(this.textBox13_TextChanged);
            // 
            // fpOpacityTrackBar
            // 
            this.fpOpacityTrackBar.Location = new System.Drawing.Point(1, 125);
            this.fpOpacityTrackBar.Maximum = 100;
            this.fpOpacityTrackBar.Name = "fpOpacityTrackBar";
            this.fpOpacityTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fpOpacityTrackBar.RightToLeftLayout = true;
            this.fpOpacityTrackBar.Size = new System.Drawing.Size(146, 45);
            this.fpOpacityTrackBar.TabIndex = 6;
            this.fpOpacityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.fpOpacityTrackBar.Value = 1;
            this.fpOpacityTrackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fpBrightnessLabel);
            this.groupBox4.Controls.Add(this.brightnessTrackBar);
            this.groupBox4.Controls.Add(this.fpContrastLabel);
            this.groupBox4.Controls.Add(this.fpBrightnessTextBox);
            this.groupBox4.Controls.Add(this.fpContrastTextBox);
            this.groupBox4.Controls.Add(this.contrastTrackBar);
            this.groupBox4.Location = new System.Drawing.Point(3, 433);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 167);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image Setting";
            // 
            // fpBrightnessLabel
            // 
            this.fpBrightnessLabel.AutoSize = true;
            this.fpBrightnessLabel.Location = new System.Drawing.Point(4, 32);
            this.fpBrightnessLabel.Name = "fpBrightnessLabel";
            this.fpBrightnessLabel.Size = new System.Drawing.Size(101, 13);
            this.fpBrightnessLabel.TabIndex = 25;
            this.fpBrightnessLabel.Text = "FP Brightness (%) > ";
            this.fpBrightnessLabel.Click += new System.EventHandler(this.label10_Click);
            // 
            // brightnessTrackBar
            // 
            this.brightnessTrackBar.Location = new System.Drawing.Point(-1, 58);
            this.brightnessTrackBar.Maximum = 100;
            this.brightnessTrackBar.Minimum = -100;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.brightnessTrackBar.RightToLeftLayout = true;
            this.brightnessTrackBar.Size = new System.Drawing.Size(146, 45);
            this.brightnessTrackBar.SmallChange = 5;
            this.brightnessTrackBar.TabIndex = 24;
            this.brightnessTrackBar.TickFrequency = 5;
            this.brightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.brightnessTrackBar.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // fpContrastLabel
            // 
            this.fpContrastLabel.AutoSize = true;
            this.fpContrastLabel.Location = new System.Drawing.Point(4, 106);
            this.fpContrastLabel.Name = "fpContrastLabel";
            this.fpContrastLabel.Size = new System.Drawing.Size(94, 13);
            this.fpContrastLabel.TabIndex = 22;
            this.fpContrastLabel.Text = "FP Contrast (%)  > ";
            this.fpContrastLabel.Click += new System.EventHandler(this.label9_Click);
            // 
            // fpBrightnessTextBox
            // 
            this.fpBrightnessTextBox.AcceptsReturn = true;
            this.fpBrightnessTextBox.Location = new System.Drawing.Point(104, 29);
            this.fpBrightnessTextBox.MaxLength = 4;
            this.fpBrightnessTextBox.Name = "fpBrightnessTextBox";
            this.fpBrightnessTextBox.Size = new System.Drawing.Size(38, 20);
            this.fpBrightnessTextBox.TabIndex = 23;
            this.fpBrightnessTextBox.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // fpContrastTextBox
            // 
            this.fpContrastTextBox.AcceptsReturn = true;
            this.fpContrastTextBox.Location = new System.Drawing.Point(104, 103);
            this.fpContrastTextBox.MaxLength = 4;
            this.fpContrastTextBox.Name = "fpContrastTextBox";
            this.fpContrastTextBox.Size = new System.Drawing.Size(39, 20);
            this.fpContrastTextBox.TabIndex = 3;
            this.fpContrastTextBox.TextChanged += new System.EventHandler(this.textBox16_TextChanged);
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.Location = new System.Drawing.Point(-1, 129);
            this.contrastTrackBar.Maximum = 100;
            this.contrastTrackBar.Minimum = -100;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.contrastTrackBar.RightToLeftLayout = true;
            this.contrastTrackBar.Size = new System.Drawing.Size(146, 45);
            this.contrastTrackBar.SmallChange = 5;
            this.contrastTrackBar.TabIndex = 4;
            this.contrastTrackBar.TickFrequency = 5;
            this.contrastTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // displayedMinTextBox
            // 
            this.displayedMinTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.displayedMinTextBox.Location = new System.Drawing.Point(20, 158);
            this.displayedMinTextBox.Name = "displayedMinTextBox";
            this.displayedMinTextBox.Size = new System.Drawing.Size(100, 20);
            this.displayedMinTextBox.TabIndex = 0;
            this.displayedMinTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.displayedMinTextBox.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // displayedMinLabel
            // 
            this.displayedMinLabel.AutoSize = true;
            this.displayedMinLabel.Location = new System.Drawing.Point(21, 142);
            this.displayedMinLabel.Name = "displayedMinLabel";
            this.displayedMinLabel.Size = new System.Drawing.Size(99, 13);
            this.displayedMinLabel.TabIndex = 11;
            this.displayedMinLabel.Text = "Displayed Minutiae:";
            this.displayedMinLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // totalMinTextBox
            // 
            this.totalMinTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.totalMinTextBox.Location = new System.Drawing.Point(20, 107);
            this.totalMinTextBox.Name = "totalMinTextBox";
            this.totalMinTextBox.Size = new System.Drawing.Size(100, 20);
            this.totalMinTextBox.TabIndex = 0;
            this.totalMinTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalMinTextBox.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(84, 53);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.ReadOnly = true;
            this.heightTextBox.Size = new System.Drawing.Size(46, 20);
            this.heightTextBox.TabIndex = 0;
            this.heightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.heightTextBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(10, 53);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.ReadOnly = true;
            this.widthTextBox.Size = new System.Drawing.Size(48, 20);
            this.widthTextBox.TabIndex = 0;
            this.widthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.widthTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(17, 37);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 4;
            this.widthLabel.Text = "Width:";
            this.widthLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // totalMinDetectedLabel
            // 
            this.totalMinDetectedLabel.AutoSize = true;
            this.totalMinDetectedLabel.Location = new System.Drawing.Point(9, 91);
            this.totalMinDetectedLabel.Name = "totalMinDetectedLabel";
            this.totalMinDetectedLabel.Size = new System.Drawing.Size(124, 13);
            this.totalMinDetectedLabel.TabIndex = 3;
            this.totalMinDetectedLabel.Text = "Total Minutiae Detected:";
            this.totalMinDetectedLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(87, 37);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height:";
            this.heightLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 808);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_IAFIS);
            this.groupBox3.Controls.Add(this.radioButton_M1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 606);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 20);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(781, 832);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinOpacityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOpacityTrackBar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar fpOpacityTrackBar;
        private System.Windows.Forms.TextBox fpOpacityTextBox;
        private System.Windows.Forms.Label fpPpacityLabel;
        private System.Windows.Forms.TextBox displayedMinTextBox;
        private System.Windows.Forms.Label displayedMinLabel;
        private System.Windows.Forms.RadioButton radioButton_M1;
        private System.Windows.Forms.RadioButton radioButton_IAFIS;
        private System.Windows.Forms.TextBox qualityTextBox;
        private System.Windows.Forms.TextBox totalMinTextBox;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label totalMinDetectedLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label qualityLabel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox MinOpacityTextBox;
        private System.Windows.Forms.Label minOpacityLabel;
        private System.Windows.Forms.TrackBar MinOpacityTrackBar;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button nfiqButton;
        private System.Windows.Forms.TextBox nfiqScoreTextBox;
        private System.Windows.Forms.Label fpContrastLabel;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.TextBox fpContrastTextBox;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.TextBox fpBrightnessTextBox;
        private System.Windows.Forms.Label fpBrightnessLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button minDetectButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

    }
}

