using System;
using System.Collections.Generic;
using System.Drawing;

namespace BitmapToVectorImageConverter
{
	public class GisPolygonR2V
	{
		public int mPolygonSystemId { get; set; }
		public List<GisObjectR2V> mLeftListOfArcs { get; set; }
		public List<GisObjectR2V> mRightListOfArcs { get; set; }
        public List<GisObjectR2V> mBorderPoints { get; set; }
		public Color c { get; set; }

		public GisPolygonR2V ()
		{
            mLeftListOfArcs = new List<GisObjectR2V>();
            mRightListOfArcs = new List<GisObjectR2V>();
            mBorderPoints = new List<GisObjectR2V>();
		}
	}
}

