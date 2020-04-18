using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    class Cluster
    {
        public List<Point> Points { get; private set; }

        public int Size
        {
            get => Points.Count;
        }
    }
}
