using NUnit.Framework;
using Hierarchical_Agglomerative_Clustering;
using System.Collections.Generic;

namespace Hierarchical_Agglomerative_Clustering_Tests
{
    class PointTests
    {
        private Point _p1, _p2, _p3, _p4, _p5, _p6;
        [SetUp]
        public void Setup()
        {
            _p1 = new Point(1.0, 2.0);
            _p2 = new Point(1.0, 2.0);
            _p3 = new Point(2.0, 1.0);
            _p4 = new Point(1.0, 2.0, 0.0);
            _p5 = new Point(1.0, 2.0, 0.0);
            _p6 = new Point(0.0, 2.0, 1.0);
        }

        [Test]
        public void PointConstructorTest()
        {
            Point p = new Point(2.5, 3.6);

            Assert.AreEqual(2.5, p.Dimensions[0], 0.001, "Point created with incorrect dim0 value");
            Assert.AreEqual(3.6, p.Dimensions[1], 0.001, "Point created with incorrect dim1 value");

            p = new Point(new List<double> { 1.1, 2.1, 3.1 });

            Assert.AreEqual(1.1, p.Dimensions[0], 0.001, "Point created with incorrect dim0 value (list ctor)");
            Assert.AreEqual(2.1, p.Dimensions[1], 0.001, "Point created with incorrect dim1 value (list ctor)");
            Assert.AreEqual(3.1, p.Dimensions[2], 0.001, "Point created with incorrect dim2 value (list ctor)");
        }

        [Test]
        public void PointEqualsTest()
        {
            Assert.IsTrue(_p1.Equals(_p2), "Points 1 and 2 are not equal (Equals method)");
            Assert.IsTrue(_p1 == _p2, "Points 1 and 2 are not equal (== operator)");
            Assert.IsFalse(_p1 == _p3, "Points 1 and 3 are equal (== operator)");
            Assert.IsTrue(_p2 != _p3, "Points 2 and 2 are equal (!= operator)");
            Assert.IsFalse(_p1 == _p4, "Points 1 and 4 are equal");
            Assert.IsFalse(_p4 == _p6, "Points 4 and 6 are equal");
            Assert.IsTrue(_p4 == _p5, "Points 4 and 5 are not equal");
        }

        [Test]
        public void PointToStringTest()
        {
            Assert.AreEqual("1.000000\t2.000000", _p1.ToString(), "Point string generated incorrectly");
            Assert.AreEqual("2.000000\t1.000000", _p3.ToString(), "Point string generated incorrectly");
        }

        [Test]
        public void PointHashCodeTest()
        {
            Assert.AreNotEqual(_p1.GetHashCode(), _p3.GetHashCode(), "Hash codes of points 1 and 3 are equal");
            Assert.AreEqual(_p1.GetHashCode(), _p2.GetHashCode(), 0.0001, "Hash codes of points 1 and 2 are not equal");
            Assert.AreNotEqual(_p1.GetHashCode(), _p4.GetHashCode(), "Hash codes of points 1 and 4 are equal");
            Assert.AreNotEqual(_p4.GetHashCode(), _p6.GetHashCode(), "Hash codes of points 4 and 6 are equal");
        }
    }
}
