using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public DateTime StartWorkingDate { get; private set; }
        public double BaseSalaryRate { get; set; }
        private IBoss _boss;
        public IBoss Boss {
            get => _boss;
            set
            {
                if (this != value)
                {
                    _boss = value;
                }
                else
                    throw new Exception(message: "unable set Boss to self");
            }
        }

        public Employee(string name, DateTime startWorkingDate, double baseSalary = 1000)
        {
            Name = name;
            StartWorkingDate = startWorkingDate;
            BaseSalaryRate = baseSalary;
        }

        //bonus conditions
        //Зарплата сотрудника Employee - это базовая ставка плюс 3% за каждый год работы в компании, но не больше 30% суммарной надбавки.
        private double _bonusYearPercent = 0.03,
                       _maxBonusPercent = 0.3;

        public virtual double CalcSalary()
        {
            int totalWorkingYears = (DateTime.Today - StartWorkingDate).Days / 365;

            double currentBonusPercent = _maxBonusPercent;
            if (totalWorkingYears * _bonusYearPercent < currentBonusPercent)
                currentBonusPercent = totalWorkingYears * _bonusYearPercent;

            return BaseSalaryRate + BaseSalaryRate * currentBonusPercent;
        }
    }
}
