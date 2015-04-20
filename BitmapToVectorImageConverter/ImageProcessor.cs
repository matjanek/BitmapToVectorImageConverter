using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapToVectorImageConverter
{
    public class ImageProcessor
    {
        private Bitmap _bitmap;
        private int _bitmapWidth;
        private int _bitmapHeight;
        
        public ImageProcessor(Bitmap bitmap)
        {
            _bitmap = bitmap;
            if (bitmap != null)
            {
                _bitmapWidth = bitmap.Width;
                _bitmapHeight = bitmap.Height;
            }
            else
            {
                _bitmapWidth = 0;
                _bitmapHeight = 0;
            }
        }

        public Color[,] GetFloatingWindow(int startLine)
        {
            int endLine = startLine + 1;
            if (startLine < 0 || endLine > _bitmapHeight - 1)
            {
                return null;
            }
            Color[,] floatingWindow = new Color[_bitmapWidth, 2];
            for (int i = 0; i < _bitmapWidth; i++)
            {
                floatingWindow[i, 0] = _bitmap.GetPixel(i, startLine);
                floatingWindow[i, 1] = _bitmap.GetPixel(i, endLine);
            }
            return floatingWindow;
        }

        public int GetImageHeight()
        {
            return _bitmapHeight;
        }

        public int GetImageWidth()
        {
            return _bitmapWidth;
        }
    }
}
