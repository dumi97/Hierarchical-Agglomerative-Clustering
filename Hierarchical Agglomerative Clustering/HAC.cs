using System;
using System.Collections.Generic;
using static Hierarchical_Agglomerative_Clustering.Utils;
using ShellProgressBar;

namespace Hierarchical_Agglomerative_Clustering
{
    public class HAC
    {
        private List<Point> Points { get; set; }
        private List<Cluster> CurrentClusters { get; set; }
        private List<Cluster> FinalClusters { get; set; }
        private Dictionary<Cluster, Dictionary<Cluster, double>> distanceMatrix;

        public List<Cluster> ClusterData(List<Point> data, string linkageMethod = "average", string distanceMethod = "euclidean2", int verbosity = 1)
        {
            distanceMatrix = new Dictionary<Cluster, Dictionary<Cluster, double>>();
            Points = data;
            CurrentClusters = new List<Cluster>();
            FinalClusters = new List<Cluster>();

            foreach (Point p in Points)
                CurrentClusters.Add(new Cluster(p));

            // variant with writing to console
            if (verbosity > 0)
            {
                // progress bar
                int totalTicks = CurrentClusters.Count - 1;
                var options = new ProgressBarOptions
                {
                    ForegroundColor = ConsoleColor.Gray,
                    ForegroundColorDone = ConsoleColor.Gray,
                    BackgroundColor = ConsoleColor.DarkGray,
                    BackgroundCharacter = '\u2593',
                    ProgressBarOnBottom = true
                };
                Console.WriteLine('.');
                Console.WriteLine('.');
                using (var pbar = new ProgressBar(totalTicks, "Clustering progress", options))
                {
                    while (CurrentClusters.Count > 1)
                    {
                        MergeClusters(linkageMethod, distanceMethod);
                        pbar.Tick();
                    }
                }
            }
            // variant without writing to console
            else
                while (CurrentClusters.Count > 1)
                    MergeClusters(linkageMethod, distanceMethod);

            FinalClusters.Add(CurrentClusters[0]);

            return FinalClusters;
        }

        private void MergeClusters(string linkageMethod, string distanceMethod)
        {
            double minDist = double.MaxValue;
            Cluster minClust1 = null, minClust2 = null;
            bool buildInitialMatrix = false;

            if(distanceMatrix.Count == 0)
                buildInitialMatrix = true;

            for(int i = 0; i < CurrentClusters.Count; ++i)
            {
                Cluster c1 = CurrentClusters[i];

                if(buildInitialMatrix)
                    distanceMatrix[c1] = new Dictionary<Cluster, double>();

                for (int j = i+1; j < CurrentClusters.Count; ++j)
                {
                    Cluster c2 = CurrentClusters[j];
                    double dist;

                    if (buildInitialMatrix)
                        dist = distanceMatrix[c1][c2] = ClusterDistance(CurrentClusters[i], CurrentClusters[j], linkageMethod, distanceMethod);
                    else
                        dist = distanceMatrix[CurrentClusters[i]][CurrentClusters[j]];

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

            Cluster newCluster = new Cluster(minClust1, minClust2);

            FinalClusters.Add(minClust1);
            FinalClusters.Add(minClust2);
            
            CurrentClusters.Remove(minClust1);
            CurrentClusters.Remove(minClust2);

            // update distance matrix and add new cluster to current clusters
            distanceMatrix.Remove(minClust1);
            distanceMatrix.Remove(minClust2);

            foreach(Cluster c in CurrentClusters)
                distanceMatrix[c][newCluster] = ClusterDistance(c, newCluster, linkageMethod, distanceMethod);

            distanceMatrix[newCluster] = new Dictionary<Cluster, double>();
            CurrentClusters.Add(newCluster);
        }
    }
}
