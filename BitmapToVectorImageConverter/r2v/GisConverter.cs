using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace BitmapToVectorImageConverter
{
    public class GisConverter
    {
        private const int BPP = 4;
        private GisConverter()
        {
        }

        static private long arcGlobalSystemIdCounter = 0;

        static private void connectTwoArcs(int i, int prev, int next,
            GisChtArmR2V[,] arms)
        {
            var nextArm = arms[i, next];
            var prevArm = arms[i, prev];
            var upperArm = i > 0 ? arms[i - 1, next] : null;

            if (upperArm == null)
            {
          //      upperArm = prevArm; // to chyba jest błąd (nie zawsze, ale są przypadki, gdzie wprowadza błąd)
            }

            bool upperSolid = upperArm != null ? !upperArm.mpArmVerticalVirtual : false;
            bool bottomSolid = !nextArm.mpArmVerticalVirtual;
            bool rightSolid = !nextArm.mpArmHorizontalVirtual;
            bool leftSolid = !prevArm.mpArmHorizontalVirtual;

            // 16 przypadków lączenia

            int caseNr = 0;
            if (!upperSolid)
            {
                caseNr += 1;
            }

            if (!leftSolid)
            {
                caseNr += 2;
            }

            if (!rightSolid)
            {
                caseNr += 4;
            }

            if (!bottomSolid)
            {
                caseNr += 8;
            }

            switch (caseNr)
            {
                case 0:
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Inside);
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    // nextArm.mpInsidePolygon = swój własny
                    break;
                case 1:
                    nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    // nextArm.mpInsidePolygon = swój własny
                    break;
                case 2:
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Inside);
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    // nextArm.mpInsidePolygon = swój własny
                    break;
                case 3:
                    nextArm.mpAbovePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    // nextArm.mpInsidePolygon = swój własny
                    break;
                case 4: // prawy
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above);
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    nextArm.mpInsidePolygon = SafeGetPolygon(upperArm, PolyLocation.Inside);
                    break;
                case 5: // lewy	
                    nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
                    nextArm.mpInsidePolygon = prevArm.mpAbovePolygon;
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    break;
                case 6:
                    nextArm.mpLeftPolygon = prevArm.mpInsidePolygon;
                    nextArm.mpInsidePolygon = SafeGetPolygon(upperArm, PolyLocation.Inside);
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above);
                    break;
                case 7:
                    throw new Exception("Unexpected case occurred");
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above);
                    break;
                case 8:
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above);
                    break;
                case 9:
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
                    break;
                case 10:
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Inside);
                    break;
                case 11:
                    throw new Exception("Unexpected case occurred");
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = prevArm.mpInsidePolygon;
                    break;
                case 12:
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above); // zmieniłem z prevarm.mpAbove
                    break;
                case 13:
                    throw new Exception("Unexpected case occurred");
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = SafeGetPolygon(upperArm, PolyLocation.Above);
                    break;
                case 14:
                    throw new Exception("Unexpected case occurred");
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
                    break;
                case 15:
                    throw new Exception("Unexpected case occurred");
                    nextArm.mpLeftPolygon = prevArm.mpLeftPolygon;
                    nextArm.mpInsidePolygon = prevArm.mpInsidePolygon;
                    nextArm.mpAbovePolygon = prevArm.mpAbovePolygon;
                    break;
                default:
                    throw new NotSupportedException("There should not be such a case");
            }

        }

        static void connectHorizontalArc(int j, GisChtArmR2V[] prevArms, GisChtArmR2V[] currArms)
        {
            var id = ++arcGlobalSystemIdCounter;
            var arc = new GisCsArc();
            arc.mArcSystemId = ++arcGlobalSystemIdCounter;
            arc.mListOfVertexCorrdinate = new List<GisObjectR2V>();
            arc.mListOfVertexCorrdinate.Add(prevArms[j]);
            arc.mListOfVertexCorrdinate.Add(currArms[j]);

        }

        static int getColor(byte[] data, int currIdx)
        {
            byte b = data[currIdx];
            byte g = data[currIdx + 1];
            byte r = data[currIdx + 2];
            int c = ((int)b << 16) | ((int)g << 8) | (int)r;
            return c;
        }

        static GisChtArmR2V smartCopy(GisChtArmR2V item, int x, int y)
        {
            var result = new GisChtArmR2V();
            result.mlColPos = item.mlColPos;
            result.mPixelValue = item.mPixelValue; // czy na pewno?
            result.X = x;
            result.Y = y; // chyba nie, jesteśmy rząd niżej
            result.mpArmHorizontalVirtual = false; //TODO: zweryfikować // czy zawsze?
            result.mpArmVerticalVirtual = true;
            return result;
        }

        static public String Convert(Image img)
        {
            Console.Error.WriteLine("Konwertuje: {0}x{1}", img.Width, img.Height);
            var bmp = new Bitmap(img);
            var rect = new Rectangle(new Point(0, 0), bmp.Size);
            var bmpData = bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var height = bmpData.Height;
            var width = bmpData.Width;
            Console.WriteLine("PixelFormat: {0}", bmpData.PixelFormat);

            var arms = new GisChtArmR2V[height+1, width+1];
            int last = 0;
            Byte[] data = new Byte[height * bmpData.Stride];
            Marshal.Copy(bmpData.Scan0, data, 0, height * bmpData.Stride);
            int row = bmpData.Stride;

            // dla 1 linii całość wypełniona
            // wykrywanie różnic w 1. linii, konieczne dla 2. linii (w prevArms)

            for (var j = 0; j < width; j++)
            {
                int currIdx = BPP * j;
                int nextIdx = currIdx + BPP;
                var c = getColor(data, currIdx);
                var c2 = j < width - 1 ? getColor(data, nextIdx) : 0; // dla obrazka 1x1 mamy tutaj index out of range exception

                if (c != c2 || j == 0)
                {
                    arms[0, j] = new GisChtArmR2V(false, false, 0, j, getColor(data, BPP * j));
                }
            }

            arms[0, width] = new GisChtArmR2V(false, true,0, width, 0);

            // krok (e): Add Extra Two-Arm Chains based on prior line
            for (var j = 1; j < width + 1; j++)
            {
                if (arms[0, j] != null)
                {
                    connectTwoArcs(0, last, j, arms);
                    last = j;
                }
            }

            // dane o 1. linii dla 2. linii uzupełnione

            Console.Error.WriteLine("Stride: {0}", bmpData.Stride);

            for (var i = 1; i < height; i++)
            { // 2 linie bufor. Badamy wiersze i-ty oraz (i-1)-wszy
                for (var j = 0; j < width; j++)
                {
                    int currIdx = i * bmpData.Stride + BPP * j; // indeks bajtu, który nas interesuje
                    int bottomIdx = (i - 1) * bmpData.Stride + BPP * j; // który to? chyba powyżej, a nie poniżej?
                    int nextIdx = currIdx + BPP; // bajt na prawo
                    var c = getColor(data, currIdx);
                    var c2 = j < width - 1? getColor(data, nextIdx) : 0;
                    var c3 = getColor(data, bottomIdx);

                    if (c != c2) // jest zmiana koloru, tworzymy ramię
                    {
                        Console.WriteLine("Linia: {0}, Kolumna: {1}, C: {2}, C2: {3}",
                            i, j, c, c2);

                        // tworzymy instancję "ramion"
                        arms[i, j] = new GisChtArmR2V(false, c == c3, i, j, c);    // TODO: tu jest bug przy obrazie 1x2, ramię poziome powinno być wirtualne.               
                    }
                }

                // ostatnia i przedostatnia kolumna // chyba chodzi o pierwszą i ostatnią

                arms[i, width] = new GisChtArmR2V(false, true, i, width, 0); // arms dla (i, j) - piksela


                // na początku i na końcu zawsze musi być

                var copyCurrArms = new GisChtArmR2V[width];

                // dziedziczymy z poprzedniej linii
                for (var j = 0; j < width+1; j++)
                {
                    if (arms[i, j] == null && arms[i - 1, j] != null)
                    {
                        arms[i, j] = smartCopy(arms[i - 1, j], j, i);
                    }
                }

                // Two Arm Chain creating

                last = 0;

                // krok (e): Add Extra Two-Arm Chains based on prior line
                for (var j = 1; j < width+1; j++)
                {
                    if (arms[i, j] != null)
                    {
                        connectTwoArcs(i, last, j, arms);
                        last = j;
                    }
                }

                // Join with prior arm chain

                for (var j = 0; j < width+1; j++)
                {
                    if (arms[i, j] != null)
                    {

                        // połącz z prevArms[j]
                        // jeśli nie ma utwórz z wirtualnym vertical i solid horizontal

                        if (arms[i - 1, j] == null)
                        {
                            int c = j < width ? getColor(data, i * bmpData.Stride + BPP * j) : 0;
                            arms[i - 1, j] = new GisChtArmR2V(true, false, i, j, c);

                            // szukamy na lewo i na prawo sąsiada

                            int idx = j - 1;

                            while (idx >= 0 && arms[i - 1, idx] == null)
                            {
                                idx--;
                            }

                            int left = idx;

                            // łączymy lewy z środkiem
                            // środek z prawym, albo nawet nie potrzebujemy
                            // bo jeśli jest połączony to już dobrze będzie,
                            // gdyż lewy to j praktycznie

                            if (left >= 0)
                            {
                                arms[i - 1, j].mpLeftPolygon = arms[i, left].mpLeftPolygon;
                                arms[i - 1, j].mpInsidePolygon = arms[i, left].mpInsidePolygon;
                                arms[i - 1, j].mpAbovePolygon = arms[i, left].mpAbovePolygon;
                            }

                        }

                    }
                }


            } // main row-by-row loop


            for (var j = 0; j < width+1; j++) {
                arms[height, j] = new GisChtArmR2V(true, false, 0, j, 0);
            }

            arms[height, width].mpArmHorizontalVirtual = true;

            last = 0;

            // krok (e): Add Extra Two-Arm Chains based on prior line
            for (var j = 1; j < width + 1; j++)
            {
                if (arms[height, j] != null)
                {
                    connectTwoArcs(height, last, j, arms);
                    last = j;
                }
            }

            // TODO: jest za mało pionowych arms, powinno być n + 1
            ArmsProcessor processor = new ArmsProcessor(arms);
            processor.Process();

            Marshal.Copy(data, 0, bmpData.Scan0, height * bmpData.Stride);

            bmp.UnlockBits(bmpData);

            bmp.Save("temp.png");

            return "";
        }

        private static GisPolygonR2V SafeGetPolygon(GisChtArmR2V arm, PolyLocation location)
        {
            if (arm == null) { return null; }
            switch(location)
            {
                case PolyLocation.Above:
                    return arm.mpAbovePolygon;
                case PolyLocation.Inside:
                    return arm.mpInsidePolygon;
                case PolyLocation.Left:
                    return arm.mpLeftPolygon;
                default:
                    throw new ArgumentException("unknown location");
            }
        }

        private enum PolyLocation
        {
            Inside, 
            Above,
            Left,
        }
    }
}

