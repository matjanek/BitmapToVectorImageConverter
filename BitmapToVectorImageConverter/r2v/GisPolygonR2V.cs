using System;
using System.Collections.Generic;

namespace BitmapToVectorImageConverter
{
	public class GisPolygonR2V
	{

		public int mPolygonSystemId { get; set; }
		public List<GisObjectR2V> mLeftListOfArcs { get; set; }
		public List<GisObjectR2V> mRightListOfArcs { get; set; }

		public GisPolygonR2V ()
		{
		}
	}
}

