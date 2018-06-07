using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{

    public interface IEmployee
    {
        string Name { get; set; }
        DateTime StartWorkingDate { get; }
        double CalcSalary();
        IBoss Boss { get; set; }
    }

    public interface IBoss : IEmployee
    {
        IEmployeeList SubordinateEmployees { get; }
    }

    public interface IEmployeeList : IEnumerable<IEmployee>
    {
        void Add(params IEmployee[] employees);
        void Remove(params IEmployee[] employees);
        event EventHandler<IEmployee> AddedEmployee;
        event EventHandler<IEmployee> RemovedEmployee;
    }

}
