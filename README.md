# Employee Manager – Console Application

A structured .NET console application built with layered architecture principles.  
This project demonstrates clean code practices, separation of concerns, and basic domain-driven design concepts.

---

## Project Goal

The goal of this project is to implement a simple employee management system while applying professional project structure and architectural separation.

The application allows users to manage employees via a console interface.

---

## Clean Architecture

This solution follows a layered structure:

EmployeeManager.sln  
- EmployeeManager.Console (Presentation layer)
- EmployeeManager.Application (Business logic layer)
- EmployeeManager.Domain (Domain models)

### Layer Responsibilities

- **Console**
  - Handles user input/output
  - Displays menu options
  - Calls application services

- **Application**
  - Contains business logic
  - Implements employee management operations
  - Uses interfaces for abstraction

- **Domain**
  - Contains core entities and enums
  - No external dependencies

This separation ensures scalability and maintainability.

---

## Functionalities

- Add new employees
- Remove employees
- View all employees
- Search employee by ID
- Basic input validation

---

## Technologies Used

- C#
- .NET 9 (or your version)
- Visual Studio 2022
- Git & GitHub

---

## How to Run

1. Clone the repository: https://github.com/robbevanhalst-dev/dotnet-console-employee-manager.git
   
3. Open the solution in **Visual Studio 2022**

4. Set `EmployeeManager.Console` as the startup project

5. Run the application

---

## Concepts Demonstrated

- Layered Architecture
- Separation of Concerns
- Object-Oriented Programming (OOP)
- Basic Dependency Management
- Clean Code Principles

---

## Possible Future Improvements

- Add persistence (database support)
- Introduce dependency injection
- Add unit tests
- Implement logging

---

## Author

Vanhalst Robbe

Junior .NET Developer  
GitHub: https://github.com/robbevanhalst-dev

