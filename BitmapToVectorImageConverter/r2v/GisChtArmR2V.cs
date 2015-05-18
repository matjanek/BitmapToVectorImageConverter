using System;

namespace BitmapToVectorImageConverter
{
	public class GisChtArmR2V : GisObjectR2V
	{
		public long mPixelValue { get; set; }
		public long mlColPos { get; set; }
		public GisPolygonR2V mpInsidePolygon { get; set; }
		public GisPolygonR2V mpAbovePolygon { get; set; }
		public GisPolygonR2V mpLeftPolygon { get; set; }
		public GisCsArc mpArcVerticalArm { get; set; }
		public bool mpArmVerticalVirtual { get; set; }
		public GisCsArc mpArcHorizontalArm { get; set; }
		public bool mpArmHorizontalVirtual { get; set; }
		public GisChtArmR2V ()
		{	
		}

        public GisChtArmR2V(bool verticalVirtual, bool horizontalVirtual, int i, int j, int c)
        {
            X = j;
            Y = i;
            mpAbovePolygon = null;
            mlColPos = j;
            mpLeftPolygon = null;
            mpArmVerticalVirtual = verticalVirtual;
            mpArmHorizontalVirtual = horizontalVirtual;
            mpInsidePolygon = (!verticalVirtual && !horizontalVirtual)? new GisPolygonR2V() : null;
            mPixelValue = c;
            mpArcVerticalArm = new GisCsArc();
            mpArcHorizontalArm = new GisCsArc();
        }
	}
}

