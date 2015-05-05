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

		static private void connectTwoArcs (int prev, int next, 
			GisChtArmR2V[] prevArms, GisChtArmR2V[] currArms)
		{
			var nextArm = currArms  [next];
			var prevArm = currArms  [prev];
			var upperArm = prevArms [next];
			bool upperSolid = !prevArm.mpArmVerticalVirtual;
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
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
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
				break;
			case 7:
				break;
			case 8:
				break;
			case 9:
				break;
			case 10:
				break;
			case 11:
				break;
			case 12:
				break;
			case 13:
				break;
			case 14:
				break;
			case 15:
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
				mlColPos = j,
				mpAbovePolygon = new GisPolygonR2V(),
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

		static GisChtArmR2V smartCopy (GisChtArmR2V gisChtArmR2V)
		{
			return gisChtArmR2V;
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
			var prevArms = new GisChtArmR2V[width];
			var currArms = new GisChtArmR2V[width];

			Byte[] data = new Byte[height*bmpData.Stride];
			Marshal.Copy (bmpData.Scan0, data, 0, height * bmpData.Stride);
			int stride = bmpData.Stride;

			Console.Error.WriteLine ("Stride: {0}", stride);

			for (var i = 0; i < height; i++) { // 2 linie bufor
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
						currArms[j] = createNode (i, j, c);
						currArms[j].mpArmHorizontalVirtual = c != c2;
						currArms[j].mpArmVerticalVirtual = c != c3;
					}						
				}

				int c0 = getColor (data, i * row);
				int c0p = getColor (data, (i-1) * row);
				int c0n = getColor (data, i * row + 3);

				currArms [0] = createNode (i, 0, c0);
				currArms[0].mpArmHorizontalVirtual = c0 != c0n;
				currArms[0].mpArmVerticalVirtual = c0 != c0p;

				int cl = getColor (data, i * row + 3*(width-1));
				int clp = getColor (data, (i-1) * row + 3*(width-1));
				currArms [width-1] = createNode (i, width-1, getColor(data, 3*(width-1)));
				currArms [width - 1].mpArmHorizontalVirtual = true; // tak zakładamy, że poza to różne
				currArms [width - 1].mpArmVerticalVirtual = cl != clp;
				// na początku i na końcu zawsze musi być

				// dziedziczymy z poprzedniej linii
				for (var j = 0; j < width; j++) {
					if (currArms[j] == null && prevArms[j] != null) {
						currArms [j] = prevArms [j];
					}
				}

				// Two Arm Chain creating

				var last = 0;

				for (var j = 1; j < width; j++) {
					if (currArms[j] != null) {
						connectTwoArcs(last, j, prevArms, currArms);
						last = j;
					}
				}
					
				// Join with prior arm chain

				for (var j = 0; j < width; j++) {
					if (prevArms[j] != null) {
						connectHorizontalArc (j, prevArms, currArms);
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

