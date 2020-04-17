using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    class HAC
    {
        public List<Point> Points { get; set; }

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
