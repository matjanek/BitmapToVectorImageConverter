using System;
using System.Drawing;

namespace BitmapToVectorImageConverter
{
	public class GisConverter
	{
		private GisConverter ()
		{
		}

		static private long arcGlobalSystemIdCounter = 0;

		static private GisCsArc createArc (int last, int j, GisChtArmR2V[] currArms)
		{
			var id = ++arcGlobalSystemIdCounter;
			var arc = new GisCsArc ();
			arc.mArcSystemId = id;
			int n = j - last + 1;
			arc.mListOfVertexCorrdinate = new GisObjectR2V[n];
			arc.mListOfVertexCorrdinate [0] = currArms [last];
			arc.mListOfVertexCorrdinate[n-1] = currArms [j];
			for (var i = 1; i < n - 1; i++) {
				arc.mListOfVertexCorrdinate [i] = new GisObjectR2V () {
					x = last + i,
					y = currArms[last].y
				};
			}
			return arc;
		}

		GisCsArc createHorizontalArc (int j, GisChtArmR2V[] prevArms, GisChtArmR2V[] currArms)
		{
			var id = ++arcGlobalSystemIdCounter;
			var arc = new GisCsArc ();
			arc.mArcSystemId = ++arcGlobalSystemIdCounter;
			arc.mListOfVertexCorrdinate = new GisObjectR2V[2];
			arc.mListOfVertexCorrdinate [0] = prevArms [j];
			arc.mListOfVertexCorrdinate [1] = currArms [j];
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


			for (var i = 1; i < height; i++) { // 2 linie bufor
				var prevLine = data + (i - 1) * row;
				var currLine = data + i * row;
				for (var j = 0; j < width; j++) {
					int c = (currLine + j).ToInt32 ();;
					int c2 = (j < width - 1) ? (currLine + j+1).ToInt32() : 0;

					if (c != c2 || j == 0 || j == width - 1) {
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
						currArms[j].mpArcHorizontalArm = createArc(last, j, currArms);
						last = j;
					}
				}
					
				// Join with prior arm chain

				for (var j = 0; j < width; j++) {
					if (prevArms[j] != null) {
						prevArms [j].mpArcVerticalArm = createHorizontalArc (j, prevArms, currArms);
					}
				}

				// TODO inside/above polygons information passing
					
			}

			bmp.UnlockBits (bmpData);

			return "";
		}

	}
}

