using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class HACTests
    {
        private Point _p1, _p2, _p3, _p4, _p5, _p6;
        private List<Point> _testInput;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(0.6, 0.4);
            _p2 = new Point(1.6, 0.4);
            _p3 = new Point(2.6, 2.4);
            _p4 = new Point(3.6, 2.6);
            _p5 = new Point(4.6, 0.4);
            _p6 = new Point(5.6, 0.6);

            _testInput = new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 };
        }

        [Test]
        public void HACClusterDataTest()
        {
            HAC hac = new HAC();
            List<Cluster> testOutput = hac.ClusterData(_testInput, verbosity: 0);
            List<Cluster> testOutput2 = hac.ClusterData(_testInput, "average", "manhattan", 0);
            List<Cluster> testOutput3 = hac.ClusterData(_testInput, "minimum", "euclidean", 0);

            bool equal12 = true;
            if (testOutput.Count != testOutput2.Count)
                equal12 = false;
            else
                foreach(Cluster c in testOutput)
                    if(!testOutput2.Contains(c))
                        equal12 = false;

            Assert.IsTrue(equal12, "Outputs 1 and 2 are not equal");

            Assert.IsTrue(testOutput.Count.Equals(_testInput.Count*2 - 1), "HAC output incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p1)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p2)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p3)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p4)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p5)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p6)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2})), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p3, _p4 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p3, _p4, _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsFalse(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2, _p3 })), "HAC output 1 has incorrect data");
            Assert.IsFalse(testOutput.Contains(new Cluster(new List<Point> { _p1, _p6 })), "HAC output 1 has incorrect data");

            Assert.IsTrue(testOutput3.Count.Equals(_testInput.Count * 2 - 1), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p1)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p2)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p3)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p4)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p5)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p6)), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2 })), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p3, _p4 })), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p5, _p6 })), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4 })), "HAC output 2 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 })), "HAC output 2 incomplete");
            Assert.IsFalse(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3 })), "HAC output 2 has incorrect data");
            Assert.IsFalse(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p6 })), "HAC output 2 has incorrect data");
        }
    }
}
