using System;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Cluster> outputList;
            DataIO dio = new DataIO();
            HAC hac = new HAC();

            //List<Point> testList = new List<Point>();
            //testList = dio.LoadData(@"input.txt");
            //dio.SaveData(testList);

            outputList = hac.ClusterData(dio.GenerateData(count:4,fileName:"generated.txt"));
            dio.SaveData(outputList);
        }
    }
}
