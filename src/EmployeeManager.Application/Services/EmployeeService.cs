using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new();
        private int _nextId = 1;

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("First name cannot be empty.", nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(employee));

            if (employee.Salary < 0)
                throw new ArgumentException("Salary cannot be negative.", nameof(employee));

            employee.Id = _nextId++;
            _employees.Add(employee);
        }

        public bool RemoveEmployee(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            var employee = FindById(id);
            if (employee == null)
                return false;

            return _employees.Remove(employee);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee? FindById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return _employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
