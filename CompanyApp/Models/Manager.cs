using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApp.Models
{
    public abstract class ManagerAbstract : Employee, IBoss     // share logic between all MenagerAbstract derived classes 
    {
        public ManagerAbstract(string name, DateTime startWorkingDate, double baseSalary) : base(name, startWorkingDate, baseSalary)
        {
            SubordinateEmployees = new SubordinateList(this);
            SubordinateEmployees.AddedEmployee += SetAsBoss;        // subscribe to suboridnate add
        }

        public IEmployeeList SubordinateEmployees { get; private set; }

        private void SetAsBoss(object sender, IEmployee employee)
        {
            employee.Boss = this;
        }
    }

    public class Manager : ManagerAbstract
    {
        public Manager(string name, DateTime startWorkingDate, double baseSalary = 1000) : base(name, startWorkingDate, baseSalary)
        {
        }

        //bonus conditions
        //Зарплата сотрудника Manager - это базовая ставка плюс 5% за каждый год работы в компании(но не больше 40% суммарной надбавки) плюс 0,5% зарплаты всех подчинённых первого уровня.
        double _bonusYearPercent = 0.05,
                _maxBonusPercent = 0.4,
                _firstLevelSubordinariesBonusPercent = 0.005;

        public override double CalcSalary()
        {
            //calc salary
            double firstLevelSubordinariesBonus = SubordinateEmployees.Sum(sub => sub.CalcSalary()) * _firstLevelSubordinariesBonusPercent;

            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonusPercent = _maxBonusPercent;
            if (totalWorkingYears * _bonusYearPercent < currentBonusPercent)
                currentBonusPercent = totalWorkingYears * _bonusYearPercent;

            return BaseSalaryRate + BaseSalaryRate * currentBonusPercent + firstLevelSubordinariesBonus;
        }
    }
}
