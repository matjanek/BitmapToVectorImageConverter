using System;
using System.Collections.Generic;
using Svg;
using System.Drawing;

namespace BitmapToVectorImageConverter
{
	public class SvgTools
	{
		private SvgTools ()
		{
		}

		static public void save(List<GisPolygonR2V> polygons, string path) {
			var doc = new SvgDocument ();

			foreach (var polygon in polygons) {
				var poly = new SvgPolygon ();
				poly.Points = new SvgPointCollection ();
				var points = polygon.mBorderPoints;
				// FIXME czy może połączyć left i right skiny?

				foreach (var point in points) {
					poly.Points.Add (new SvgUnit (point.X));
					poly.Points.Add (new SvgUnit (point.Y));
				}

				doc.Children.Add (poly);

			}
	
			doc.Write (path);
		}
	}
}
	