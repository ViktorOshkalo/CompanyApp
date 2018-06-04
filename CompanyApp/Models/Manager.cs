using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public abstract class ManagerAbstract : EmployeeAbstract, IBoss
    {
        public ManagerAbstract(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }

        private List<IEmployee> _subordinateEmployees = new List<IEmployee>();
        public List<IEmployee> SubordinateEmployees
        {
            get
            {
                return _subordinateEmployees;
            }
            set
            {
                _subordinateEmployees = value;
                foreach (var sub in _subordinateEmployees)
                {
                    sub.Boss = this;
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
