using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class PointTests
    {
        private Point _p1, _p2, _p3;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(1.0, 2.0);
            _p2 = new Point(1.0, 2.0);
            _p3 = new Point(2.0, 1.0);
        }

        [Test]
        public void PointConstructorTest()
        {
            Point p = new Point(2.5, 3.6);

            Assert.AreEqual(2.5, p.X, 0.001, "Point created with incorrect X value");
            Assert.AreEqual(3.6, p.Y, 0.001, "Point created with incorrect Y value");
        }

        [Test]
        public void PointEqualsTest()
        {
            Assert.IsTrue(_p1.Equals(_p2), "Points 1 and 2 are not equal (Equals method)");
            Assert.IsTrue(_p1 == _p2, "Points 1 and 2 are not equal (== operator)");
            Assert.IsFalse(_p1 == _p3, "Points 1 and 3 are equal (== operator)");
            Assert.IsTrue(_p2 != _p3, "Points 2 and 2 are equal (!= operator)");
        }

        [Test]
        public void PointToStringTest()
        {
            Assert.AreEqual("1.000000\t2.000000", _p1.ToString(), "Point string generated incorrectly");
            Assert.AreEqual("2.000000\t1.000000", _p3.ToString(), "Point string generated incorrectly");
        }
    }
}
