using System;
using System.Collections.Generic;
using static Hierarchical_Agglomerative_Clustering.Utils;

namespace Hierarchical_Agglomerative_Clustering
{
    public class HAC
    {
        private List<Point> Points { get; set; }
        private List<Cluster> CurrentClusters { get; set; }
        private List<Cluster> FinalClusters { get; set; }

        public List<Cluster> ClusterData(List<Point> data, string linkageMethod = "average", string distanceMethod = "euclidean2")
        {
            Points = data;
            CurrentClusters = new List<Cluster>();
            FinalClusters = new List<Cluster>();

            foreach (Point p in Points)
                CurrentClusters.Add(new Cluster(p));

            while (CurrentClusters.Count > 1)
                MergeClusters(linkageMethod, distanceMethod);

            FinalClusters.Add(CurrentClusters[0]);

            return FinalClusters;
        }

        private void MergeClusters(string linkageMethod, string distanceMethod)
        {
            double minDist = double.MaxValue;
            Cluster minClust1 = null, minClust2 = null;

            for(int i = 0; i < CurrentClusters.Count; ++i)
            {
                for(int j = i+1; j < CurrentClusters.Count; ++j)
                {
                    double dist = ClusterDistance(CurrentClusters[i], CurrentClusters[j], linkageMethod, distanceMethod);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minClust1 = CurrentClusters[i];
                        minClust2 = CurrentClusters[j];
                    }     
                }
            }

            if (minClust1 == null || minClust2 == null)
            {
                Console.WriteLine("[WARNING] Could not find clusters to merge");
                return;
            }

            FinalClusters.Add(minClust1);
            FinalClusters.Add(minClust2);
            CurrentClusters.Add(new Cluster(minClust1, minClust2));
            CurrentClusters.Remove(minClust1);
            CurrentClusters.Remove(minClust2);
        }
    }
}
