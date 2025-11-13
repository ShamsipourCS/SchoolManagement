# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Architecture Overview

This is a **Clean Architecture** ASP.NET 7 project for school management with clear separation of concerns across four layers:

### Layer Dependencies (Dependency Inversion)
```
API → Application → Domain ← Infrastructure
```

- **Domain** (center): Contains core business logic with zero external dependencies
- **Application**: Business use cases and orchestration (depends on Domain only)
- **Infrastructure**: Data persistence, EF Core, external services (depends on Domain)
- **API**: REST endpoints, DI configuration, middleware (depends on all layers)

### Domain Layer (`SchoolManagement.Domain`)
Core business entities and contracts with no framework dependencies:

- **Entities**: `Student`, `Teacher`, `Course`, `Enrollment` (all inherit from `BaseEntity`)
  - `BaseEntity` provides: `Id`, `CreatedAt`, `UpdatedAt`
  - Entities use virtual navigation properties for EF Core lazy loading

- **Value Objects**: `Email`, `Grade` (immutable, equality by value)
  - Implement `IEquatable<T>` with validation in constructors
  - Example: `Email` validates format, throws `ArgumentException` on invalid input

- **Repository Interfaces**:
  - `IGenericRepository<T>`: Base CRUD operations (GetAll, GetById, Add, Update, Delete, Exists)
  - Specific repositories: `IStudentRepository`, `ITeacherRepository`, `ICourseRepository`, `IEnrollmentRepository`
  - `IUnitOfWork`: Transaction management across repositories with `SaveChangesAsync()`

### Infrastructure Layer (`SchoolManagement.Infrastructure`)
Data access implementation using EF Core 7:

- **DbContext**: `SchoolDbContext` with DbSets for all entities
  - Uses Fluent API configurations via `IEntityTypeConfiguration<T>`
  - Configuration classes in `Persistence/Configurations/` (e.g., `StudentConfiguration.cs`)
  - Relationships defined with Fluent API (cascade delete, foreign keys, constraints)

- **Entity Configurations** enforce:
  - Max lengths (e.g., `FullName` max 200 chars)
  - Required fields
  - Default values (e.g., `IsActive` defaults to true)
  - Relationship mappings with proper delete behaviors

### Application Layer (`SchoolManagement.Application`)
Currently minimal - designed for CQRS/MediatR patterns, use cases, DTOs, validators.

### API Layer (`SchoolManagement.API`)
ASP.NET Core Web API with Swagger/OpenAPI support. Entry point is `Program.cs`.

## Development Commands

### Build & Run
```bash
# Restore dependencies and build solution
dotnet restore
dotnet build

# Run the API project
dotnet run --project SchoolManagement.API/SchoolManagement.API.csproj

# Run tests
dotnet test
```

### Database Migrations
EF Core migrations require both Infrastructure (where DbContext lives) and API (startup project):

```bash
# Add new migration
dotnet ef migrations add <MigrationName> \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.API

# Apply migrations to database
dotnet ef database update \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.API

# Remove last migration (if not applied)
dotnet ef migrations remove \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.API
```

### Testing
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Run tests from specific project
dotnet test SchoolManagement.Tests/SchoolManagement.Tests.csproj
```

## Key Patterns & Conventions

### Repository Pattern with Unit of Work
- Repositories abstract data access logic
- Unit of Work manages transactions across multiple repositories
- All repository methods use async/await (suffix with `Async`)

### Entity Relationships
- **Student ↔ Enrollment**: One-to-many (cascade delete)
- **Course ↔ Enrollment**: One-to-many (cascade delete)
- **Teacher ↔ Course**: One-to-many

### Value Objects
- Immutable by design (readonly properties)
- Validation in constructor throws `ArgumentException`
- Override `Equals()`, `GetHashCode()`, `ToString()`
- Use `sealed` keyword to prevent inheritance

### Naming Conventions
- Entities: PascalCase, singular (e.g., `Student`, not `Students`)
- DbSet properties: PascalCase, plural (e.g., `Students`, `Courses`)
- Async methods: Suffix with `Async`
- Private fields: camelCase (standard C# convention)

## Technology Stack
- **.NET 7**: Target framework for all projects
- **EF Core 7**: ORM with SQL Server provider
- **xUnit**: Testing framework
- **Serilog**: Structured logging (logs stored in `logs/` directory)
- **JWT**: Planned for authentication
- **Swagger/OpenAPI**: API documentation (available at `/swagger` in development)

## Database Configuration
Connection string should be defined in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;"
  }
}
```

## Project Status
The project has established:
- ✅ Domain entities with relationships
- ✅ Value objects with validation
- ✅ Repository interfaces
- ✅ EF Core DbContext with Fluent API configurations
- ⏳ Repository implementations (pending)
- ⏳ Unit of Work implementation (pending)
- ⏳ Application layer use cases (pending)
- ⏳ API controllers beyond template (pending)
- ⏳ Authentication/Authorization (pending)

## Important Notes
- Always use `--project` and `--startup-project` flags for EF Core commands
- Repository methods return `Task<T>` or `Task<IEnumerable<T>>` for async operations
- Navigation properties use `virtual` keyword to enable lazy loading
- Entity validation should happen in value objects or domain layer, not in controllers
