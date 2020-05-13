using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class HACTests
    {
        private Point _p1, _p2, _p3, _p4, _p5, _p6, _p7, _p8, _p9, _p10;
        private List<Point> _testInput, _testInput2;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(0.6, 0.4);
            _p2 = new Point(1.6, 0.4);
            _p3 = new Point(2.6, 2.4);
            _p4 = new Point(3.6, 2.6);
            _p5 = new Point(4.6, 0.4);
            _p6 = new Point(5.6, 0.6);

            _p7 = new Point(0.0, 0.0, 0.0, 0.0);
            _p8 = new Point(1.0, 0.0, 1.0, 0.0);
            _p9 = new Point(0.0, 2.0, 0.0, 2.0);
            _p10 = new Point(5.0, 5.0, 5.0, 5.0);

            _testInput = new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 };
            _testInput2 = new List<Point> { _p7, _p8, _p9, _p10 };
        }

        [Test]
        public void HACClusterDataTest2D()
        {
            HAC hac = new HAC();
            List<Cluster> testOutput = hac.ClusterData(_testInput, "average", "euclidean2", 0);
            List<Cluster> testOutput2 = hac.ClusterData(_testInput, "average", "manhattan", 0);
            List<Cluster> testOutput3 = hac.ClusterData(_testInput, "minimum", "euclidean", 0);
            List<Cluster> testOutput4 = hac.ClusterData(_testInput, "maximum", "euclidean2", 0);

            bool equal124 = true;
            if (testOutput.Count != testOutput2.Count || testOutput.Count != testOutput4.Count)
                equal124 = false;
            else
                foreach (Cluster c in testOutput)
                    if (!testOutput2.Contains(c) || ! testOutput4.Contains(c))
                        equal124 = false;

            Assert.IsTrue(equal124, "Outputs 1, 2 and 4 are not equal");

            Assert.IsTrue(testOutput.Count.Equals(_testInput.Count * 2 - 1), "HAC output incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p1)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p2)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p3)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p4)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p5)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(_p6)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p3, _p4 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p3, _p4, _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 })), "HAC output 1 incomplete");
            Assert.IsFalse(testOutput.Contains(new Cluster(new List<Point> { _p1, _p2, _p3 })), "HAC output 1 has incorrect data");
            Assert.IsFalse(testOutput.Contains(new Cluster(new List<Point> { _p1, _p6 })), "HAC output 1 has incorrect data");

            Assert.IsTrue(testOutput3.Count.Equals(_testInput.Count * 2 - 1), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p1)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p2)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p3)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p4)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p5)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(_p6)), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2 })), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p3, _p4 })), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p5, _p6 })), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4 })), "HAC output 3 incomplete");
            Assert.IsTrue(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3, _p4, _p5, _p6 })), "HAC output 3 incomplete");
            Assert.IsFalse(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p2, _p3 })), "HAC output 3 has incorrect data");
            Assert.IsFalse(testOutput3.Contains(new Cluster(new List<Point> { _p1, _p6 })), "HAC output 3 has incorrect data");
        }

        [Test]
        public void HACClusterDataTest4D()
        {
            HAC hac = new HAC();
            List<Cluster> testOutput1 = hac.ClusterData(_testInput2, "average", "euclidean", 0);
            List<Cluster> testOutput2 = hac.ClusterData(_testInput2, "minimum", "euclidean2", 0);
            List<Cluster> testOutput3 = hac.ClusterData(_testInput2, "minimum", "manhattan", 0);
            List<Cluster> testOutput4 = hac.ClusterData(_testInput2, "maximum", "euclidean", 0);
            List<Cluster> testOutput5 = hac.ClusterData(_testInput2, "maximum", "manhattan", 0);

            bool allEqual = true;
            if (testOutput1.Count != testOutput2.Count || testOutput1.Count != testOutput3.Count ||
                testOutput1.Count != testOutput4.Count || testOutput1.Count != testOutput5.Count)
                allEqual = false;
            else
                foreach (Cluster c in testOutput1)
                    if (!testOutput2.Contains(c) || !testOutput3.Contains(c)
                        || !testOutput4.Contains(c) || !testOutput5.Contains(c))
                        allEqual = false;

            Assert.IsTrue(allEqual, "Outputs are not equal");
            Assert.IsTrue(testOutput1.Count.Equals(_testInput2.Count * 2 - 1), "HAC output incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(_p7)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(_p8)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(_p9)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(_p10)), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(new List<Point> { _p7, _p8 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(new List<Point> { _p7, _p8, _p9 })), "HAC output 1 incomplete");
            Assert.IsTrue(testOutput1.Contains(new Cluster(new List<Point> { _p7, _p8, _p9, _p10 })), "HAC output 1 incomplete");
            Assert.IsFalse(testOutput1.Contains(new Cluster(new List<Point> { _p7, _p9})), "HAC output 1 has incorrect data");
            Assert.IsFalse(testOutput1.Contains(new Cluster(new List<Point> { _p9, _p10 })), "HAC output 1 has incorrect data");
        }
    }
}
