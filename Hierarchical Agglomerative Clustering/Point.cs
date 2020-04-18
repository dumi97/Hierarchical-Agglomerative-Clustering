using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Cluster { get; set; }

        public Point(double x = 0, double y = 0, int cluster = -1)
        {
            X = x;
            Y = y;
            Cluster = cluster;
        }
    }
}
