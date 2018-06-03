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
            Console.WriteLine(Viktor1.ToString());
            Employee Viktor2 = new Employee("Viktor2", new DateTime(2013, 10, 1));
            Console.WriteLine(Viktor2.ToString());

            Manager Yuri = new Manager("Yuri", new DateTime(2005, 5, 15));
            Sales Rozhok = new Sales("Rozhek", new DateTime(2014, 8, 1));
            Sales Rozhok2 = new Sales("Rozhek2", new DateTime(2014, 8, 1));

            Rozhok.SubordinateEmployees = new List<Employee>() { Viktor1, Viktor2 };
            Rozhok2.SubordinateEmployees = new List<Employee>() { Rozhok, Yuri };
            

            company.AddEmployee(Rozhok);
            company.AddEmployee(Rozhok2);

            Console.WriteLine();
            Console.WriteLine("//----------------------------");
            company.Employees.ForEach(emp => Console.WriteLine(emp));

            Console.WriteLine();
            Console.WriteLine("//----------------------------");
            Console.WriteLine("Total salary: " + company.GetTotalSalary());
            Console.ReadKey();

        }
    }
}
