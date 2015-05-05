using System;
using System.Collections.Generic;

namespace BitmapToVectorImageConverter
{
	public class GisCsArc
	{
		public long mArcSystemId { get; set; }
		public List<GisObjectR2V> mListOfVertexCorrdinate { get; set; }
		public GisCsArc ()
		{
		}
	}
}

