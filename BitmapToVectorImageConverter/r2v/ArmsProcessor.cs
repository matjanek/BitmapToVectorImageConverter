using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapToVectorImageConverter
{
    public class ArmsProcessor
    {
        HashSet<GisPolygonR2V> allPolygons;
        private GisChtArmR2V[,] arms;

        public ArmsProcessor(GisChtArmR2V[,] arms)
        {
            allPolygons = new HashSet<GisPolygonR2V>();
            this.arms = arms;
        }

        public void Process()
        {            
            for (int i = 0; i < arms.GetLength(0); i++)
            {
                for (int j = 0; j < arms.GetLength(1); j++)
                {
                    ProcessArm(i, j);
                }
            }
        }

        private void ProcessArm(int i, int j)
        {
            HashSet<GisPolygonR2V> neighbors = new HashSet<GisPolygonR2V>();
            if(i > 0)
            {
                if (arms[i - 1, j] != null)
                {
                    neighbors.Add(arms[i - 1, j].mpInsidePolygon);
                    neighbors.Add(arms[i - 1, j].mpAbovePolygon); //  co zrobić z polygonami "poza obrazem"? ignorować, dodawać...?
                }
            }
            
            if(j > 0)
            {
                if (arms[i, j - 1] != null)
                {
                    neighbors.Add(arms[i, j - 1].mpInsidePolygon);
                }
            }

            if (arms[i, j] != null)
            {
                neighbors.Add(arms[i, j].mpInsidePolygon);
            }

            foreach(GisPolygonR2V poly in neighbors)
            {
                if (poly == null) { continue; }
                poly.mBorderPoints.Add(new GisObjectR2V(i, j));
                allPolygons.Add(poly); // try adding to the main polygon set
            }
        }
    }
}
