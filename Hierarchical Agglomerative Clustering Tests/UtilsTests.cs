using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using static Hierarchical_Agglomerative_Clustering.Utils;
using System.Collections.Generic;
using System;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    public class UtilsTests
    {
        private Point _p1, _p2, _p3, _p4, _p5, _p6;
        private Cluster _c1, _c2; 
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(0.6, 0.4);
            _p2 = new Point(1.6, 0.4);
            _p3 = new Point(2.6, 2.4);
            _p4 = new Point(3.6, 2.6);
            _p5 = new Point(4.6, 0.4);
            _p6 = new Point(5.6, 0.6);

            _c1 = new Cluster(new List<Point> { _p1, _p2, _p3 });
            _c2 = new Cluster(new List<Point> { _p4, _p5, _p6 });
        }

        [Test]
        public void DistanceManhattanTest()
        {
            Assert.AreEqual(1.0, Distance(_p1, _p2, "manhattan"), 0.001, "Manhattan distance calculated incorrectly");
            Assert.AreEqual(5.2, Distance(_p1, _p4, "manhattan"), 0.001, "Manhattan distance calculated incorrectly");
        }

        [Test]
        public void DistanceEuclideanTest()
        {
            Assert.AreEqual(1.0, Distance(_p1, _p2, "euclidean"), 0.001, "Euclidean distance calculated incorrectly");
            Assert.AreEqual(3.72022, Distance(_p1, _p4, "euclidean"), 0.00001, "Euclidean distance calculated incorrectly");
        }

        [Test]
        public void DistanceEuclideanSquaredTest()
        {
            Assert.AreEqual(1.0, Distance(_p1, _p2, "euclidean2"), 0.001, "Euclidean squared distance calculated incorrectly");
            Assert.AreEqual(13.84, Distance(_p1, _p4, "euclidean2"), 0.001, "Euclidean squared distance calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceMinimumTest()
        {
            Assert.AreEqual(Distance(_p3, _p4), ClusterDistance(_c1, _c2, "minimum"),
                0.001, "Cluster distance (minimum linkage) calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceMaximumTest()
        {
            Assert.AreEqual(Distance(_p1, _p6), ClusterDistance(_c1, _c2, "maximum"),
                0.001, "Cluster distance (maximum linkage) calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceAverageTest()
        {
            double expected = 0;

            foreach (Point p1 in _c1.Points)
                foreach (Point p2 in _c2.Points)
                    expected += Distance(p1, p2);

            expected /= 3*3;

            Assert.AreEqual(expected, ClusterDistance(_c1, _c2, "average"),
                0.001, "Cluster distance (average linkage) calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceMinimumManhattanTest()
        {
            Assert.AreEqual(Distance(_p3, _p4, "manhattan"), ClusterDistance(_c1, _c2, "minimum", "manhattan"),
                0.001, "Cluster distance (minimum linkage, manhattan) calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceMaximumManhattanTest()
        {
            Assert.AreEqual(Distance(_p1, _p6, "manhattan"), ClusterDistance(_c1, _c2, "maximum", "manhattan"),
                0.001, "Cluster distance (maximum linkage, manhattan) calculated incorrectly");
        }

        [Test]
        public void ClusterDistanceAverageManhattanTest()
        {
            double expected = 0;

            foreach (Point p1 in _c1.Points)
                foreach (Point p2 in _c2.Points)
                    expected += Distance(p1, p2, "manhattan");

            expected /= 3 * 3;

            Assert.AreEqual(expected, ClusterDistance(_c1, _c2, "average", "manhattan"),
                0.001, "Cluster distance (average linkage, manhattan) calculated incorrectly");
        }
    }
}