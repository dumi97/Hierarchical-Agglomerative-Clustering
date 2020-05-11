using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Hierarchical_Agglomerative_Clustering
{
    public class Point
    {
        public List<double> Dimensions { get; private set; }

        #region Constructors
        public Point(params double[] dims)
        {
            Dimensions = new List<double>();
            foreach (double d in dims)
                Dimensions.Add(d);
        }

        public Point(List<double> dims)
        {
            Dimensions = dims;
        }

        public Point(Point p)
        {
            Dimensions = new List<double>();
            foreach (double d in p.Dimensions)
                Dimensions.Add(d);
        }
        #endregion

        public int GetDimenstions()
        {
            return Dimensions.Count;
        }

        #region Equality
        public static bool operator ==(Point p1, Point p2)
        {
            if (p1 is null)
                return p2 is null;

            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point p2 = (Point)obj;

            if (GetDimenstions() != p2.GetDimenstions())
                return false;

            for (int i = 0; i < GetDimenstions(); ++i)
                if (p2.Dimensions[i] != Dimensions[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                foreach (double d in Dimensions)
                    hash = (hash * 397) ^ d.GetHashCode();
                return hash;
            }
        }
        #endregion

        public override string ToString()
        {
            string output = "";
            foreach (double d in Dimensions)
                output += $"{d.ToString("F6", CultureInfo.InvariantCulture)}\t";

            return output.Trim();
        }
    }
}
