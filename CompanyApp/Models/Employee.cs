using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Employee : IEmployee
    {
        public IBoss Boss { get; set; }
        public string Name { get; set; }
        public DateTime StartWorkingDate { get; private set; }
        public virtual double BaseSalaryRate { get; set; } = 1000; // by default

        public Employee(string name, DateTime startWorkingDate)
        {
            Name = name;
            StartWorkingDate = startWorkingDate;
        }

        //bonus conditions
        //Зарплата сотрудника Employee - это базовая ставка плюс 3% за каждый год работы в компании, но не больше 30% суммарной надбавки.
        private double _bonusYearPercent = 0.03,
                       _maxBonusPercent = 0.3;

        public virtual double CalcSalary()
        {
            //calc salary
            double maxBonus = BaseSalaryRate * _bonusYearPercent;
            double maxSalary = BaseSalaryRate + maxBonus;
            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonus = BaseSalaryRate * _maxBonusPercent * totalWorkingYears;
            if (currentBonus > maxBonus)
                return maxSalary;
            else
                return BaseSalaryRate + currentBonus;
        }
    }
}
