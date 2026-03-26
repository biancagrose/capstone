namespace FpMV
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.imgWidthLabel = new System.Windows.Forms.Label();
            this.imgHeightLabel = new System.Windows.Forms.Label();
            this.imgHeightTextBox = new System.Windows.Forms.TextBox();
            this.imgWidthTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(197, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imgWidthLabel
            // 
            this.imgWidthLabel.AutoSize = true;
            this.imgWidthLabel.Location = new System.Drawing.Point(12, 27);
            this.imgWidthLabel.Name = "imgWidthLabel";
            this.imgWidthLabel.Size = new System.Drawing.Size(67, 13);
            this.imgWidthLabel.TabIndex = 5;
            this.imgWidthLabel.Text = "Image width:";
            this.imgWidthLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // imgHeightLabel
            // 
            this.imgHeightLabel.AutoSize = true;
            this.imgHeightLabel.Location = new System.Drawing.Point(141, 27);
            this.imgHeightLabel.Name = "imgHeightLabel";
            this.imgHeightLabel.Size = new System.Drawing.Size(71, 13);
            this.imgHeightLabel.TabIndex = 6;
            this.imgHeightLabel.Text = "Image height:";
            this.imgHeightLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // imgHeightTextBox
            // 
            this.imgHeightTextBox.AcceptsReturn = true;
            this.imgHeightTextBox.AcceptsTab = true;
            this.imgHeightTextBox.Location = new System.Drawing.Point(218, 24);
            this.imgHeightTextBox.MaxLength = 4;
            this.imgHeightTextBox.Name = "imgHeightTextBox";
            this.imgHeightTextBox.Size = new System.Drawing.Size(54, 20);
            this.imgHeightTextBox.TabIndex = 2;
            this.imgHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imgWidthTextBox
            // 
            this.imgWidthTextBox.AcceptsReturn = true;
            this.imgWidthTextBox.AcceptsTab = true;
            this.imgWidthTextBox.Location = new System.Drawing.Point(81, 24);
            this.imgWidthTextBox.MaxLength = 4;
            this.imgWidthTextBox.Name = "imgWidthTextBox";
            this.imgWidthTextBox.Size = new System.Drawing.Size(54, 20);
            this.imgWidthTextBox.TabIndex = 1;
            this.imgWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imgWidthTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form2
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(284, 105);
            this.Controls.Add(this.imgWidthTextBox);
            this.Controls.Add(this.imgHeightTextBox);
            this.Controls.Add(this.imgHeightLabel);
            this.Controls.Add(this.imgWidthLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set RAW Dimension";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label imgWidthLabel;
        private System.Windows.Forms.Label imgHeightLabel;
        private System.Windows.Forms.TextBox imgHeightTextBox;
        private System.Windows.Forms.TextBox imgWidthTextBox;
    }
}