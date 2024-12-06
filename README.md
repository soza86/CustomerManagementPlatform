This repository contains a complete solution for managing customers, leveraging modern .NET 8 technologies. The solution consists of four distinct projects:

1. Web API: A .NET 8 API for managing customer data using JWT Authentication and Microsoft Identity.
2. Blazor WebAssembly: A single-page application for the user interface.
3. Console Application: Contains solution for the problem of creating a method in a new class that takes either Manager or an Employee as a parameter and prints its name, without using a base class or reflection.
4. Test Project: A suite of automated tests to ensure application reliability

# **Projects Overview**

## Web API (WebApi)
  ### Features:
   * Built using .NET 8.
   * RESTful endpoints for managing customers (CRUD operations).
   * Uses Entity Framework Core with SQL Server.
   * Dependency injection for modularity and testability.
   * Swagger/OpenAPI documentation for testing and exploration.

### Run Instructions:
    * In the appsettings.json file, adjust the connection string to point your desired SQL server instance.
    * Run the command update-database in order to run the migrations and setup the database.
    * Run the solution.

## Blazor WebAssembly (BlazorWasm)
  ### Features:
* Built with Blazor WebAssembly.
* Fully client-side rendered SPA.
* Communicates with the Web API via HTTP clients.
* Provides a responsive user interface for managing customers.

### Run Instructions:
    * Use the following user to login:
       Username: dummyuser
       password: Sample@123
		* Setup as start up projects both API and client.

## Test Project
### Features:
* Includes unit tests and integration tests.
* Tests Web API endpoints.
* Built with xUnit and Moq for mocking dependencies.
