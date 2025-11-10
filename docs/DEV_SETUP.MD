# Development Setup - SchoolManagement

## Prerequisites
- .NET 7 SDK (download from https://dotnet.microsoft.com)
- SQL Server (LocalDB or SQL Server Developer)
- IDE: Visual Studio 2022/2023 or VS Code
- EF Core Tools: dotnet tool install --global dotnet-ef (optional)

## Local steps
1. Clone repo:
   git clone https://github.com/ShamsipourCS/SchoolManagement.git
2. Switch to main and pull:
   git checkout main
   git pull origin main
3. Build:
   dotnet restore
   dotnet build
4. Run API:
   dotnet run --project SchoolManagement.API/SchoolManagement.API.csproj

## Database
- Default connection string (example - use appsettings.Development.json):
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;"
  }

## Migrations
- Add migration:
  dotnet ef migrations add InitialCreate --project SchoolManagement.Infrastructure --startup-project SchoolManagement.API
- Apply migration:
  dotnet ef database update --project SchoolManagement.Infrastructure --startup-project SchoolManagement.API

## Notes
- Run tests: dotnet test
- Logging: logs/ (Serilog)