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
        IBoss Boss { get; set; }
        double CalcSalary();
    }

    public interface IBoss : IEmployee
    {
        IList<IEmployee> SubordinateEmployees { get; set; }
    }
}
