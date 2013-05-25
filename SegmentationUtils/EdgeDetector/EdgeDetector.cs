using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SegmentationUtils.EdgeDetector
{
    abstract public class EdgeDetector : ISegmentator
    {
        virtual public Bitmap Segmentate(Bitmap b)
        {
            throw new NotImplementedException();
        }
    }
}
