using Xunit;
using EmployeeManager.Application.Services;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployee_ValidEmployee_AddsEmployeeSuccessfully()
        {
            // Arrange
            var service = new EmployeeService();
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Janssens",
                Role = EmployeeRole.Developer,
                Salary = 50000
            };

            // Act
            service.AddEmployee(employee);

            // Assert
            var allEmployees = service.GetAllEmployees();
            Assert.Single(allEmployees);
            Assert.Equal(1, employee.Id);
            Assert.Equal("Jan", employee.FirstName);
        }

        [Fact]
        public void AddEmployee_MultipleEmployees_AssignsIncrementalIds()
        {
            // Arrange
            var service = new EmployeeService();
            var employee1 = new Employee { FirstName = "Jan", LastName = "Janssens", Role = EmployeeRole.Developer, Salary = 50000 };
            var employee2 = new Employee { FirstName = "Piet", LastName = "Pieters", Role = EmployeeRole.Manager, Salary = 60000 };

            // Act
            service.AddEmployee(employee1);
            service.AddEmployee(employee2);

            // Assert
            Assert.Equal(1, employee1.Id);
            Assert.Equal(2, employee2.Id);
            Assert.Equal(2, service.GetAllEmployees().Count());
        }

        [Fact]
        public void AddEmployee_NullEmployee_ThrowsArgumentNullException()
        {
            // Arrange
            var service = new EmployeeService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddEmployee(null!));
        }

        [Fact]
        public void AddEmployee_EmptyFirstName_ThrowsArgumentException()
        {
            // Arrange
            var service = new EmployeeService();
            var employee = new Employee
            {
                FirstName = "",
                LastName = "Janssens",
                Role = EmployeeRole.Developer,
                Salary = 50000
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.AddEmployee(employee));
            Assert.Contains("First name", exception.Message);
        }

        [Fact]
        public void AddEmployee_EmptyLastName_ThrowsArgumentException()
        {
            // Arrange
            var service = new EmployeeService();
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "",
                Role = EmployeeRole.Developer,
                Salary = 50000
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.AddEmployee(employee));
            Assert.Contains("Last name", exception.Message);
        }

        [Fact]
        public void AddEmployee_NegativeSalary_ThrowsArgumentException()
        {
            // Arrange
            var service = new EmployeeService();
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Janssens",
                Role = EmployeeRole.Developer,
                Salary = -1000
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.AddEmployee(employee));
            Assert.Contains("Salary", exception.Message);
        }

        [Fact]
        public void RemoveEmployee_ExistingEmployee_RemovesSuccessfully()
        {
            // Arrange
            var service = new EmployeeService();
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Janssens",
                Role = EmployeeRole.Developer,
                Salary = 50000
            };
            service.AddEmployee(employee);

            // Act
            var result = service.RemoveEmployee(employee.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(service.GetAllEmployees());
        }

        [Fact]
        public void RemoveEmployee_NonExistingEmployee_ReturnsFalse()
        {
            // Arrange
            var service = new EmployeeService();

            // Act
            var result = service.RemoveEmployee(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveEmployee_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var service = new EmployeeService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.RemoveEmployee(0));
            Assert.Throws<ArgumentException>(() => service.RemoveEmployee(-1));
        }

        [Fact]
        public void RemoveEmployee_RemovesCorrectEmployee_WhenMultipleExist()
        {
            // Arrange
            var service = new EmployeeService();
            var employee1 = new Employee { FirstName = "Jan", LastName = "Janssens", Role = EmployeeRole.Developer, Salary = 50000 };
            var employee2 = new Employee { FirstName = "Piet", LastName = "Pieters", Role = EmployeeRole.Manager, Salary = 60000 };
            var employee3 = new Employee { FirstName = "Marie", LastName = "Maris", Role = EmployeeRole.Designer, Salary = 55000 };
            
            service.AddEmployee(employee1);
            service.AddEmployee(employee2);
            service.AddEmployee(employee3);

            // Act
            var result = service.RemoveEmployee(employee2.Id);

            // Assert
            Assert.True(result);
            Assert.Equal(2, service.GetAllEmployees().Count());
            Assert.Null(service.FindById(employee2.Id));
            Assert.NotNull(service.FindById(employee1.Id));
            Assert.NotNull(service.FindById(employee3.Id));
        }
    }
}
