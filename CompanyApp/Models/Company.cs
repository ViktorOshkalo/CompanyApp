using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Company
    {
        public List<Employee> Employees { get; } = new List<Employee>();

        public static Company companyInstance { get; private set; }

        static Company()
        {
            companyInstance = new Company();
        }

        private Company()   // Lets say this is singleton
        {
        }

        public void AddEmployee(Employee employee)
        {
            if (employee is ManagerAbstract)
            {
                foreach (var subordinate in (employee as ManagerAbstract).SubordinateEmployees)
                {
                    try
                    {
                        AddEmployee(subordinate);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            if (!Employees.Contains(employee))
                Employees.Add(employee);
            else
                throw new Exception(message: "Employee already exist in current list: " + employee.Name + ", ID: " + employee.ID);
        }

        public double GetTotalSalary()
        {
            return Employees.Sum(emp => emp.CalcSalary());
        }
    }
}
