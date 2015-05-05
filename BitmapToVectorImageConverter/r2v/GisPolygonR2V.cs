using System;

namespace BitmapToVectorImageConverter
{
	public class GisPolygonR2V
	{

		public int mPolygonSystemId { get; set; }
		public GisObjectR2V mLeftListOfArcs { get; set; }
		public GisObjectR2V mRightListOfArcs { get; set; }

		public GisPolygonR2V ()
		{
		}
	}
}

