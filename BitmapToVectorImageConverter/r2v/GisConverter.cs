using System;
using System.Drawing;

namespace BitmapToVectorImageConverter
{
	public class GisConverter
	{
		private GisConverter ()
		{
		}

		static private GisCsArc createArc (int last, int j, GisChtArmR2V[] currArms)
		{
			var arc = new GisCsArc ();
			int n = j - last + 1;
			arc.mListOfVertexCorrdinate = new GisObjectR2V[n];
			arc [0] = currArms [last];
			arc [n-1] = currArms [j];
			for (var i = 1; i < n - 1; i++) {
				arc [i] = new GisObjectR2V () {
					x = last + i,
					y = currArms[last].y
				};
			}
			return arc;
		}

		public String Convert(Image img) {
			var bmp = new Bitmap (img);
			var rect = new Rectangle (new Point (0, 0), bmp.Size);
			var bmpData = bmp.LockBits (rect, 
				System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			var data = bmpData.Scan0;
			var row = bmpData.Stride;
			var height = bmpData.Height;
			var width = bmpData.Width;

			var arms = new GisChtArmR2V[height, width];
			var prevArms = new GisChtArmR2V[width];
			var currArms = new GisChtArmR2V[width];
			var joinedArms = new GisChtArmR2V[width];

			for (var i = 1; i < height; i++) { // 2 linie bufor
				var prevLine = data + (i - 1) * row;
				var currLine = data + i * row;
				for (var j = 0; j < width; j++) {
					int cb = currLine [3 * j];
					int cg = currLine [3 * j + 1];
					int cr = currLine [3 * j + 2];
					int c = cb * 256 * 256 + cg * 256 + cr;

					int cb2 = (j < width - 1) ? currLine[3 * (j+1)] : 0;
					int cg2 = (j < width - 1) ? currLine[3 * (j+1)+1] : 0;
					int cr2 = (j < width - 1) ? currLine[3 * (j+1)+2] : 0;
					int c2 = cb2 * 256 * 256 + cg2 * 256 + cr2;

					if (c != c2 || j == 0 || || j == width - 1) {
						currArms [j] = new GisChtArmR2V () {
							x = j,
							y = i,
							mlColPos = j,
							mpAbovePolygon = null,
							mpLeftPolygon = null,
							mpInsidePolygon = null,
							mPixelValue = c,
							mpArcVerticalArm = null,
							mpArcHorizontalArm = null
						};
					}						
				}

				for (var j = 0; j < width; j++) {
					if (currArms[j] == null && prevArms[j] != null) {
						currArms[j] = prevArms[j];
					}
				}

				// Two Arm Chain creating

				var last = 0;

				for (var j = 1; j < width; j++) {
					if (currArms[j] != null) {
						joinedArms[last] = createArc(last, j, currArms);
						last = j;
					}
				}

				currArms = joinedArms;

				// Join with prior arm chain

			}

			bmp.UnlockBits (bmpData);

			return "";
		}

	}
}

