using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Hierarchical_Agglomerative_Clustering;

namespace HAC_Benchmark
{
    class Benchmark
    {
        static void Main(string[] args)
        {
            int[] counts = { 10, 50, 100, 500, 1000};
            int[] dims = { 5, 10, 50, 100};
            RunBenchmark(counts, dims, 5);

        }

        static void RunBenchmark(int[] pointCounts, int[] pointDims, int averageOf, string linkageMethod = "average", string distanceMethod = "euclideansquared", string resultFilename = "benchmark_results.txt")
        {
            string startupMessage = $"Clustering benchmark with parameters: averageOf={averageOf}," +
                $" linkageMethod=\"{linkageMethod}\", distanceMethod=\"{distanceMethod}\"";

            Console.WriteLine(startupMessage);
            DataIO dio = new DataIO();
            HAC hac = new HAC();

            using (StreamWriter file = new StreamWriter(resultFilename))
            {
                file.WriteLine(startupMessage);
                foreach (int count in pointCounts)
                {
                    foreach (int dim in pointDims)
                    {
                        long result = 0;
                        for(int i = 0; i < averageOf; ++i)
                        {
                            Console.WriteLine($"Now clustering: {count}-{dim}-{i}...");
                            Stopwatch watch = new Stopwatch();
                            List<Point> input = dio.GenerateData(-1000000, 1000000, count, dim);
                            watch.Start();
                            hac.ClusterData(input, "average", "euclidean2", 0);
                            watch.Stop();
                            result += watch.ElapsedMilliseconds;
                        }
                        result /= averageOf;
                        file.WriteLine($"{count} points, {dim} dimensions = {result} milliseconds");
                    }
                }
            }
            Console.WriteLine("Benchmark finished successfully");
        }
    }
}
