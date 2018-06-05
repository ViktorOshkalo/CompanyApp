using System;
using NUnit.Framework;
using CompanyApp.Models;

namespace CompanyApp.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private Company _company;

        [OneTimeSetUp]
        public void InitData()
        {
            _company = Company.CompanyInstance;
        }

        public void AddSubordinaries()
        {
            IBoss boss = new
        }



        [Test]
        public void CreateEmployees()
        {
            Company company = Company.CompanyInstance;


            Assert.That(0, Is.EqualTo(0));
        }

        [Test]
        public void TestMethod2()
        {
            Assert.That(0, Is.EqualTo(1));
        }
    }
}
