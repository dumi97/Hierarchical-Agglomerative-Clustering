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

        public Cluster()
        {
            Points = new List<Point>();
        }

        public Cluster(Cluster c1, Cluster c2)
        {
            Points = new List<Point>();

            foreach (Point p in c1.Points)
                Points.Add(new Point(p));

            foreach (Point p in c2.Points)
                Points.Add(new Point(p));
        }

        public Cluster(List<Point> pl)
        {
            Points = pl;
        }

        public Cluster (Point[] ap)
        {
            Points = new List<Point>(ap);
        }

        public Cluster(Point p)
        {
            Points = new List<Point> { p };
        }
    }
}
