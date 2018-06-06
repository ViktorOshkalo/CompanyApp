using System;
using NUnit.Framework;
using CompanyApp.Models;
using System.Collections.Generic;

namespace CompanyApp.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private Company _company;
        IEmployee _employee1;
        IEmployee _employee2;
        IBoss _manager;
        IBoss _sales;

        [Test, Order(1)]
        public void MakeEmployees()
        {
            Assert.DoesNotThrow(() =>
                {
                    _company = Company.CompanyInstance;
                    _employee1 = new Employee("Jhon1", new DateTime(2017, 3, 3));
                    _employee2 = new Employee("Jhon2", new DateTime(2017, 3, 3));
                    _manager = new Manager("Nick", new DateTime(2015, 2, 2));
                    _sales = new Manager("Ralf", new DateTime(2012, 1, 1));
                }
            );

            Assert.Multiple(() =>
               {
                   Assert.NotNull(_employee1);
                   Assert.NotNull(_employee2);
                   Assert.NotNull(_manager);
                   Assert.NotNull(_sales);
               }
            );
        }

        [Test, Order(2)]
        public void SetSubordinates()
        {
            Assert.DoesNotThrow(() =>
                {
                    _manager.SubordinateEmployees = new List<IEmployee>() { _manager };
                }
            );
            Assert.That(_manager.SubordinateEmployees, Has.No.Member(_manager));

            Assert.DoesNotThrow(() =>
                {
                    _manager.SubordinateEmployees = new List<IEmployee>() { _employee1, _employee2 };
                    _sales.SubordinateEmployees = new List<IEmployee>() { _manager };
                }
            );
            Assert.That(_manager.SubordinateEmployees, Contains.Item(_employee1));
            Assert.That(_manager.SubordinateEmployees, Contains.Item(_employee2));
            Assert.That(_sales.SubordinateEmployees, Contains.Item(_manager));
        }

        [Test, Order(3)]
        public void SetCompanyEmployees()
        {
            Assert.DoesNotThrow(() =>
                {
                    _company.AddEmployee(_sales);   // also adds subordinates
                    _company.AddEmployee(_manager);
                }
            );

            Assert.That(_company.Employees.Count, Is.EqualTo(4));
        }


        [Test, Order(3)]
        public void SetBossToSelf()
        {
            _manager.Boss = _manager;
            Assert.That(_manager.Boss, Is.Not.EqualTo(_manager));
        }

        [Test(Description = "test employee1 salary"), Order(4)] 
        public void SalaryCalcEmployee()
        {
            //bonusYearPercent = 0.03,
            //maxBonusPercent = 0.3,
            //baseSalaryRate = 1000;
            //Start work = 2017/3/3

            // slary 1030

            Assert.That(Math.Round(_employee1.CalcSalary(), 1), Is.EqualTo(1030.0));
        }

        [Test(Description = "test manager salary"), Order(5)]
        public void SalaryCalcManager()
        {
            //bonusYearPercent = 0.05,
            //maxBonusPercent = 0.4,
            //firstLevelSubordinariesBonusPercent = 0.005;
            //baseSalaryRate = 1000;
            //Start work = 2015/2/2

            // slary 1150

            Assert.That(Math.Round(_manager.CalcSalary(), 1), Is.EqualTo(1160.3));
        }

        [Test(Description = "test sales salary"), Order(5)]
        public void SalaryCalcSales()
        {
            //bonusYearPercent = 0.01,
            //maxBonusPercent = 0.35,
            //firstLevelSubordinariesBonusPercent = 0.003;
            //baseSalaryRate = 1000;
            //Start work = 2012/1/1

            // slary 1305.75

            Assert.That(Math.Round(_sales.CalcSalary(), 1), Is.EqualTo(1305.8));
        }

        [Test(Description = "test total salary"), Order(5)]
        public void SalaryCalcTotal()
        {
            // total slary 1305.75

            Assert.That(Math.Round(_company.GetTotalSalary(), 1), Is.EqualTo(4526.1));
        }

    }
}
