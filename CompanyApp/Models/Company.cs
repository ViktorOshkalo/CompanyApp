using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Company
    {
        public List<IEmployee> Employees { get; } = new List<IEmployee>();

        public static Company CompanyInstance { get; private set; }

        static Company()
        {
            CompanyInstance = new Company();
        }

        private Company()   // Lets say this is singleton
        {
        }

        public void AddEmployee(IEmployee employee)
        {
            if (employee is IBoss)
            {
                foreach (var subordinate in (employee as IBoss).SubordinateEmployees)
                {
                    AddEmployee(subordinate);
                }
            }

            if (!Employees.Contains(employee))
                Employees.Add(employee);
        }

        public double GetTotalSalary()
        {
            return Employees.Sum(emp => emp.CalcSalary());
        }
    }
}
