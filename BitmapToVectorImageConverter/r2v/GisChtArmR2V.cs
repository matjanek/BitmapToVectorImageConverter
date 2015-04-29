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
		public GisCsArc mpArcHorizontalArm { get; set; }
		public GisChtArmR2V ()
		{
		}
	}
}

