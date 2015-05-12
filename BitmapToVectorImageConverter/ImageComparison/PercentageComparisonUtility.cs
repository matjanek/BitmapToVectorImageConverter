using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapToVectorImageConverter.ImageComparison
{
    class PercentageComparisonUtility
    {
        public static double Compare(Bitmap image1, Bitmap image2)
        {
            if (image1 == null || image2 == null)
            {
                return double.NaN;
            }
            double difference = 0d;
            int width1 = image1.Width;
            int height1 = image1.Height;
            int width2 = image2.Width;
            int height2 = image2.Height;
            if (width1 != width2 || height1 != height2)
            {
                return double.NaN;
            }
            for (int i = 0; i < width1; i++)
            {
                for (int j = 0; j < height1; j++)
                {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);
                    difference += Math.Abs(pixel1.R - pixel2.R) / 255d;
                    difference += Math.Abs(pixel1.G - pixel2.G) / 255d;
                    difference += Math.Abs(pixel1.B - pixel2.B) / 255d;
                }
            }
            
            return (difference * 100d) / (width1 * height1 * 3);
        }
    }
}
