using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            if ((object)p1 == null)
                return (object)p2 == null;

            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point p2 = (Point)obj;
            return (X == p2.X && Y == p2.Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"{X.ToString("F6", CultureInfo.InvariantCulture)}\t" +
                        $"{Y.ToString("F6", CultureInfo.InvariantCulture)}";
        }
    }
}
