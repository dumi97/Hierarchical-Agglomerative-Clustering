using System;
using System.Collections.Generic;
using Mono.Options;

namespace Hierarchical_Agglomerative_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            // cmd argument variables
            bool showHelp = false;
            string linkageMethod = "average", distanceMethod = "euclidean2";
            bool generateData = false;
            string generatedFileName = "";
            double minGen = 0, maxGen = 100;
            int genCount = 6, genDims = 4;
            string inputFile = "input.txt";
            string outputFile = "output.txt";
            bool pause = false;

            var p = new OptionSet() {
                { "l|linkage=", "(string) the linkage method of the algorithm, which is the criteria of joining two clusters\n" +
                    "possible valuse: minimum, maximum, average\n" +
                    "default: average",
                    (string v) => linkageMethod = v },
                { "d|distance=", "(string) the method of calculating distance between two points in the algorithm\n" +
                    "possible valuse: euclidean, euclidean2 (or euclideansquared), manhattan\n" +
                    "default: euclidean2",
                    (string v) => distanceMethod = v },
                { "g|generate", "generate input data",
                    v => generateData = v != null },
                { "f|generatedName=", "(string) name of the file to save the generated input data\n" +
                    "empty string means no file will be generated\n" +
                    "default: <empty>",
                    (string v) => generatedFileName = v },
                { "n|minGenerated=", "(double) minimum value of the generated data\n" +
                    "default: 0.0",
                    (double v) => minGen = v },
                { "m|maxGenerated=", "(double) maximum value of the generated data\n" +
                    "default: 100.0",
                    (double v) => maxGen = v },
                { "c|generatedCount=", "(int) how many data point to generate\n" +
                    "default: 6",
                    (int v) => genCount = v },
                { "e|generatedDimensions=", "(int) how many dimensions should the generated point have\n" +
                    "default: 4",
                    (int v) => genDims = v },
                { "i|input=", "(string) the input file\n" +
                    "default: input.txt",
                    (string v) => inputFile = v },
                { "o|output=", "(string) the output file\n" +
                    "type \"\" for no output file\n" +
                    "default: output.txt",
                    (string v) => outputFile = v },
                { "p|pause",  "pause when application finished",
                    v => pause = v != null },
                { "h|help",  "show this message and exit",
                    v => showHelp = v != null },
            };

            // parse and apply cmd arguments
            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Hierarchical Agglomerative Clustering: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'hac.exe -h' for more information.");
                return;
            }

            // handle unknown arguments
            if(extra.Count != 0)
            {
                foreach (string s in extra)
                    Console.WriteLine($"[WARNING] Unknown argument: {s}");
                Console.WriteLine($"Try 'hac.exe -h' for a full list of arguments.");
            }

            if(showHelp)
            {
                Utils.ShowHelp(p);
                return;
            }

            List<Cluster> outputList;
            DataIO dio = new DataIO();
            HAC hac = new HAC();

            // load data
            List<Point> input = null;
            if (generateData)
            {
                Console.WriteLine("Generating data...");
                input = dio.GenerateData(minGen, maxGen, genCount, genDims, generatedFileName);
            }
            else
            {
                Console.WriteLine($"Loading data from {inputFile}...");
                input = dio.LoadData(inputFile);
                if (input.Count == 0)
                    return;
            }

            // cluster data
            Console.WriteLine("Performing Hierarchical Agglomerative Clustering...");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            outputList = hac.ClusterData(input, linkageMethod, distanceMethod);
            watch.Stop();
            Console.WriteLine($"Done in {watch.ElapsedMilliseconds} milliseconds.");


            // output data
            if(!outputFile.Equals(""))
            {
                Console.WriteLine($"Saving output to {outputFile}...");
                dio.SaveData(outputList, outputFile);
            }
            Console.WriteLine("Finished succesfully.");

            if(pause)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
        }
    }
}
