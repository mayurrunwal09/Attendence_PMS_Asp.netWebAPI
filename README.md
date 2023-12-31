
# Attendance Management System (PMS) - ASP.NET Core Web API
## UI(Front End) of Project in React
https://github.com/mayurrunwal09/Attendence_PMS_In_React.git

## Overview
The Attendance Management System (PMS) is a comprehensive web application developed using ASP.NET Core Web API. The project follows the Onion Architecture, dividing the codebase into distinct layers: Domain Layer, Repository and Services Layer, and Web API Layer. This architecture promotes separation of concerns, maintainability, and testability.

## Key Features
Modular Structure: The project is organized into various modules, including Attendence, Break, Clockout, Event, Finish Break, Leave, ManualRequest, Report, Session, User, and UserType.

Service Layer: Each module has dedicated services with corresponding interfaces. This separation allows for easy scalability, maintenance, and unit testing.

Authentication and Authorization using JWT token.

## Project Structure
The project is structured as follows:

Domain Layer: Contains the domain models that represent the core entities in the system, such as User, UserType, Attendence, Break, Clockout, Event, Finish Break, Leave, ManualRequest, Report, Session, etc.

Repository and Services Layer: This layer houses the database context (MainDBContext), repositories, and services for each module. The repositories handle data access, and services encapsulate the business logic.

Web API Layer: The API controllers interact with the services to handle HTTP requests and responses. Swagger is integrated for easy API documentation.


## Modules
### Attendence Module
Manage Clockin time for present.

Break Module
Manages breaks and associated functionalities.

Clockout Module
Tracks clock-out times for users.

Event Module
Deals with events, including creation and management.

Finish Break Module
Manages the finishing of breaks.

Leave Module
Handles leave requests and approvals.

ManualRequest Module
Processes manual attendance requests.

Report Module
Generates and manages reports related to attendance and breaks with calculation Productive time and actual time and calculation of Break duration.

Session Module
Manages user sessions during events.

User Module
Manages user-related operations.

UserType Module
Defines and categorizes user types.

## Dependencies
Entity Framework Core: Utilized for data access and database management.

Swagger: Integrated for API documentation.

JWT Authentication: Ensures secure authentication and authorization.

## How to Run
Clone the repository.
Set up the database connection in appsettings.json.
Run migrations to create the database using dotnet ef database update.
Launch the application using dotnet run.
Access the Swagger UI at /swagger for API documentation.

## Project Layer
Project is build in Asp.Net core WebAPI using onion architecture which contain 3 main layer Domain,Infrastrure And WebAPI layer.
### Domain Layer
     -The Domain Layer contain all the models of project.
### Infrastructure Layer
      -The Infrastructure layer contain Repository and Services.
      -In this layer DBContext class conatin all the relationship between the all the modules.
      -In this layer there is services layer for Generic Service and Custom Services for all modules.

### WebAPI Layer
        -The Web API layer contains Controllers,Program.cs file and Middleware like JWT Authentication.
        -The program.cs files contains all the dependency injection and middleware of project.




