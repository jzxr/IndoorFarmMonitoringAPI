# IndoorFarmMonitoringAPI

A .NET 6 Web API project for monitoring indoor farm sensor data. Built with ASP.NET Core, PostgreSQL, and Entity Framework Core. Includes unit testing using xUnit and Moq.

---

## Features

- RESTful API for managing plant sensor data
- PostgreSQL with Entity Framework Core
- Swagger UI for testing endpoints
- Unit tests using xUnit + Moq + InMemory DB
- Structured in service/data/controller layers

---

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- [pgAdmin 4](https://www.pgadmin.org/download/) _(optional, GUI for PostgreSQL)_
- [Git](https://git-scm.com/)

---

## PostgreSQL Setup

### 1. Install & Start PostgreSQL (macOS Homebrew)

```bash
brew install postgresql@14
brew services start postgresql@14
```

## Access PostgreSQL CLI
```bash
psql -U postgres
```
Set password and create the database:

```bash
ALTER USER postgres WITH PASSWORD 'farm_pass';
CREATE DATABASE farm_db;
```

Or use pgAdmin to create the database farm_db.

## Configuration

Update IndoorFarmMonitoringAPI/appsettings.json:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=farm_db;Username=postgres;Password=farm_pass"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

# Running Unit Tests

1. Go to the test project:
```bash
cd IndoorFarmMonitoringAPI.Tests
```
2. Run the tests:
```bash
dotnet test
```

# Run the API

```bash
dotnet run
```

Visit:
-  http://localhost:5000/swagger
- http://localhost:5000/api/plantsensordata (example endpoint)

# Entity Framework Migrations

From the project root:
```bash
cd IndoorFarmMonitoringAPI

dotnet ef migrations add InitialCreate
dotnet ef database update
```