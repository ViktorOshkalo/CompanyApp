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

        public Sales(string name, DateTime startWorkingDate, double baseSalary = 1000) : base(name, startWorkingDate, baseSalary)
        {
        }

        public override double CalcSalary()
        {
            //calc salary
            double allLevelSubordinariesBonus = SubordinateEmployees.Sum(sub => CalcRecursionSalary(sub)) * _allLevelSubordinariesBonusPercent;

            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonusPercent = _maxBonusPercent;
            if (totalWorkingYears * _bonusYearPercent < currentBonusPercent)
                currentBonusPercent = totalWorkingYears * _bonusYearPercent;

            return BaseSalaryRate + BaseSalaryRate * currentBonusPercent + allLevelSubordinariesBonus;
        }

        //recursive salary calculation of all levels subordinate employees 
        private double CalcRecursionSalary(IEmployee employee)
        {
            double sumSalary = 0;
            if (employee is ManagerAbstract)
            {
                foreach (var subordinate in (employee as IBoss).SubordinateEmployees)
                {
                    sumSalary += subordinate.CalcSalary();
                    if (subordinate is IBoss)
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
