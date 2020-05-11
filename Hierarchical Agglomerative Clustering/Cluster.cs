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

        public static bool operator ==(Cluster c1, Cluster c2)
        {
            if (c1 is null)
                return c2 is null;

            return c1.Equals(c2);
        }

        public static bool operator !=(Cluster c1, Cluster c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Cluster c2 = (Cluster)obj;
            List<Point> points2 = c2.Points;

            if (Points.Count != points2.Count)
                return false;

            foreach (Point p in Points)
                if (!points2.Contains(p))
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Points);
        }
    }
}
