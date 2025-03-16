# Project Documentation


## Table of Contents
- [Introduction](#introduction)
- [Aspire](#aspire)
- [Key Logic Locations](#key-logic-locations)
- [Launching the Project](#launching-the-project)
- [Creating New EF Migrations](#creating-new-ef-migrations)
- [DB script](#db-script)


## Introduction
This project is designed to showcase .NET DDD architecture. Domain-Driven Design (DDD) is a software development approach that focuses on modeling the business domain and its logic. It emphasizes collaboration between technical and domain experts to create a shared understanding of the domain, which is then reflected in the code. DDD helps in managing complex software projects by breaking them down into smaller, more manageable parts, each representing a specific aspect of the business domain.


## Aspire
Aspire is a key component of this project, designed to facilitate the development and testing of the application. It provides a set of tools and libraries that help streamline the development process, ensuring that the application adheres to best practices and standards. It handles all required services similar to docker-compose, providing a full local development experience. This means developers can run and test the entire application locally, with all dependencies and services configured and running as they would in a production environment.


## Used technologies
- **CQRS** 
- **Dependency injection** 
- **Mediator (MediatR)**
- **MassTransit**
- **Entity Framework Core**
- **Scalar (OpenApi UI)**: URL: webApiUrl/scalar
- **Blazor**


## Key Logic Locations
- **Main Application Logic**: `src/BitcoinApp.Application`
- **Infrastructure (DB connections, external services, etc.)**: `src/BitcoinApp.Infrasturcture`
- **Domain Models**: `src/BitcoinApp.Domain`
- **Controllers**: `src/BitcoinApp.WebApi`
- **Frontend**: `src/BitcoinApp.Web`


## Launching the Project
To launch the project, follow these steps:
1. Navigate to the project Root directory
2. Install the required dependencies:
    ```dotnet restore```
3. Start the application - docker (postman - via aspire documentation) and .net 9 SDK must be installed:
    ```
    dotnet run --project .\src\BitcoinApp.AppHost\BitcoinApp.AppHost.csproj 
    ```
4. Target Aspire dashboard UI URL should be shown in console (probably https://localhost:17167) - Aspire dashboard displays all available services and their endpoints (the main ones are "webApi" and "webFrontend")


## Creating New EF Migrations
To create new Entity Framework (EF) migrations, use the following commands:
1. Open a terminal and navigate to the Root directory
2. Add a new migration:
    ```
    dotnet ef migrations add <Migration-Name> --project .\src\BitcoinApp.Infrastructure\ --startup-project .\src\BitcoinApp.AppHost\
    ```
3. Update of the database will be applied on next project start

## DB script
Script for manual DB creation is located at root of this project: ```migrationScript.sql```
