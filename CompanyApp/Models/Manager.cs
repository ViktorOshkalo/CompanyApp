using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApp.Models
{
    public abstract class ManagerAbstract : Employee, IBoss     // share logic between all MenagerAbstract derived classes 
    {
        public ManagerAbstract(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }

        public ManagerAbstract(string name, DateTime startWorkingDate, double baseSalary) : base(name, startWorkingDate, baseSalary)
        {
        }

        private List<IEmployee> _subordinateEmployees = new List<IEmployee>();
        public IList<IEmployee> SubordinateEmployees
        {
            get => _subordinateEmployees.AsReadOnly();  // unable to change private field directly
            set
            {
                _subordinateEmployees = value as List<IEmployee>;

                if (_subordinateEmployees.Contains(this))   // remove self
                    _subordinateEmployees.Remove(this);

                foreach (var sub in _subordinateEmployees)
                {
                    sub.Boss = this;    // set Boss property for all subordinates to this instance 
                }
            }
        }
    }

    public class Manager : ManagerAbstract
    {
        public Manager(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }

        public Manager(string name, DateTime startWorkingDate, double baseSalary) : base(name, startWorkingDate, baseSalary)
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
