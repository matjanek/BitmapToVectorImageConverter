using System;

namespace BitmapToVectorImageConverter
{
	public class GisObjectR2V
	{
		public long X { get; set; } // najlepiej byłoby wszystkie property mieć z wielkiej litery (taka jest konwencja w C#)
		public long Y { get; set; }

        public GisObjectR2V()
        {
            this.X = long.MinValue;
            this.Y = long.MinValue;
        }

		public GisObjectR2V (long x, long y)
		{
            this.X = x;
            this.Y = y;
		}

        public override string ToString()
        {
            return String.Format("{0}, {1}", X, Y);
        }
	}
}

