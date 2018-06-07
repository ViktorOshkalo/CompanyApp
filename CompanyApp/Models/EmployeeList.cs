using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    class EmployeeList : IEmployeeList
    {
        private List<IEmployee> _employees = new List<IEmployee>();

        public void Add(IEmployee employee)
        {
            if (!_employees.Contains(employee))
            {
                _employees.Add(employee);
                AddedEmployee?.Invoke(this, employee);
            }
        }

        public void Add(params IEmployee[] employees)
        {
            foreach (IEmployee emp in employees)
                Add(emp);
        }

        public bool Remove(IEmployee employee)
        {
            if (_employees.Remove(employee))
            {
                RemovedEmployee?.Invoke(this, employee);
                return true;
            }
            return false;
        }


        public void Remove(params IEmployee[] employees)
        {
            foreach (IEmployee emp in employees)
                Remove(emp);
        }

        public IEnumerator GetEnumerator()
        {
            return _employees.GetEnumerator();
        }

        IEnumerator<IEmployee> IEnumerable<IEmployee>.GetEnumerator()
        {
            return _employees.GetEnumerator();
        }

        public event EventHandler<IEmployee> AddedEmployee;
        public event EventHandler<IEmployee> RemovedEmployee;
    }

    class SubordinateList : EmployeeList
    {
        public IBoss Owner { get; private set; }

        public SubordinateList(IBoss owner)
        {
            Owner = owner;
        }

    }
}
