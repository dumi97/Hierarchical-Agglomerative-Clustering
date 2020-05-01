using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class ClusterTests
    {
        private Point _p1, _p2, _p3, _p4, _p5, _p6;
        private Cluster _c1, _c2, _c3, _c4, _c5, _c6;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(0.6, 0.4);
            _p2 = new Point(1.6, 0.4);
            _p3 = new Point(2.6, 2.4);
            _p4 = new Point(3.6, 2.6);
            _p5 = new Point(4.6, 0.4);
            _p6 = new Point(5.6, 0.6);

            _c1 = new Cluster(new List<Point> { _p1 });
            _c2 = new Cluster(new List<Point> { _p2, _p3 });
            _c3 = _c1;
            _c4 = new Cluster(new List<Point> { _p1 });
            _c5 = new Cluster(new Point(0.6, 0.4));
            _c6 = new Cluster(new List<Point> { new Point(1.6, 0.4), new Point(2.6, 2.4) });
        }

        [Test]
        public void ClusterConstructorTest()
        {
            Cluster c = new Cluster();
            Assert.AreEqual(0, c.Size, 0.000001, "Cluster empty constructor failed");

            c = new Cluster(_p1);
            Assert.AreEqual(1, c.Size, 0.000001, "Cluster single point constructor failed");
            Assert.IsTrue(c.Points[0] == _p1, "Cluster single point constructor failed");

            c = new Cluster(new List<Point> { _p1, _p2 });
            Assert.AreEqual(2, c.Size, 0.000001, "Cluster list constructor failed");
            Assert.IsTrue(c.Points[0] == _p1 && c.Points[1] == _p2, "Cluster list constructor failed");

            c = new Cluster(new Point[] { _p1, _p2 });
            Assert.AreEqual(2, c.Size, 0.000001, "Cluster array constructor failed");
            Assert.IsTrue(c.Points[0] == _p1 && c.Points[1] == _p2, "Cluster array constructor failed");

            c = new Cluster(_c1, _c2);
            Assert.AreEqual(3, c.Size, 0.000001, "Cluster join clusters constructor failed");
            Assert.IsTrue(c.Points[0] == _p1 && c.Points[1] == _p2 && c.Points[2] == _p3, "Cluster join clusters constructor failed");
        }

        [Test]
        public void ClusterEqualsTest()
        {
            Assert.IsFalse(_c1 == _c2, "Clusters should not be equal");
            Assert.IsFalse(_c1.Equals(_c2), "Clusters should not be equal");
            Assert.IsTrue(_c1 != _c2, "Clusters should not be equal");
            Assert.IsTrue(_c1 == _c3, "Clusters should be equal");
            Assert.IsTrue(_c1 == _c4, "Clusters should be equal");
            Assert.IsTrue(_c1 == _c5, "Clusters should be equal");
            Assert.IsTrue(_c3 == _c5, "Clusters should be equal");
            Assert.IsTrue(_c3 == _c4, "Clusters should be equal");
            Assert.IsTrue(_c4 == _c5, "Clusters should be equal");
            Assert.IsTrue(_c2 == _c6, "Clusters should be equal");
        }
    }
}
