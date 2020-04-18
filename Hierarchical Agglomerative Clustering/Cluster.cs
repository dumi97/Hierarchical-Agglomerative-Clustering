using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public class Cluster
    {
        public List<Point> Points { get; private set; }

        public int Size
        {
            get => Points.Count;
        }

        public Cluster(List<Point> pl)
        {
            Points = pl;
        }

        public Cluster (Point[] ap)
        {
            Points = new List<Point>(ap);
        }
    }
}
