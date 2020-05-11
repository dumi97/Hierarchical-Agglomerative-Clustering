using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Hierarchical_Agglomerative_Clustering
{
    class DataIO
    {
        public List<Point> LoadData(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"[WARNING] File \"{fileName}\" does not exist. Generating random data.");
                return GenerateData();
            }

            List<Point> loadedList = new List<Point>();
            char[] delims = { '\t', '\n', '\r', ' ' };
            string[] coords = File.ReadAllText(fileName)
                .Split(delims, StringSplitOptions.RemoveEmptyEntries);

            if (!int.TryParse(coords[0], out int dimNum))
            {
                Console.WriteLine("[ERROR] Could not read number of dimensions");
                Environment.Exit(1);
            }

            if( (coords.Length - 1) % dimNum != 0)
            {
                Console.WriteLine($"[ERROR] The number of total parameters present in {fileName} does not match the defined number of dimensions");
                Environment.Exit(1);
                return loadedList;
            }

            // create point from coords
            // if number of coords not even - omit last coord
            for (int i = 1; i < coords.Length-1; i+= dimNum)
            {
                List<double> dims = new List<double>();

                for (int j = 0; j < dimNum; ++j)
                {
                    if (!double.TryParse(coords[i+j], NumberStyles.Float, CultureInfo.InvariantCulture, out double d))
                    {
                        Console.WriteLine($"[WARNING] Could not parse dimension at index {i - 1 + j}, assuming 0.0");
                        d = 0;
                    }
                    dims.Add(d);
                }

                Point p = new Point(dims);
                loadedList.Add(p);

                Console.WriteLine(p); // DEBUG
            }

            return loadedList;
        }

        public void SaveData(List<Cluster> data, string fileName = @"output.txt")
        {
            // create new temp list sorted by cluster size
            List<Cluster> tmpList = data.OrderBy(o=>o.Size).ToList();

            using (StreamWriter file = new StreamWriter(fileName))
            {
                int currentSize = 0;
                foreach (Cluster c in tmpList)
                {
                    if(c.Size != currentSize)
                    {
                        currentSize = c.Size;
                        file.WriteLine($"--- {currentSize}");
                    }

                    file.WriteLine("{");
                    foreach (Point p in c.Points)
                        file.WriteLine($"\t{p}");
                    file.WriteLine("}");
                    file.WriteLine();
                }
            }
        }

        public List<Point> GenerateData(double minNumber = 0, double maxNumber = 100, int count = 100, int dimensions = 4, string fileName = "")
        {
            StreamWriter file = null;
            if(!fileName.Equals(""))
                file = new StreamWriter(fileName);

            List<Point> generatedList = new List<Point>();
            Random random = new Random();

            if (file != null)
                file.WriteLine(dimensions);

            for (int i = 0; i < count; ++i)
            {
                List<double> dims = new List<double>();
                for (int j = 0; j < dimensions; ++j)
                    dims.Add(random.NextDouble() * (maxNumber - minNumber) + minNumber);

                Point p = new Point(dims);
                generatedList.Add(p);

                if (file != null)
                    file.WriteLine(p);
            }

            if (file != null)
                file.Close();

            return generatedList;
        }
    }
}
