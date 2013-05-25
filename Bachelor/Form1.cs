using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bachelor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap source_bmp, picturebox1_bmp, seg_bmp, picturebox2_bmp;

        private void button1_Click(object sender, EventArgs e)
        {
            _kMeans = new KMeans(source_bmp, 3, ImageProcessor.Colour.Types.RGB);

            picturebox2_bmp = new Bitmap(seg_bmp,
                    ImageUtils.GenerateImageDimensions(seg_bmp.Width, seg_bmp.Height, pictureBox2.Width, pictureBox2.Height));
            pictureBox2.Image = picturebox2_bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                source_bmp = new Bitmap(openFileDialog1.FileName);
                picturebox1_bmp = new Bitmap(source_bmp,
                    ImageUtils.GenerateImageDimensions(source_bmp.Width, source_bmp.Height, pictureBox1.Width, pictureBox1.Height));
                pictureBox1.Image = picturebox1_bmp;
                //progressBar1.Maximum = source_bmp.Width * source_bmp.Height;
            }
        }
    }
}
