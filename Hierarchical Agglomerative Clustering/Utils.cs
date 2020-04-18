using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public static class Utils
    {
        public static double Distance(Point p1, Point p2, string method = "euclidean2")
        {
            method = method.Replace(" ", string.Empty).ToLower();

            if (method.Equals("manhattan"))
                return DistanceManhattan(p1, p2);
            else if (method.Equals("euclidean"))
                return DistanceEuclidean(p1, p2);

            return DistanceEuclideanSquared(p1, p2);
        }

        private static double DistanceEuclidean(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static double DistanceEuclideanSquared(Point p1, Point p2)
        {
            return Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2);
        }

        private static double DistanceManhattan(Point p1, Point p2)
        {
            return Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
        }

        public static double ClusterDistance(Cluster c1, Cluster c2, string linkageMethod = "average", string distanceMethod = "euclidean2")
        {
            linkageMethod = linkageMethod.Replace(" ", string.Empty).ToLower();

            if (linkageMethod.Equals("minimum"))
                return ClusterDistanceMinimum(c1, c2, distanceMethod);
            else if (linkageMethod.Equals("maximum"))
                return ClusterDistanceMaximum(c1, c2, distanceMethod);

            return ClusterDistanceAverage(c1, c2, distanceMethod);
        }

        private static double ClusterDistanceMinimum(Cluster c1, Cluster c2, string distanceMethod)
        {
            double minimum = double.MaxValue;

            foreach (Point p1 in c1.Points)
            {
                foreach(Point p2 in c2.Points)
                {
                    double dist = Distance(p1, p2, distanceMethod);
                    if (dist < minimum)
                        minimum = dist;
                }
            }

            return minimum;
        }

        private static double ClusterDistanceMaximum(Cluster c1, Cluster c2, string distanceMethod)
        {
            double maximum = 0;

            foreach (Point p1 in c1.Points)
            {
                foreach (Point p2 in c2.Points)
                {
                    double dist = Distance(p1, p2, distanceMethod);
                    if (dist > maximum)
                        maximum = dist;
                }
            }

            return maximum;
        }

        private static double ClusterDistanceAverage(Cluster c1, Cluster c2, string distanceMethod)
        {
            double average = 0;

            foreach (Point p1 in c1.Points)
                foreach (Point p2 in c2.Points)
                    average += Distance(p1, p2, distanceMethod);

            average /= (c1.Size * c2.Size);

            return average;
        }
        public static void ShowHelp(Mono.Options.OptionSet p)
        {
            Console.WriteLine("Usage: hac.exe [OPTIONS]");
            Console.WriteLine("Perform Hierarchical Agglomerative Clustering on a given set of data.");
            Console.WriteLine("The data consists of pairs of coordinates (X and Y) of points in 2D space.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
