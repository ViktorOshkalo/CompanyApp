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
            // some actions 

            Company company = Company.CompanyInstance;

            IEmployee Viktor1 = new Employee("Viktor1", new DateTime(2013, 10, 1));
            IEmployee Viktor2 = new Employee("Viktor2", new DateTime(2013, 10, 1));

            IBoss Yuri = new Manager("Yuri", new DateTime(2005, 5, 15));
            IBoss Alex = new Sales("Alex", new DateTime(2014, 8, 1));
            IBoss Alex2 = new Sales("Alex2", new DateTime(2014, 8, 1));

            Alex.SubordinateEmployees.Add(Viktor1);
            Alex.SubordinateEmployees.Add(Viktor2);

            Alex2.SubordinateEmployees.Add(Alex);
            Alex2.SubordinateEmployees.Add(Yuri);

            company.AddEmployee(Alex);
            company.AddEmployee(Alex2);


            //----------------

            IEmployee Viktor3 = new Employee("Viktor333", new DateTime(2010, 10, 1));
            Alex2.SubordinateEmployees.Add(Viktor3);

            //Rozhok2.SubordinateEmployees.Add(Viktor3);    // error: collection is read only
            //company.Employees.Add(Viktor3);               // error: collection is read only

            //-----------------

            // Alex.Boss = Alex;    // throw exception

            //-----------------

            company.RemoveEmployee(Viktor3);

            //Viktor3.Boss = Alex;
            //Alex.SubordinateEmployees.Add(Viktor3);


            company.Employees.ToList().ForEach(emp => Console.WriteLine(String.Format(
                "Employee: \n\t Name:\t\t {0} \n\t Start date:\t {1} \n\t Boss:\t\t {2} \n\t Salary:\t {3} \n",
                emp.Name,
                emp.StartWorkingDate.ToShortDateString(),
                emp.Boss != null ? emp.Boss.Name : "--",
                emp.CalcSalary()))
                );

            Console.WriteLine("-----------//------------\n");
            Console.WriteLine("Total salary: " + company.GetTotalSalary());
            Console.ReadKey();
        }
    }
}
