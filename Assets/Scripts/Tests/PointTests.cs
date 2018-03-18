using NUnit.Framework;
using UnityEngine;
using Utility;

namespace Tests
{
    public class PointTests
    {
        [Test]
        public void TestDistance()
        {
            var a = new Point(0, 0);
            var b = new Point(1, 0);
            var c = new Point(-1, 1);
            var d = new Point(1, -1);

            Assert.AreEqual(0, a.Distance(a));
            Assert.AreEqual(1, a.Distance(b));
            Assert.AreEqual(Mathf.Sqrt(2), a.Distance(c));
            Assert.AreEqual(Mathf.Sqrt(2), a.Distance(d));
            
            Assert.AreEqual(0, b.Distance(b));
            Assert.AreEqual(Mathf.Sqrt(5), b.Distance(c));
            Assert.AreEqual(1, b.Distance(d));

            Assert.AreEqual(0, c.Distance(c));
            Assert.AreEqual(Mathf.Sqrt(8), c.Distance(d));
            
            Assert.AreEqual(0, d.Distance(d));
        }
    }
}