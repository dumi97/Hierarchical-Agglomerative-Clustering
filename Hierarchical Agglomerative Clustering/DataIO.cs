using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    class DataIO
    {
        public List<Point> LoadData(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"[WARNING] File \"{fileName}\" does not exist. Generating random data.");
                return GenerateData(fileName:@"generatedInput.txt");
            }

            List<Point> loadedList = new List<Point>();
            char[] delims = { '\t', '\n', '\r', ' ' };
            string[] coords = File.ReadAllText(fileName)
                .Split(delims, StringSplitOptions.RemoveEmptyEntries);

            // create point from coords
            // if number of coords not even - omit last coord
            for (int i = 0; i < coords.Length-1; i+=2)
            {
                double x, y;

                if (!double.TryParse(coords[i], NumberStyles.Float, CultureInfo.InvariantCulture, out x))
                {
                    Console.WriteLine($"[ERROR] Could not parse x coord at index {i}");
                    x = 0;
                }
                    
                if (!double.TryParse(coords[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                {
                    Console.WriteLine($"[ERROR] Could not parse y coord at index {i+1}");
                    y = 0;
                } 

                loadedList.Add(new Point(x, y));

                Console.WriteLine($"{x}\t{y}");
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

        public List<Point> GenerateData(double minNumber = 0, double maxNumber = 100, int count = 100, string fileName = "")
        {
            StreamWriter file = null;
            if(!fileName.Equals(""))
                file = new StreamWriter(fileName);

            List<Point> generatedList = new List<Point>();
            Random random = new Random();
            double x, y;

            for (int i = 0; i < count; ++i)
            {
                x = random.NextDouble() * (maxNumber - minNumber) + minNumber;
                y = random.NextDouble() * (maxNumber - minNumber) + minNumber;
                generatedList.Add(new Point(x, y));

                if (file != null)
                    file.WriteLine($"{x.ToString("F6", CultureInfo.InvariantCulture)}\t" +
                        $"{y.ToString("F6", CultureInfo.InvariantCulture)}");
            }

            if (file != null)
                file.Close();

            return generatedList;
        }
    }
}
