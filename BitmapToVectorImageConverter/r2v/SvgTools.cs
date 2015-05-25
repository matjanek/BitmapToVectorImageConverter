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

			var g = new SvgGroup ();

			Console.WriteLine ("Liczba wielokatow: {0}", polygons.Count);

			foreach (var polygon in polygons) {
				Console.WriteLine ("Wielokat: {0}, dl: {1}", polygon.c, polygon.mBorderPoints.Count);
				var poly = new SvgPolygon () {
					Stroke = new SvgColourServer(Color.Red),
					Fill = new SvgColourServer(Color.Black),
					StrokeWidth = 1
				};

				poly.Points = new SvgPointCollection ();
				var points = polygon.mBorderPoints;
				// FIXME czy może połączyć left i right skiny?

				foreach (var point in points) {
					Console.WriteLine ("Punkty: ({0},{1})", point.X, point.Y);
					poly.Points.Add (new SvgUnit (point.X));
					poly.Points.Add (new SvgUnit (point.Y));
					doc.Width  = Math.Max (doc.Width, point.X);
					doc.Height = Math.Max (doc.Height, point.Y);
				}

				g.Children.Add (poly);

			}
			doc.Children.Add (g);
			doc.Write (path);
		}
	}
}
	
