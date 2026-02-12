using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        bool RemoveEmployee(int id);
        IEnumerable<Employee> GetAllEmployees();
        Employee? FindById(int id);
    }
}
