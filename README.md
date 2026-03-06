# Employee Management API

## Overview

Employee Management API is a RESTful backend service built with **ASP.NET Core** that manages employees, departments, projects, and employee position history.

This project was developed as part of a **.NET Backend Technical Assessment** and demonstrates modern backend practices including **Clean Architecture, CQRS, SOLID principles, and secure API design**.

The API supports:

* Employee CRUD operations
* Authentication and authorization using JWT
* Role-based access control
* Employee position history tracking
* Database persistence using Entity Framework Core
* Middleware for logging and exception handling

---

# Key Features

* Clean Architecture implementation
* CQRS pattern for command and query separation
* JWT authentication and role-based authorization
* FluentValidation pipeline
* Global exception handling middleware
* Logging middleware
* Entity Framework Core with migrations
* Modular layered architecture
* Scalable and maintainable codebase

---

# Architecture

The project follows **Clean Architecture** to ensure separation of concerns, maintainability, and scalability.

```
API
 ↓
Application
 ↓
Domain

Infrastructure
Persistence
```

## Layer Responsibilities

### Domain

Contains core business entities and domain interfaces.

Examples:

* Employee
* PositionHistory
* Domain interfaces

This layer **does not depend on any other layer**.

---

### Application

Contains application logic and use cases.

Includes:

* Commands
* Queries
* DTOs
* Validation
* Pipeline behaviors

This layer orchestrates business workflows.

---

### Persistence

Handles database interaction using **Entity Framework Core**.

Includes:

* DbContext
* Entity configurations
* Migrations
* Repository implementations

---

### Infrastructure

Contains integrations with external systems and service implementations.

Examples:

* Authentication services
* Token generation

---

### API

Responsible for exposing REST endpoints.

Includes:

* Controllers
* Middleware
* Dependency injection
* Authentication configuration

---

# Design Principles

The project follows **SOLID principles** to create maintainable and extensible software.

| Principle             | Implementation                                               |
| --------------------- | ------------------------------------------------------------ |
| Single Responsibility | Classes have a single responsibility                         |
| Open/Closed           | Strategies and handlers allow extension without modification |
| Liskov Substitution   | Interfaces used for abstractions                             |
| Interface Segregation | Small focused interfaces                                     |
| Dependency Inversion  | Dependencies depend on abstractions                          |

---

# Architectural and Design Patterns

The project implements several design patterns to ensure scalability and maintainability.

---

## Clean Architecture

Clean Architecture separates concerns into independent layers.

Benefits:

* Independent business logic
* Easier testing
* Better maintainability
* Clear dependency flow

---

## CQRS (Command Query Responsibility Segregation)

The project separates **read and write operations**.

Commands modify the system state.

Examples:

```
CreateEmployeeCommand
UpdateEmployeeCommand
DeleteEmployeeCommand
```

Queries retrieve data.

Examples:

```
GetEmployeesQuery
GetEmployeeByIdQuery
```

Benefits:

* clearer responsibilities
* easier scalability
* better performance optimization

---

## Repository Pattern

Repositories abstract the data access layer.

Example:

```
IEmployeeRepository
EmployeeRepository
```

Benefits:

* decouples business logic from persistence
* improves testability
* simplifies data access

---

## Mediator Pattern

Commands and queries are handled by dedicated handlers.

Examples:

```
CreateEmployeeCommandHandler
GetEmployeesQueryHandler
```

Controllers delegate work to handlers instead of implementing business logic.

Benefits:

* reduces controller complexity
* decouples application layers

---

## Pipeline Behavior Pattern

Used to implement cross-cutting concerns.

Examples:

* ValidationPipelineBehavior
* LoggingPipelineBehavior

These act similarly to middleware within the application layer.

---

# Authentication and Authorization

The API uses **JWT (JSON Web Tokens)** for authentication.

Authentication flow:

```
Client
 ↓
AuthController
 ↓
Validate credentials
 ↓
Generate JWT Token
 ↓
Return token to client
```

Roles implemented:

| Role  | Permissions                   |
| ----- | ----------------------------- |
| Admin | Full CRUD access to employees |
| User  | Read-only access              |

Example authorization:

```
[Authorize(Roles = "Admin")]
```

---

# Middleware

Custom middleware components are implemented.

### RequestLoggingMiddleware

Logs all incoming HTTP requests including method and endpoint.

---

### GlobalExceptionHandlingMiddleware

Handles unhandled exceptions and returns standardized error responses.

Example response:

```
{
    "type": "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/422",
    "title": "UnprocessableEntity",
    "status": 422,
    "instance": "/api/v1/employees/b88c9be1-aff1-4cfb-ac53-518f2c17c163d",
    "message": "UnprocessableEntity",
    "errors": [
        {
            "field": "id",
            "message": "The value 'b88c9be1-aff1-4cfb-ac53-518f2c17c163d' is not valid."
        }
    ],
    "traceId": "0HNJRJ6Q9OFSN:00000004"
}

or good response:

{
    "type": "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200",
    "title": "OK",
    "status": 200,
    "detail": "",
    "instance": "/api/v1/employees/b88c9be1-aff1-4cfb-ac53-518f2c17c163",
    "message": "Employee found",
    "data": {
        "mapped": {
            "id": "b88c9be1-aff1-4cfb-ac53-518f2c17c163",
            "name": "Jane Smith",
            "currentPosition": "RegularEmployee",
            "salary": 3500.00
        },
        "bonus": 350.00
    },
    "errors": []
}
```

---

# Database

The project uses **SQL Server with Entity Framework Core**.

Main entities:

```
Employees
Departments
Projects
PositionHistory
```

Relationship example:

```
Employee
 ├── PositionHistory
 └── EmployeeProjects
        └── Projects
```

---

# Entity Framework Migrations

Migration command used:

```
dotnet ef migrations add InitialCreate \
--project EmployeeManagement.Persistence \
--startup-project EmployeeManagement.API
```

To apply migrations:

```
dotnet ef database update
```

---

# API Endpoints

## Employees

```
GET /api/employees
GET /api/employees/{id}
POST /api/employees
PUT /api/employees/{id}
DELETE /api/employees/{id}
```

---

## Authentication

```
POST /api/auth/login
POST /api/auth/register(I will do make in the future)
```

---

# Running the Project

## 1 Clone the repository

```
git clone https://github.com/yilmarvegag/employee-management-api
```

---

## 2 Navigate to project folder and change of connection to SQL Server(in appsettings.Development.json)

```
cd employee-management-api
```

---

## 3 Apply migrations(It is not actually necessary, since it does so on its own.)

```
dotnet ef database update
```

---

## 4 Run the API or IDE Microsoft Visual Studio

```
dotnet run
```

The API will start on:

[https://localhost:7183](https://localhost:7183/swagger/index.html)

Swagger will be available at:

```
/swagger
```
<img width="1646" height="954" alt="image" src="https://github.com/user-attachments/assets/690fd83f-5835-4a21-8625-b635f73a727e" />

---

# Technologies Used

* ASP.NET Core
* Entity Framework Core
* SQL Server
* JWT Authentication
* FluentValidation
* CQRS
* Clean Architecture

---

# Project Structure

```
EmployeeManagement
│
├── EmployeeManagement.API
├── EmployeeManagement.Application
├── EmployeeManagement.Domain
├── EmployeeManagement.Infrastructure
├── EmployeeManagement.Persistence
│
├── EmployeeManagement.sln
└── README.md
```
# Future Improvements

- Unit tests with xUnit
- Integration tests
- Docker support
- CI/CD pipeline


# References and Learning Resources

The implementation of this project was inspired(or used) by industry best practices and documentation from trusted technical resources.

The following sources were used to guide architecture decisions, patterns, and implementation approaches.

---

## Microsoft Official Documentation

Microsoft documentation was used as the primary reference for ASP.NET Core and Entity Framework best practices.

* ASP.NET Core Documentation
  https://learn.microsoft.com/en-us/aspnet/core/security/authentication/
  https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/
  https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
  
* Best implementation with bearer
  https://learn.microsoft.com/en-us/answers/questions/5646974/swagger-addsecurityrequirement-fails-after-migrati

* Clean Architecture in ASP.NET Core
  https://learn.microsoft.com/en-us/dotnet/architecture/

* Microsoft Guide: CQRS Pattern
  https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs

* Refactoring Guru
  https://refactoring.guru/es/design-patterns

* FluentValidation
  https://docs.fluentvalidation.net/en/latest/
---

## JWT Authentication

JWT implementation references:

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0

---

## Community Articles and Engineering Blogs

The following engineering blogs and community resources were consulted for best practices and implementation strategies:

* Dev.to Engineering Articles
  [https://dev.to](https://dev.to/ravivis13370227/clean-architecture-in-net-application-step-by-step-2ol0)

* Medium Engineering Articles
  [https://medium.com](https://medium.com/@roshikanayanadhara/clean-architecture-in-net-a-practical-guide-with-examples-817568b3f42e)
  [CQRS Approach](https://medium.com/@compileandconquer/clean-architecture-in-net-infrastructure-presentation-layers-69b6fb37ac3f)

* Atalupadhyay
  [Clean Architecture](https://atalupadhyay.wordpress.com/2025/11/15/clean-architecture-with-asp-net-core-10/)

---

## Purpose of These References

These resources were used to ensure that the implementation follows modern software engineering practices.

---

# Author

**Yilmar Vega**

Full Stack Developer

GitHub
https://github.com/yilmarvegag

[Linkedin](https://www.linkedin.com/in/yilmarvegag/)

