using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace BitmapToVectorImageConverter
{
	public class GisConverter
	{
		private GisConverter ()
		{
		}

		static private long arcGlobalSystemIdCounter = 0;

		static private void connectTwoArcs (int i, int prev, int next, 
			GisChtArmR2V[,] arms)
		{
			var nextArm = arms  [i,next];
			var prevArm = arms  [i, prev];
			var upperArm = arms [i-1,next];

			if (upperArm == null) {
				upperArm = prevArm;
			}

			bool upperSolid = upperArm != null ? !upperArm.mpArmVerticalVirtual : false;
			bool bottomSolid = !nextArm.mpArmVerticalVirtual;
			bool rightSolid = !nextArm.mpArmHorizontalVirtual;
			bool leftSolid = !prevArm.mpArmHorizontalVirtual;

			// 16 przypadków lączenia

			int caseNr = 0;
			if (!upperSolid) {
				caseNr += 1;
			}

			if (!leftSolid) {
				caseNr += 2;
			}

			if (!rightSolid) {
				caseNr += 4;
			}

			if (!bottomSolid) {
				caseNr += 8;
			}

			switch (caseNr) {
			case 0:
				nextArm.mpAbovePolygon = upperArm.mpInsidePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				// nextArm.mpInsidePolygon = swój własny
				break;
			case 1:
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				// nextArm.mpInsidePolygon = swój własny
				break;
			case 2:
				nextArm.mpAbovePolygon = upperArm.mpInsidePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				// nextArm.mpInsidePolygon = swój własny
				break;
			case 3:
				nextArm.mpAbovePolygon = prevArm.mpInsidePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				// nextArm.mpInsidePolygon = swój własny
				break;
			case 4: // prawy
				nextArm.mpAbovePolygon = upperArm.mpAbovePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				nextArm.mpInsidePolygon = upperArm.mpInsidePolygon;
				break;
			case 5: // lewy	
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				nextArm.mpInsidePolygon = prevArm.mpAbovePolygon;
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				break;
			case 6:
				nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
				nextArm.mpInsidePolygon = upperArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = upperArm.mpAbovePolygon;
				break;
			case 7:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = upperArm.mpAbovePolygon;
				break;
			case 8:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = upperArm.mpAbovePolygon;
				break;
			case 9:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				break;
			case 10:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = upperArm.mpInsidePolygon;
				break;
			case 11:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = prevArm.mpInsidePolygon;
				break;
			case 12:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				break;
			case 13:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = upperArm.mpAbovePolygon;
				break;
			case 14:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				break;
			case 15:
				nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
				nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
				nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
				break;
			default:
				throw new NotSupportedException ("There should not be such a case");
			}

		}

		static void connectHorizontalArc (int j, GisChtArmR2V[] prevArms, GisChtArmR2V[] currArms)
		{
			var id = ++arcGlobalSystemIdCounter;
			var arc = new GisCsArc ();
			arc.mArcSystemId = ++arcGlobalSystemIdCounter;
			arc.mListOfVertexCorrdinate = new List<GisObjectR2V> ();
			arc.mListOfVertexCorrdinate.Add (prevArms [j]);
			arc.mListOfVertexCorrdinate.Add (currArms [j]);

		}

		static GisChtArmR2V createNode (int i, int j, int c)
		{
			return new GisChtArmR2V () {
				x = j,
				y = i,
				mpAbovePolygon = new GisPolygonR2V(),
				mlColPos = j,
				mpLeftPolygon = new GisPolygonR2V(),
				mpInsidePolygon = new GisPolygonR2V(),
				mPixelValue = c,
				mpArcVerticalArm = new GisCsArc(),
				mpArcHorizontalArm = new GisCsArc()
			};
		}

		static int getColor (byte[] data, int currIdx)
		{
			byte b = data [currIdx];
			byte g = data [currIdx + 1];
			byte r = data [currIdx + 2];
			int c = ((int)b << 16) | ((int)g << 8) | (int)r;
			return c;
		}

		static GisChtArmR2V smartCopy (GisChtArmR2V item)
		{
			var result = new GisChtArmR2V ();
			result.mlColPos = item.mlColPos;
			result.mPixelValue = item.mPixelValue;
			result.x = item.x;
			result.y = item.y;
			result.mpArmHorizontalVirtual = false;
			result.mpArmVerticalVirtual = true;
			return result;
		}

		static public String Convert(Image img) {
			Console.Error.WriteLine ("Konwertuje: {0}x{1}", img.Width, img.Height);
			var bmp = new Bitmap (img);
			var rect = new Rectangle (new Point (0, 0), bmp.Size);
			var bmpData = bmp.LockBits (rect, 
				System.Drawing.Imaging.ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			var row = bmpData.Stride;
			var height = bmpData.Height;
			var width = bmpData.Width;
			Console.WriteLine ("PixelFormat: {0}", bmpData.PixelFormat);

			var arms = new GisChtArmR2V[height, width];

			Byte[] data = new Byte[height*bmpData.Stride];
			Marshal.Copy (bmpData.Scan0, data, 0, height * bmpData.Stride);
			int stride = bmpData.Stride;

			// dla 1 linii całość wypełniona
			// wykrywanie różnic w 1. linii, konieczne dla 2. linii (w prevArms)

			for (var j = 0; j < width; j++) {

				int currIdx = 3*j;
				int nextIdx = currIdx + 3;
				var c = getColor (data, currIdx);
				var c2 = getColor (data, nextIdx);

				if (c != c2 || j == 0 || j == width - 1) {
					arms [0,j] = createNode (0, j, getColor (data, 3 * j));
					arms [0, j].mpLeftPolygon = null;
					arms [0, j].mpAbovePolygon = null;
					arms [0, j].mpArmVerticalVirtual = false;
					arms [0, j].mpArmHorizontalVirtual = false;
				}
			}

			arms[0,width-1].mpArmHorizontalVirtual = true;

			// dane o 1. linii dla 2. linii uzupełnione

			Console.Error.WriteLine ("Stride: {0}", stride);

			for (var i = 1; i < height; i++) { // 2 linie bufor
				for (var j = 1; j < width-1; j++) {
					int currIdx = i * stride + 3 * j;
					int bottomIdx = i * stride + 3 * j - row;
					int nextIdx = currIdx + 3;
					var c = getColor (data, currIdx);
					var c2 = getColor (data, nextIdx);
					var c3 = getColor (data, bottomIdx);

					if (c != c2) {
							Console.WriteLine ("Linia: {0}, Kolumna: {1}, C: {2}, C2: {3}",
								i, j, c, c2);

						// tworzymy instancję "ramion"
						arms[i,j] = createNode (i, j, c);
						arms[i,j].mpArmHorizontalVirtual = c == c3;
						arms[i,j].mpArmVerticalVirtual = false;
					}						
				}
					
				// ostatnia i przedostatnia kolumna

				int c0 = getColor (data, i * row);
				int c0p = getColor (data, (i-1) * row);

				arms [i,0] = createNode (i, 0, c0);
				arms [i,0].mpArmHorizontalVirtual = c0 != c0p;
				arms [i,0].mpArmVerticalVirtual = false;

				arms [i,width-1] = createNode (i, width-1, getColor(data, 3*(width-1)));
				arms [i,width - 1].mpArmHorizontalVirtual = true;
				arms [i,width - 1].mpArmVerticalVirtual = false;

				// na początku i na końcu zawsze musi być

				var copyCurrArms = new GisChtArmR2V[width];

				// dziedziczymy z poprzedniej linii
				for (var j = 0; j < width; j++) {
					if (arms[i,j] == null && arms[i-1,j] != null) {
						arms [i,j] = smartCopy(arms[i-1,j]);
					}
				}

				// Two Arm Chain creating

				var last = 0;

				for (var j = 1; j < width; j++) {
					if (arms[i,j] != null) {
						connectTwoArcs(i, last, j, arms);
						last = j;
					}
				}
					
				// Join with prior arm chain

				for (var j = 0; j < width; j++) {
					if (arms[i,j] != null) {

						// połącz z prevArms[j]
						// jeśli nie ma utwórz z false horizontal

						if (arms[i-1,j] == null) {
							arms[i-1,j] = createNode (i, j, getColor (data, i * row + 3 * j));
							arms[i-1,j].mpArmVerticalVirtual = true;
							arms[i-1,j].mpArmHorizontalVirtual = false;
							// ale jeszcze trzeba je potem połączyć

							// szukamy na lewo i na prawo sąsiada

							int idx = j-1;

							while (idx >= 0 && arms[i-1,idx] == null) {
								idx--;
							}

							int left = idx;

							// łączymy lewy z środkiem
							// środek z prawym, albo nawet nie potrzebujemy
							// bo jeśli jest połączony to już dobrze będzie,
							// gdyż lewy to j praktycznie

							if (left >= 0) {
								arms [i - 1, j].mpLeftPolygon = arms [i, left].mpLeftPolygon;
								arms [i - 1, j].mpInsidePolygon = arms [i, left].mpInsidePolygon;
								arms [i - 1, j].mpAbovePolygon = arms [i, left].mpAbovePolygon;
							}

						}

					}
				}
					
					
			}

			Marshal.Copy (data, 0, bmpData.Scan0, height * row);

			bmp.UnlockBits (bmpData);

			bmp.Save ("temp.png");

			return "";
		}

	}
}

