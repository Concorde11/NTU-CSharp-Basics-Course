using NationalInstruments.Vision;
using NationalInstruments.Vision.Analysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vision_Assistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            ImagePreviewFileDialog imageDialog = new ImagePreviewFileDialog();

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                FileInformation fileinfo = Algorithms.GetFileInformation(imageDialog.FileName);
                imageViewer.Image.Type = fileinfo.ImageType;
                imageViewer.Image.ReadFile(imageDialog.FileName);
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            imageViewer.Palette.Type = Image_Processing.ProcessImage(imageViewer.Image);

           
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}