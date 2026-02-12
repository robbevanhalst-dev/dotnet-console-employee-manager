using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using EmployeeManager.Console.Menu;

IEmployeeService employeeService = new EmployeeService();
MenuService menuService = new MenuService(employeeService);

menuService.Run();
