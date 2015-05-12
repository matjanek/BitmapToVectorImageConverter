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
			var doc = new SvgDocument () {
				Width = 300,
				Height = 300
			};
			var circle = new SvgCircle () {
				StrokeWidth = 2,
				Radius = 100,
				Fill = new SvgColourServer(Color.Red),
				Stroke = new SvgColourServer(Color.Black),
				CenterX = 100,
				CenterY = 100
			};

			var poly = new SvgPolygon () {
				StrokeWidth = 20,
				Fill = new SvgColourServer(Color.Red),
				Stroke = new SvgColourServer(Color.Black),
			};


			poly.Points = new SvgPointCollection ();
			poly.Points.Add (new SvgUnit (100));
			poly.Points.Add (new SvgUnit (100));

			poly.Points.Add (new SvgUnit (200));
			poly.Points.Add (new SvgUnit (100));

			poly.Points.Add (new SvgUnit (200));
			poly.Points.Add (new SvgUnit (200));

			poly.Points.Add (new SvgUnit (100));
			poly.Points.Add (new SvgUnit (200));

			var g = new SvgGroup ();

			g.Children.Add (circle);
			g.Children.Add (poly);
			doc.Children.Add (g);
			doc.Write (path);
		}
	}
}

	