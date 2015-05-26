using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lomont;

namespace BitmapToVectorImageConverter.ImageComparison
{
    public enum ImageComparisonMethods
    {
        Percentage,
        SSIM
    }
    
    public class ImageComparer
    {
        private Bitmap _model;
        private List<Bitmap> _images;
        private List<string> _fileNames;

        public ImageComparer(Bitmap model, List<Bitmap> images, ListBox.ObjectCollection fileNames)
        {
            _model = model;
            _images = images;
            _fileNames = new List<string>();
            foreach (var fileName in fileNames)
            {
                _fileNames.Add(fileName.ToString());
            }
        }

        public string Compare(IProgress<int> progress)
        {
            const int methodsCount = 3;
            int images = _images.Count;
            int iterations = methodsCount*images;
            string results = "File;Percentage;SSIM;MSE\r\n";
            ReportProgress(progress, 0d);
            for (int i = 0; i < images; i++)
            {
                Bitmap image = _images[i];
                if (_model.Width == image.Width && _model.Height == image.Height)
                {
                    double percentageResult = PercentageComparisonUtility.Compare(_model, image);
                    ReportProgress(progress, 100 * (3 * i + 1) / iterations);
                    SSIM ssim = new SSIM();
                    double ssimResult = ssim.Index(_model, image);
                    ReportProgress(progress, 100 * (3 * i + 2) / iterations);
                    // TODO MSE compare
                    double mseResult = double.NaN;
                    ReportProgress(progress, 100 * (3 * i + 3) / iterations);
                    results += _fileNames[i] + ";" + percentageResult + ";" + ssimResult + ";" + mseResult + "\r\n";
                }
                else
                {
                    ReportProgress(progress, 100 * (3 * i + 3) / iterations);
                    results += _fileNames[i] + ";" + double.NaN + ";" + double.NaN + ";" + double.NaN + "\r\n";
                }
            }
            return results;
        }

        void ReportProgress(IProgress<int> progress, double progressValue)
        {
            if (progress != null)
            {
                progress.Report((int)Math.Round(progressValue));
            }
        }
    }
}
