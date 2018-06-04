using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyApp.Models;

namespace CompanyApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Company company = Company.companyInstance;

            Employee Viktor1 = new Employee("Viktor1", new DateTime(2013, 10, 1));
            Employee Viktor2 = new Employee("Viktor2", new DateTime(2013, 10, 1));

            Manager Yuri = new Manager("Yuri", new DateTime(2005, 5, 15));
            Sales Rozhok = new Sales("Rozhek", new DateTime(2014, 8, 1));
            Sales Rozhok2 = new Sales("Rozhek2", new DateTime(2014, 8, 1));

            Rozhok.SubordinateEmployees = new List<IEmployee>() { Viktor1, Viktor2 };
            Rozhok2.SubordinateEmployees = new List<IEmployee>() { Rozhok, Yuri };
            
            company.AddEmployee(Rozhok);
            company.AddEmployee(Rozhok2);

            company.Employees.ForEach(emp => Console.WriteLine( String.Format(
                "Employee: \n\t Name: {0} \n\t Boss: {1} \n\t Salary: {2} \n", 
                emp.Name, 
                emp.Boss != null ? emp.Boss.Name : "--",
                emp.CalcSalary() ))
                );

            Console.ReadKey();

        }
    }
}
