﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SegmentationUtils.EdgeDetector
{
    public class Sobol : EdgeDetector
    {
        override sealed public Bitmap Segmentate(Bitmap img)
        {
            Bitmap edge = new Bitmap(img.Width, img.Height); // Output image

            for (int i = 1; i < edge.Height - 1; i++)
            {
                for (int j = 1; j < edge.Width - 1; j++)
                {
                    int mag = 0;
                    mag += Math.Abs(img.GetPixel(j - 1, i - 1).R * -1
                                  - img.GetPixel(j - 1, i).R * 2
                                  - img.GetPixel(j - 1, i + 1).R
                                  + img.GetPixel(j + 1, i - 1).R
                                  + img.GetPixel(j + 1, i).R * 2
                                  + img.GetPixel(j + 1, i + 1).R);
                    mag += Math.Abs(img.GetPixel(j - 1, i - 1).R * -1
                                  - img.GetPixel(j, i - 1).R * 2
                                  - img.GetPixel(j + 1, i - 1).R
                                  + img.GetPixel(j - 1, i + 1).R
                                  + img.GetPixel(j, i + 1).R * 2
                                  + img.GetPixel(j + 1, i + 1).R);

                    // Just dumb code to scale the data to [0,255]
                    mag /= 8;

                    edge.SetPixel(j, i, Color.FromArgb(mag, mag, mag));
                }
            }
            return edge;
        }
    }
}
