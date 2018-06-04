using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Sales : ManagerAbstract
    {
        //bonus conditions
        //Зарплата сотрудника Sales - это базовая ставка плюс 1% за каждый год работы в компании(но не больше 35% суммарной надбавки) плюс 0,3% зарплаты всех подчинённых всех уровней.
        double _bonusYearPercent = 0.01,
                _maxBonusPercent = 0.35,
                _allLevelSubordinariesBonusPercent = 0.003;

        public Sales(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }

        public override double CalcSalary()
        {
            //calc salary
            double allLevelSubordinariesBonus = SubordinateEmployees.Sum(sub => CalcRecursionSalary(sub)) * _allLevelSubordinariesBonusPercent;

            double maxBonus = BaseSalaryRate * _maxBonusPercent;
            double maxSalary = BaseSalaryRate + maxBonus;
            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonus = BaseSalaryRate * _bonusYearPercent * totalWorkingYears;
            if (currentBonus > maxBonus)
                return maxSalary + allLevelSubordinariesBonus;
            else
                return BaseSalaryRate + currentBonus + allLevelSubordinariesBonus;
        }

        //recursive salary calculation of all subordinate employees 
        private double CalcRecursionSalary(IEmployee employee)
        {
            double sumSalary = 0;
            if (employee is ManagerAbstract)
            {
                foreach (var subordinate in (employee as ManagerAbstract).SubordinateEmployees)
                {
                    sumSalary += subordinate.CalcSalary();
                    sumSalary += CalcRecursionSalary(subordinate);
                }
            }
            else
            {
                sumSalary += employee.CalcSalary();
            }

            return sumSalary;
        }
    }
}
