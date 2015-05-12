using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomont;

namespace BitmapToVectorImageConverter.ImageComparison
{
    public enum ImageComparisonMethods
    {
        Percentage,
        SSIM
    }
    public enum ImageComparisonPair
    {
        InputVector,
        InputOutput,
        VectorOutput
    }
    public class ImageComparer
    {
        private Bitmap _input;
        private Bitmap _vector;
        private Bitmap _output;

        public ImageComparer(Bitmap input, Bitmap vector, Bitmap output)
        {
            _input = input;
            _vector = vector;
            _output = output;
        }

        public double Compare(ImageComparisonPair ICP, ImageComparisonMethods ICM)
        {
            Bitmap image1;
            Bitmap image2;
            switch (ICP)
            {
                case ImageComparisonPair.InputVector:
                    image1 = _input;
                    image2 = _vector;
                    break;
                case ImageComparisonPair.InputOutput:
                    image1 = _input;
                    image2 = _output;
                    break;
                case ImageComparisonPair.VectorOutput:
                    image1 = _vector;
                    image2 = _output;
                    break;
                default:
                    return double.NaN;
            }
            if (image1 == null || image2 == null)
            {
                return double.NaN;
            }
            switch (ICM)
            {
                case ImageComparisonMethods.Percentage:
                    return PercentageComparisonUtility.Compare(image1, image2);
                case ImageComparisonMethods.SSIM:
                    SSIM ssim = new SSIM();
                    return ssim.Index(image1, image2);
                default:
                    return double.NaN;
            }
        }
    }
}
