using System;
using NUnit.Framework;

namespace CompanyApp.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.That(0, Is.EqualTo(0));
        }

        [Test]
        public void TestMethod2()
        {
            Assert.That(0, Is.EqualTo(1));
        }
    }
}
