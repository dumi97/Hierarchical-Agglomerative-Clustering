using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public class HAC
    {
        public List<Point> Points { get; private set; }

        public List<Point> ClusterData(List<Point> data)
        {
            Points = data;

            return Points;
        }

        public List<Point> GetLastClusteredData()
        {
            return Points;
        }
    }
}
