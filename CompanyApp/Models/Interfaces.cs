using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public interface IEmployee
    {
        string Name { get; set; }
        IBoss Boss { get; set; }
        double CalcSalary();
    }

    public interface IBoss : IEmployee
    {
        List<IEmployee> SubordinateEmployees { get; set; }
    }
}
