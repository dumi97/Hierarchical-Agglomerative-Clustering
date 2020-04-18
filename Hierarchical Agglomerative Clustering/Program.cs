using System;
using System.Collections.Generic;
using Mono.Options;

namespace Hierarchical_Agglomerative_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showHelp = true;
            string linkageMethod = "average", distanceMethod = "euclidean2";
            bool generateData = false;
            string generatedFileName = "";
            double minGen = 0, maxGen = 100;
            int genCount = 6;
            string inputFile = "input.txt";
            string outputFile = "output.txt";

            var p = new OptionSet() {
                { "l|linkage=", "(string) the linkage method of the algorithm\n" +
                    "possible valuse: minimum, maximum, average\n" +
                    "default: average",
                    (string v) => linkageMethod = v },
                { "d|distance=", "(string) the distance method of the algorithm\n" +
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
                { "i|input=", "(string) the input file\n" +
                    "default: input.txt",
                    (string v) => inputFile = v },
                { "o|output=", "(string) the output file\n" +
                    "default: output.txt",
                    (string v) => outputFile = v },
                { "h|help",  "show this message and exit",
                    v => showHelp = v != null },
            };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Hierarchical Agglomerative Clustering: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'hac.exe --help' for more information.");
                return;
            }

            if(showHelp)
            {
                //TODO
                return;
            }

            List<Cluster> outputList;
            DataIO dio = new DataIO();
            HAC hac = new HAC();

            // load data
            List<Point> input = null;
            if (generateData)
                input = dio.GenerateData(minGen, maxGen, genCount, fileName: generatedFileName);
            else
                input = dio.LoadData(inputFile);

            // cluster data
            outputList = hac.ClusterData(input, linkageMethod, distanceMethod);

            // output data
            dio.SaveData(outputList, outputFile);
        }
    }
}
