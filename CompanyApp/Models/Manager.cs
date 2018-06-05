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

        private List<IEmployee> _subordinateEmployees = new List<IEmployee>();
        public IList<IEmployee> SubordinateEmployees
        {
            get => _subordinateEmployees.AsReadOnly();  // unable to change private field directly
            set
            {
                _subordinateEmployees = value as List<IEmployee>;
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

        //bonus conditions
        //Зарплата сотрудника Manager - это базовая ставка плюс 5% за каждый год работы в компании(но не больше 40% суммарной надбавки) плюс 0,5% зарплаты всех подчинённых первого уровня.
        double _bonusYearPercent = 0.05,
                _maxBonusPercent = 0.4,
                _firstLevelSubordinariesBonusPercent = 0.005;

        public override double CalcSalary()
        {
            //calc salary
            double firstLevelSubordinariesBonus = SubordinateEmployees.Sum(sub => sub.CalcSalary()) * _firstLevelSubordinariesBonusPercent;

            double maxBonus = BaseSalaryRate * _maxBonusPercent;
            double maxSalary = BaseSalaryRate + maxBonus;
            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonus = BaseSalaryRate * _bonusYearPercent * totalWorkingYears;
            if (currentBonus > maxBonus)
                return maxSalary + firstLevelSubordinariesBonus;
            else
                return BaseSalaryRate + currentBonus + firstLevelSubordinariesBonus;
        }
    }
}
