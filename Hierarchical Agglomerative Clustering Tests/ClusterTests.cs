using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class ClusterTests
    {
        private Point _p1, _p2, _p3, _p4;
        private Cluster _c1, _c2, _c3, _c4, _c5, _c6, _c7, _c8;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(0.6, 0.4);
            _p2 = new Point(1.6, 0.4);
            _p3 = new Point(2.6, 2.4);
            _p4 = new Point(3.6, 2.6, 1.0);


            _c1 = new Cluster(new List<Point> { _p1 });
            _c2 = new Cluster(new List<Point> { _p2, _p3 });
            _c3 = _c1;
            _c4 = new Cluster(new List<Point> { _p1 });
            _c5 = new Cluster(new Point(0.6, 0.4));
            _c6 = new Cluster(new List<Point> { new Point(1.6, 0.4), new Point(2.6, 2.4) });
            _c7 = new Cluster(new List<Point> { _p3, _p2 });
            _c8 = new Cluster(_p4);
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
            Assert.IsFalse(_c1 == _c2, "Clusters 1 and 2 should not be equal (== operator)");
            Assert.IsFalse(_c1.Equals(_c2), "Clusters 1 and 2 should not be equal (Equals operator)");
            Assert.IsFalse(_c1 == _c8, "Clusters 1 and 8 should not be equal");
            Assert.IsTrue(_c1 != _c2, "Clusters 1 and 2 should not be equal (!= operator)");
            Assert.IsTrue(_c2 == _c7, "Clusters 1 and 2 should be equal");
            Assert.IsTrue(_c1 == _c3, "Clusters 1 and 3 should be equal");
            Assert.IsTrue(_c1 == _c4, "Clusters 1 and 4 should be equal");
            Assert.IsTrue(_c1 == _c5, "Clusters 1 and 5 should be equal");
            Assert.IsTrue(_c3 == _c5, "Clusters 3 and 5 should be equal");
            Assert.IsTrue(_c3 == _c4, "Clusters 3 and 4 should be equal");
            Assert.IsTrue(_c4 == _c5, "Clusters 4 and 5 should be equal");
            Assert.IsTrue(_c2 == _c6, "Clusters 2 and 6 should be equal");
        }

        [Test]
        public void ClusterHashCodeTest()
        {
            Assert.AreNotEqual(_c1.GetHashCode(), _c2.GetHashCode(), "Hash codes of clusters 1 and 2 should not be equal");
            Assert.AreNotEqual(_c1.GetHashCode(), _c8.GetHashCode(), "Hash codes of clusters 1 and 8 should not be equal");
            Assert.AreEqual(_c2.GetHashCode(), _c7.GetHashCode(), 0.0001, "Hash codes of clusters 2 and 7 should not be equal");
            Assert.AreEqual(_c1.GetHashCode(), _c3.GetHashCode(), 0.0001, "Hash codes of clusters 1 and 3 should be equal");
            Assert.AreEqual(_c1.GetHashCode(), _c4.GetHashCode(), 0.0001, "Hash codes of clusters 1 and 4 should be equal");
            Assert.AreEqual(_c1.GetHashCode(), _c5.GetHashCode(), 0.0001, "Hash codes of clusters 1 and 5 should be equal");
            Assert.AreEqual(_c3.GetHashCode(), _c5.GetHashCode(), 0.0001, "Hash codes of clusters 3 and 5 should be equal");
            Assert.AreEqual(_c3.GetHashCode(), _c4.GetHashCode(), 0.0001, "Hash codes of clusters 3 and 4 should be equal");
            Assert.AreEqual(_c4.GetHashCode(), _c5.GetHashCode(), 0.0001, "Hash codes of clusters 4 and 5 should be equal");
            Assert.AreEqual(_c2.GetHashCode(), _c6.GetHashCode(), 0.0001, "Hash codes of clusters 2 and 6 should be equal");
        }

    }
}
