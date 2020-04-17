using System;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DataIO dio = new DataIO();
            HAC hac = new HAC();

            //List<Point> testList = new List<Point>();
            //testList = dio.LoadData(@"input.txt");
            //dio.SaveData(testList);

            hac.ClusterData(dio.GenerateData());
        }
    }
}
