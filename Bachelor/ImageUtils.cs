using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Bachelor
{
    class ImageUtils
    {
        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix (
               new float[][]
               {
                   new float[] {.3f, .3f, .3f, 0, 0},
                   new float[] {.59f, .59f, .59f, 0, 0},
                   new float[] {.11f, .11f, .11f, 0, 0},
                   new float[] {0, 0, 0, 1, 0},
                   new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        public static Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            //double to hold the final multiplier to use when scaling the image
            double multiplier = Math.Min((double)destW / (double)currW, (double)destH / (double)currH);
            //string for holding layout
            /*string layout;
            //determine if it's Portrait or Landscape
            if (currH > currW) layout = "portrait";
            else layout = "landscape";
            switch (layout.ToLower())
            {
                case "portrait":
                    //calculate multiplier on heights
                    multiplier = Math.Min((double)destW / (double)currW, (double)destH / (double)currH);
                    if (destH > destW)
                    {
                        multiplier = (double)destW / (double)currW;
                    }
                    else
                    {
                        multiplier = (double)destH / (double)currH;
                    }
                    break;
                case "landscape":
                    //calculate multiplier on widths
                    if (destH > destW)
                    {
                        multiplier = (double)destW / (double)currW;
                    }
                    else
                    {
                        multiplier = (double)destH / (double)currH;
                    }
                    break;
            }*/
            //return the new image dimensions
            return new Size((int)(currW * multiplier), (int)(currH * multiplier));
        }
    }
}
