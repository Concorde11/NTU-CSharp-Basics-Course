namespace Vision_Assistant
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
            this.imageViewer = new NationalInstruments.Vision.WindowsForms.ImageViewer();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // imageViewer
            // 
            this.imageViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewer.Location = new System.Drawing.Point(0, 0);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(640, 443);
            this.imageViewer.TabIndex = 0;
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(650, 9);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(90, 28);
            this.LoadImageButton.TabIndex = 1;
            this.LoadImageButton.Text = "載入照片";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(650, 42);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(90, 28);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "尺寸量測";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(650, 76);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(90, 28);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "離開";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 446);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.LoadImageButton);
            this.Controls.Add(this.imageViewer);
            this.Name = "Form1";
            this.Text = "機械視覺檢測";
            this.ResumeLayout(false);

        }

        #endregion

        private NationalInstruments.Vision.WindowsForms.ImageViewer imageViewer;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

