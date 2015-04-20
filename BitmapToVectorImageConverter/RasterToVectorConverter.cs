using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapToVectorImageConverter
{
    public class RasterToVectorConverter
    {
        private Bitmap _bitmap;
        private ImageProcessor _imageProcessor;

        public RasterToVectorConverter(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                _bitmap = bitmap;
                _imageProcessor = new ImageProcessor(_bitmap);
            }
        }

        public void Convert()
        {
            int imageHeight = _imageProcessor.GetImageHeight();
            for (int i = 0; i < imageHeight - 1; i++)
            {
                var floatingWindow = _imageProcessor.GetFloatingWindow(i);
                // todo
                // do magic with floatingWindow
            }
            return;
        }
    }
}
