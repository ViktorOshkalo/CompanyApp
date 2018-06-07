using System;
using NUnit.Framework;
using CompanyApp.Models;
using System.Collections.Generic;

namespace CompanyApp.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void SetSubordinates()
        {
            var _employee1 = new Employee("Jhon1", new DateTime(2017, 3, 3), 1000);
            var _employee2 = new Employee("Jhon2", new DateTime(2017, 3, 3), 1000);
            var _manager = new Manager("Nick", new DateTime(2015, 2, 2), 1000.0);
            var _sales = new Manager("Ralf", new DateTime(2012, 1, 1), 1000.0);

            Assert.DoesNotThrow(() =>
                {
                    _manager.SubordinateEmployees.Add(_employee1);
                    _manager.SubordinateEmployees.Add(_employee2);
                    _sales.SubordinateEmployees.Add(_manager);
                }
            );
            Assert.That(_manager.SubordinateEmployees, Contains.Item(_employee1));
            Assert.That(_manager.SubordinateEmployees, Contains.Item(_employee2));
            Assert.That(_sales.SubordinateEmployees, Contains.Item(_manager));


            // migrate employee 
            _manager.SubordinateEmployees.Remove(_employee1);
            _sales.SubordinateEmployees.Add(_employee1);
            Assert.That(_manager.SubordinateEmployees, Has.No.Member(_employee1));
            Assert.That(_sales.SubordinateEmployees, Contains.Item(_employee1));


        }

        [Test(Description = "test employee salary")] 
        public void SalaryCalcEmployee()
        {
            // Employee
            // bonusYearPercent = 0.03,
            // maxBonusPercent = 0.3,

            IEmployee employee = new Employee("NameEmployee", new DateTime(2017, 3, 3), 1000);
            double expected = 1030; // 1000 + (1 year * 0.03) * 1000

            Assert.That(Math.Round(employee.CalcSalary()), Is.EqualTo(expected));
        }

        [Test(Description = "test manager salary")]
        public void SalaryCalcManager()
        {
            // Manager
            // bonusYearPercent = 0.05,
            // maxBonusPercent = 0.4,
            // firstLevelSubordinariesBonusPercent = 0.005;

            IEmployee employee1 = new Employee("NameEmployee1", new DateTime(2017, 3, 3), 1000); // 1030
            IEmployee employee2 = new Employee("NameEmployee2", new DateTime(2017, 3, 3), 1000); // 1030
            IBoss manager = new Manager("NameManager", new DateTime(2015, 4, 4), 1000);
            manager.SubordinateEmployees.Add(employee1, employee2);


            double expected = 1160;  // 1000 + (3 years * 0.05 * 1000) + (1030 * 0.005) 

            Assert.That(Math.Round(manager.CalcSalary()), Is.EqualTo(expected));
        }

        [Test(Description = "test sales salary")]
        public void SalaryCalcSales()
        {
            // Sales
            // bonusYearPercent = 0.01,
            // maxBonusPercent = 0.35,
            // firstLevelSubordinariesBonusPercent = 0.003;

            IEmployee employee1 = new Employee("NameEmployee1", new DateTime(2017, 3, 3), 1000); // 1030
            IEmployee employee2 = new Employee("NameEmployee2", new DateTime(2017, 3, 3), 1000); // 1030
            IBoss manager = new Manager("NameManager", new DateTime(2015, 4, 4), 1000);          // 1160
            manager.SubordinateEmployees.Add(employee1, employee2);
            IBoss sales = new Sales("NameSales", new DateTime(2013, 5, 5), 1000);
            sales.SubordinateEmployees.Add( manager);

            double expected = 1056; // 1000 + (5 years * 0.01 * 1000) + (1030 + 1030 + 1155.2) * 0.003

            Assert.That(Math.Round(sales.CalcSalary()), Is.EqualTo(expected));
        }

        [Test(Description = "test total salary")]
        public void SalaryCalcTotal()
        {
            IEmployee employee1 = new Employee("NameEmployee1", new DateTime(2017, 3, 3), 1000); // 1030
            IEmployee employee2 = new Employee("NameEmployee2", new DateTime(2017, 3, 3), 1000); // 1030
            IBoss manager = new Manager("NameManager", new DateTime(2015, 4, 4), 1000);          // 1160
            manager.SubordinateEmployees.Add(employee1, employee2);
            IBoss sales = new Sales("NameSales", new DateTime(2013, 5, 5), 1000);                // 1056
            sales.SubordinateEmployees.Add(manager);

            var company = Company.CompanyInstance;

            company.AddEmployee(employee1);
            company.AddEmployee(employee2);
            company.AddEmployee(manager);
            company.AddEmployee(sales);

            double expected = 4276;                                                             // sum 4276

            Assert.That(Math.Round(company.GetTotalSalary()), Is.EqualTo(expected));
        }

    }
}
