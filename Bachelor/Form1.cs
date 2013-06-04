using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CVKMeans;
using CVMeanshift;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap source_bmp, picturebox1_bmp, seg_bmp, picturebox2_bmp, brd_bmp, picturebox3_bmp, res_bmp, picturebox4_bmp;
        KMeans _kMeans;
        SegmentationUtils.ISegmentator filter = new SegmentationUtils.EdgeDetector.Sobol();

        private void button1_Click(object sender, EventArgs e)
        {
            _kMeans = new KMeans(source_bmp, 4, ImageProcessor.Colour.Types.RGB);
            while (!_kMeans.Converged)
            {
                _kMeans.Iterate();
            }
            seg_bmp = _kMeans.ProcessedImage;
            picturebox2_bmp = new Bitmap(seg_bmp,
                    ImageUtils.GenerateImageDimensions(seg_bmp.Width, seg_bmp.Height, pictureBox2.Width, pictureBox2.Height));
            pictureBox2.Image = picturebox2_bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "IMAGES |*.jpg;*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                source_bmp = new Bitmap(openFileDialog1.FileName);
                picturebox1_bmp = new Bitmap(source_bmp,
                    ImageUtils.GenerateImageDimensions(source_bmp.Width, source_bmp.Height, pictureBox1.Width, pictureBox1.Height));
                pictureBox1.Image = picturebox1_bmp;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            brd_bmp = filter.Segmentate(ImageUtils.MakeGrayscale(seg_bmp));
            picturebox3_bmp = new Bitmap(brd_bmp,
                    ImageUtils.GenerateImageDimensions(brd_bmp.Width, brd_bmp.Height, pictureBox3.Width, pictureBox3.Height));
            pictureBox3.Image = picturebox3_bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //source_bmp = new Bitmap(@"C:/Users/birrgrrim/documents/spine_jpg.png");
            picturebox1_bmp = new Bitmap(source_bmp,
                ImageUtils.GenerateImageDimensions(source_bmp.Width, source_bmp.Height, pictureBox1.Width, pictureBox1.Height));
            pictureBox1.Image = picturebox1_bmp;

            Image<Bgr, Byte> img = new Image<Bgr, Byte>(source_bmp);
            //img = img.PyrDown().PyrUp();
            //img._SmoothGaussian(0);
            //img = img.InRange(new Bgr(0, 0, 0)..
            //img = img.PyrDown().PyrUp(); //may be not needed
            //Image<Gray, Byte> gray = img.Convert<Gray, Byte>();//.PyrDown().PyrUp();
            Image<Gray, Byte> gray = img.Convert<Gray, Byte>();//.PyrDown().PyrUp();


            seg_bmp = gray.ToBitmap();
            picturebox2_bmp = new Bitmap(seg_bmp,
                    ImageUtils.GenerateImageDimensions(seg_bmp.Width, seg_bmp.Height, pictureBox2.Width, pictureBox2.Height));
            pictureBox2.Image = picturebox2_bmp;

            Gray cannyThreshold = new Gray(180);
            Gray cannyThresholdLinking = new Gray(120);
            Gray circleAccumulatorThreshold = new Gray(120);

            //Image<Gray, Byte> canny = gray.Canny(cannyThreshold, cannyThresholdLinking);
            Image<Gray, Byte> canny = gray.Canny(180, 120);

            brd_bmp = canny.ToBitmap();
            picturebox3_bmp = new Bitmap(brd_bmp,
                    ImageUtils.GenerateImageDimensions(brd_bmp.Width, brd_bmp.Height, pictureBox3.Width, pictureBox3.Height));
            pictureBox3.Image = picturebox3_bmp;

            /*LineSegment2D[] lines = canny.HoughLinesBinary(
                1, //Distance resolution in pixel-related units
                Math.PI / 45.0, //Angle resolution measured in radians.
                20, //threshold
                30, //min Line width
                10 //gap between lines
                )[0]; //Get the lines from the first channel

            List<LineSegment2D> newlines = new List<LineSegment2D>();

            foreach (LineSegment2D line in lines)
            {
                double deltaY = Math.Abs(line.P2.Y - line.P1.Y);
                double deltaX = Math.Abs(line.P2.X - line.P1.X);
                double angle;
                if (deltaX != 0)
                    angle = Math.Atan2(deltaY, deltaX);
                else
                    angle = 0;
                if(angle < 80) newlines.Add(line);
            }*/

            /*List<MCvBox2D> boxList = new List<MCvBox2D>();

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = canny.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //if (contours.Area > 50) //only consider contours with area greater than 250
                    {
                        //if (currentContour.Total == 4) //The contour has 4 vertices.
                        {
                            
                            bool isRectangle = true;
                            Point[] pts = currentContour.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(
                                   edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }

                            if (isRectangle) boxList.Add(currentContour.GetMinAreaRect());
                        }
                    }
                }*/
            /*Image<Bgr, Byte> rectangleImage = img.Copy();
            foreach (MCvBox2D box in boxList)
                rectangleImage.Draw(box, new Bgr(Color.DarkOrange), 2);*/

            Image<Bgr, Byte> lineImage = img.Copy();
            //foreach (LineSegment2D line in newlines)
            //   lineImage.Draw(line, new Bgr(Color.DarkOrange), 2);

            res_bmp = lineImage.ToBitmap();
            int[] dots = new int[res_bmp.Height];
            Bitmap canny_bmp = canny.ToBitmap();
            List<int> coords = new List<int>();
            List<Point> points = new List<Point>();
            for (int i = 0; i < res_bmp.Height; i++)
            {
                coords.Clear();
                for (int j = 0; j < res_bmp.Width; j++)
                {
                    Color c = canny_bmp.GetPixel(j, i);
                    byte val = c.R;
                    if (val > 128)
                    {
                        coords.Add(j);
                        points.Add(new Point(i, j));
                    }
                }
                if (coords.Count > 0)
                {
                    
                    int midle = coords.Sum() / coords.Count;
                    dots[i] = midle;
                    //Console.WriteLine("[" + i + "]= " + coords.Count.ToString());
                    //for (int k = -3; k <= 3; k++)
                    //    res_bmp.SetPixel(midle + k, i, Color.DarkOrange);
                }
                else
                    dots[i] = -1;
            }

            int eps = 25;
            for (int i = eps; i < res_bmp.Height - eps; i++)
            {
                int sum = 0, count = 0;
                for (int k = i - eps; k <= i + eps; k++)
                {
                    //if (k > 0 && k < res_bmp.Height && dots[k] != -1)
                    if (dots[k] != -1)
                    {
                        sum += dots[k];
                        count++;
                    }
                }
                if (count > 0)
                {
                    int midle = sum / count;
                    for (int d = -3; d <= 3; d++)
                        if(midle + d > 0 && midle + d < res_bmp.Width) res_bmp.SetPixel(midle + d, i, Color.DarkOrange);
                }
            }

            picturebox4_bmp = new Bitmap(res_bmp,
                    ImageUtils.GenerateImageDimensions(res_bmp.Width, res_bmp.Height, pictureBox4.Width, pictureBox4.Height));
            pictureBox4.Image = picturebox4_bmp;
        }
    }
}
