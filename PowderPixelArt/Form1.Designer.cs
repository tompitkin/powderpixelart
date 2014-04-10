namespace PowderPixelArt
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.bStart = new System.Windows.Forms.Button();
            this.bLoadImage = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.delaySpeedText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Black;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(395, 295);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Location = new System.Drawing.Point(332, 313);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 1;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bLoadImage
            // 
            this.bLoadImage.Location = new System.Drawing.Point(251, 313);
            this.bLoadImage.Name = "bLoadImage";
            this.bLoadImage.Size = new System.Drawing.Size(75, 23);
            this.bLoadImage.TabIndex = 2;
            this.bLoadImage.Text = "Load Image";
            this.bLoadImage.UseVisualStyleBackColor = true;
            this.bLoadImage.Click += new System.EventHandler(this.bLoadImage_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg";
            // 
            // DelayUpDown
            // 
            this.DelayUpDown.Location = new System.Drawing.Point(86, 313);
            this.DelayUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.DelayUpDown.Name = "DelayUpDown";
            this.DelayUpDown.Size = new System.Drawing.Size(47, 20);
            this.DelayUpDown.TabIndex = 3;
            this.DelayUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DelayUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.DelayUpDown.ValueChanged += new System.EventHandler(this.DelayUpDown_ValueChanged);
            // 
            // delaySpeedText
            // 
            this.delaySpeedText.AutoSize = true;
            this.delaySpeedText.Location = new System.Drawing.Point(12, 315);
            this.delaySpeedText.Name = "delaySpeedText";
            this.delaySpeedText.Size = new System.Drawing.Size(71, 13);
            this.delaySpeedText.TabIndex = 4;
            this.delaySpeedText.Text = "Delay Speed:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 342);
            this.Controls.Add(this.delaySpeedText);
            this.Controls.Add(this.DelayUpDown);
            this.Controls.Add(this.bLoadImage);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Powder Pixel Art";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bLoadImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NumericUpDown DelayUpDown;
        private System.Windows.Forms.Label delaySpeedText;
    }
}

