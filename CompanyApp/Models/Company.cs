using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Company
    {
        private List<IEmployee> _employees = new List<IEmployee>();
        public  IList<IEmployee> Employees { get => _employees.AsReadOnly(); private set{} } // unable to change private list directly

        public static Company CompanyInstance { get; private set; }

        static Company()
        {
            CompanyInstance = new Company();
        }

        private Company()   // Let say this is singleton
        {
        }

        public void AddEmployee(IEmployee employee)
        {
            if (employee is IBoss)
            {
                foreach (var subordinate in (employee as IBoss).SubordinateEmployees.ToList())
                {
                    AddEmployee(subordinate);
                }
                (employee as IBoss).SubordinateEmployees.AddedEmployee += AddEmployee;
                (employee as IBoss).SubordinateEmployees.RemovedEmployee += RemoveEmployee;
            }

            if (!_employees.Contains(employee))
                _employees.Add(employee);
        }

        private void AddEmployee(object sender, IEmployee employee)
        {
            AddEmployee(employee);
        }

        public bool RemoveEmployee(IEmployee employee)
        {
            employee.Boss?.SubordinateEmployees.Remove(employee);
            return _employees.Remove(employee);
        }

        private void RemoveEmployee(object sender, IEmployee employee)
        {
            RemoveEmployee(employee);
        }

        public double GetTotalSalary()
        {
            return _employees.Sum(emp => emp.CalcSalary());
        }
    }
}
