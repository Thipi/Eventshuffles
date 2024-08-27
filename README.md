# Eventshuffle Application

## Overview

Eventshuffle is an ASP.NET Core application for managing events and voting on dates. This README provides instructions on how to set up and run the application.

## Prerequisites

- [Visual Studio 2022 or later](https://visualstudio.microsoft.com/) with .NET 8.0 SDK
- [SQLite](https://www.sqlite.org/download.html) (optional for local testing)

## Getting Started

### 1. Clone the Repository
Navigate to the project root.

### 2. Restore NuGet Packages
Open the solution in Visual Studio and restore the NuGet packages:

Go to Tools > NuGet Package Manager > Package Manager Console.

Run the following command:
Update-Package -Reinstall

### 3. Update the Database
In the Package Manager Console, update the database to apply any pending migrations:

Run the following command:
Update-Database

This will create or update the SQLite database schema according to the latest migrations.

### 4. Run the Application
To run the application locally:

cd Eventshuffle.Api
dotnet run

### 6. API Endpoints
Here are the available API endpoints:

Create Event: POST /api/v1/event/create

List Events: GET /api/v1/event/list

Get Event By ID: GET /api/v1/event/{id}

Vote: POST /api/v1/event/vote

Get Event Results: GET /api/v1/event/{id}/results


To test with swagger: http://localhost:5145/swagger/

### 6. Running Tests
To run tests
dotnet run

Troubleshooting
Database Not Updating: Ensure you have the correct project selected(Eventshuffle.Infra) in the Package Manager Console when running Update-Database.
Missing Dependencies: Make sure all NuGet packages are restored

## Project Structure

- **Eventshuffle.Api**: Main API project.
- **Eventshuffle.Application**: Application layer for handling business logic.
- **Eventshuffle.Infra**: Infrastructure layer containing the SQLite DbContext.
- **Eventshuffle.Tests**: Unit and integration tests.
