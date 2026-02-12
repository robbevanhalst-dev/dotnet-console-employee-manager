using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Console.Menu
{
    internal class MenuService
    {
        private readonly IEmployeeService _employeeService;

        public MenuService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                string? choice = GetInput("Kies een optie: ");
                
                running = ProcessChoice(choice);
            }
        }

        private void ShowMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("=== Employee Manager ===");
            System.Console.WriteLine("1. Voeg werknemer toe");
            System.Console.WriteLine("2. Verwijder werknemer");
            System.Console.WriteLine("3. Toon alle werknemers");
            System.Console.WriteLine("4. Zoek werknemer op ID");
            System.Console.WriteLine("5. Afsluiten");
            System.Console.WriteLine("========================");
        }

        private string? GetInput(string prompt)
        {
            System.Console.Write(prompt);
            return System.Console.ReadLine();
        }

        private bool ProcessChoice(string? choice)
        {
            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    RemoveEmployee();
                    break;
                case "3":
                    ShowAllEmployees();
                    break;
                case "4":
                    FindEmployee();
                    break;
                case "5":
                    System.Console.WriteLine("Tot ziens!");
                    return false;
                default:
                    System.Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                    break;
            }
            return true;
        }

        private void AddEmployee()
        {
            try
            {
                System.Console.WriteLine("\n--- Voeg Werknemer Toe ---");
                
                string? firstName = GetInput("Voornaam: ");
                string? lastName = GetInput("Achternaam: ");
                
                System.Console.WriteLine("Beschikbare rollen:");
                var roles = Enum.GetValues<EmployeeRole>();
                for (int i = 0; i < roles.Length; i++)
                {
                    System.Console.WriteLine($"{i + 1}. {roles[i]}");
                }
                
                string? roleInput = GetInput("Kies een rol (nummer): ");
                if (!int.TryParse(roleInput, out int roleIndex) || roleIndex < 1 || roleIndex > roles.Length)
                {
                    System.Console.WriteLine("Ongeldige rol.");
                    return;
                }
                
                string? salaryInput = GetInput("Salaris: ");
                if (!decimal.TryParse(salaryInput, out decimal salary))
                {
                    System.Console.WriteLine("Ongeldig salaris.");
                    return;
                }

                var employee = new Employee
                {
                    FirstName = firstName ?? string.Empty,
                    LastName = lastName ?? string.Empty,
                    Role = roles[roleIndex - 1],
                    Salary = salary
                };

                _employeeService.AddEmployee(employee);
                System.Console.WriteLine($"Werknemer {employee.FirstName} {employee.LastName} is toegevoegd met ID {employee.Id}.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Fout: {ex.Message}");
            }
        }

        private void RemoveEmployee()
        {
            try
            {
                System.Console.WriteLine("\n--- Verwijder Werknemer ---");
                string? idInput = GetInput("Voer ID in: ");
                
                if (!int.TryParse(idInput, out int id))
                {
                    System.Console.WriteLine("Ongeldig ID.");
                    return;
                }

                bool removed = _employeeService.RemoveEmployee(id);
                if (removed)
                {
                    System.Console.WriteLine($"Werknemer met ID {id} is verwijderd.");
                }
                else
                {
                    System.Console.WriteLine($"Werknemer met ID {id} niet gevonden.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Fout: {ex.Message}");
            }
        }

        private void ShowAllEmployees()
        {
            System.Console.WriteLine("\n--- Alle Werknemers ---");
            var employees = _employeeService.GetAllEmployees();
            
            if (!employees.Any())
            {
                System.Console.WriteLine("Geen werknemers gevonden.");
                return;
            }

            foreach (var employee in employees)
            {
                DisplayEmployee(employee);
            }
        }

        private void FindEmployee()
        {
            try
            {
                System.Console.WriteLine("\n--- Zoek Werknemer ---");
                string? idInput = GetInput("Voer ID in: ");
                
                if (!int.TryParse(idInput, out int id))
                {
                    System.Console.WriteLine("Ongeldig ID.");
                    return;
                }

                var employee = _employeeService.FindById(id);
                if (employee != null)
                {
                    DisplayEmployee(employee);
                }
                else
                {
                    System.Console.WriteLine($"Werknemer met ID {id} niet gevonden.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Fout: {ex.Message}");
            }
        }

        private void DisplayEmployee(Employee employee)
        {
            System.Console.WriteLine($"ID: {employee.Id} | {employee.FirstName} {employee.LastName} | Rol: {employee.Role} | Salaris: €{employee.Salary:N2}");
        }
    }
}
