using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{

    public abstract class EmployeeAbstract
    {
        static private int _numberID = 0;
        public int ID { get; }
        public readonly string Name;
        public readonly DateTime StartWorkingDate;
        public double BaseSalaryRate { get; set; } = 1000; // by default
        public EmployeeAbstract Boss { get; set; }

        public EmployeeAbstract(string name, DateTime startWorkingDate)
        {
            Name = name;
            StartWorkingDate = startWorkingDate;
            ID = _numberID++;
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public abstract double CalcSalary();
    }

    public class Employee : EmployeeAbstract
    {
        //bonus conditions
        //Зарплата сотрудника Employee - это базовая ставка плюс 3% за каждый год работы в компании, но не больше 30% суммарной надбавки.
        double  _bonusYearPercent = 0.03,
                _maxBonusPercent = 0.3;

        public Employee(string name, DateTime startWorkingDate): base(name, startWorkingDate)
        {
        }

        public override string ToString()
        {

            return String.Format("Employee: \n\tName: {0} \n\tBonus year percent: {1} \n\tMaxBonusPercent: {2} \n\tSalary: {3} \n\tBoss: {4}",
                Name,
                _bonusYearPercent,
                _maxBonusPercent,
                CalcSalary(),
                Boss != null ? Boss.Name : "--");
        }

        public override double CalcSalary()
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

    //public interface IManagerEmployee
    //{
    //    List<Employee> SubordinateEmployees { get; set; }
    //}

    public abstract class ManagerAbstract: Employee
    {
        private List<Employee> _subordinateEmployees = new List<Employee>();

        public List<Employee> SubordinateEmployees {
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

        public ManagerAbstract(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }
    }

    public class Manager : ManagerAbstract
    {
        //bonus conditions
        //Зарплата сотрудника Manager - это базовая ставка плюс 5% за каждый год работы в компании(но не больше 40% суммарной надбавки) плюс 0,5% зарплаты всех подчинённых первого уровня.
        double  _bonusYearPercent = 0.05,
                _maxBonusPercent = 0.4,
                _firstLevelSubordinariesBonusPercent = 0.005;

        public Manager(string name, DateTime startWorkingDate) : base(name, startWorkingDate)
        {
        }

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

    public class Sales: ManagerAbstract
    {
        //bonus conditions
        //Зарплата сотрудника Sales - это базовая ставка плюс 1% за каждый год работы в компании(но не больше 35% суммарной надбавки) плюс 0,3% зарплаты всех подчинённых всех уровней.
        double  _bonusYearPercent = 0.01,
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
        private double CalcRecursionSalary(EmployeeAbstract employee)
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
