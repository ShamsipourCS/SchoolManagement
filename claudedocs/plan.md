# SchoolManagement App - Complete Refactor Plan

**Target Framework:** .NET 7
**Team:** Shervin (Beginner), Esi (Beginner), Ali (Experienced)
**Approach:** Fresh start with Clean Architecture scaffold
**Database:** MSSQL with EF Core 7.0.20

---

## Table of Contents

- [A. High-Level Roadmap](#a-high-level-roadmap)
- [B-C. Feature-Based Task Breakdown with Developer Assignments](#b-c-feature-based-task-breakdown-with-developer-assignments)
- [D. Step-by-Step Walkthroughs](#d-step-by-step-walkthroughs)
- [E. Clean Architecture Scaffolding](#e-clean-architecture-scaffolding)
- [F. UnitOfWork & GenericRepository](#f-unitofwork--genericrepository)
- [G. Exception Middleware](#g-exception-middleware)
- [H. Logging with Serilog](#h-logging-with-serilog)
- [I. JWT Authentication](#i-jwt-authentication)
- [J. Testing & CI/CD](#j-testing--cicd)
- [K. Conventional Commits & PR Templates](#k-conventional-commits--pr-templates)
- [L. Git Workflow Example](#l-git-workflow-example)
- [M. Onboarding Notes for Beginners](#m-onboarding-notes-for-beginners)
- [N. Final Deliverables](#n-final-deliverables)

---

## A. High-Level Roadmap

### Phase 0: Project Setup & Scaffolding
**Duration:** 1 week
**Deliverables:**
- Clean .NET 7 solution with 5 projects
- Proper project references configured
- Git repository initialized with .gitignore
- README with setup instructions

### Phase 1: Domain Layer Foundation
**Duration:** 1 week
**Deliverables:**
- Core entities (Student, Teacher, Course, Enrollment)
- Domain interfaces (no implementations)
- Value objects (if needed)
- Domain layer completely independent of infrastructure

### Phase 2: Infrastructure Layer
**Duration:** 2 weeks
**Deliverables:**
- EF Core DbContext with Fluent API configurations
- Generic Repository pattern implementation
- Unit of Work pattern implementation
- Database migrations and seed data
- Separation between domain entities and EF entities

### Phase 3: Application Layer
**Duration:** 2 weeks
**Deliverables:**
- DTOs for all entities
- Service interfaces and implementations
- AutoMapper/Mappr configuration
- Basic validation logic
- Dependency injection registration

### Phase 4: API Layer & Middleware
**Duration:** 2 weeks
**Deliverables:**
- RESTful API controllers
- Global exception handling middleware
- Request/response logging middleware
- Swagger/OpenAPI documentation
- CORS configuration (secure)

### Phase 5: Cross-Cutting Concerns
**Duration:** 1.5 weeks
**Deliverables:**
- Serilog integration (file + console)
- JWT authentication infrastructure
- Authorization policies
- User registration/login endpoints

### Phase 6: Testing & CI/CD
**Duration:** 1 week
**Deliverables:**
- xUnit test project setup
- Sample unit tests
- Sample integration tests
- GitHub Actions CI/CD pipeline

### Phase 7: Optional Enhancement (MediatR/CQRS)
**Duration:** 1.5 weeks
**Deliverables:**
- MediatR package integration
- CQRS command/query handlers
- Refactor services to use MediatR pipeline
- Validation behavior with FluentValidation

**Total Timeline:** 10-12 weeks for core implementation (Phases 0-6)

---

## B-C. Feature-Based Task Breakdown with Developer Assignments

### Phase 0: Project Setup & Scaffolding

#### Task 0.1: Create Solution Structure
**Assigned to:** Ali (Advanced)
**Branch:** `feature/scaffold-solution`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Create a new .NET 7 solution with Clean Architecture structure including 5 projects with proper dependencies.

**Acceptance Criteria:**
- [ ] Solution builds successfully
- [ ] All projects target .NET 7
- [ ] Project references configured correctly
- [ ] .gitignore includes bin/, obj/, *.user files

**Conventional Commit Examples:**
```bash
feat(scaffold): create .NET 7 solution with clean architecture structure
build(deps): add EF Core 7.0.20 packages to Infrastructure project
docs(readme): add initial setup instructions
```

**PR Title:** `feat: Initial Clean Architecture scaffold with .NET 7`

**PR Description Template:**
```markdown
## Summary
Creates the foundational solution structure with 5 projects following Clean Architecture principles.

## Changes
- Created Domain, Application, Infrastructure, API, and Tests projects
- Configured project references and dependencies
- Added .gitignore and README.md

## Testing
- [ ] Solution builds without errors
- [ ] All projects target .NET 7

## Checklist
- [ ] Code builds successfully
- [ ] No warnings
- [ ] README updated with setup steps
```

---

#### Task 0.2: Configure Git Repository
**Assigned to:** Esi (Beginner)
**Branch:** `chore/git-setup`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Initialize git repository, add .gitignore for .NET projects, create initial README.

**Acceptance Criteria:**
- [ ] Git repository initialized
- [ ] .gitignore excludes bin/, obj/, .vs/, *.user
- [ ] README.md has project description and tech stack

**Conventional Commit Examples:**
```bash
chore(git): initialize repository with .gitignore
docs(readme): add project overview and tech stack
```

**PR Title:** `chore: Initialize git repository with documentation`

---

#### Task 0.3: Setup Development Environment Documentation
**Assigned to:** Shervin (Beginner)
**Branch:** `docs/dev-environment`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Document required tools, installation steps, and how to run the project locally.

**Acceptance Criteria:**
- [ ] Prerequisites listed (.NET 7 SDK, SQL Server, IDE)
- [ ] Step-by-step setup instructions
- [ ] Database connection string configuration explained

**Conventional Commit Examples:**
```bash
docs(setup): add development environment requirements
docs(setup): add database configuration guide
```

**PR Title:** `docs: Add development environment setup guide`

---

### Phase 1: Domain Layer Foundation

#### Task 1.1: Create Core Domain Entities
**Assigned to:** Ali (Advanced)
**Branch:** `feature/domain-entities`
**Base Branch:** `main`
**Complexity:** Intermediate

**Description:**
Create domain entities (Student, Teacher, Course, Enrollment) without any infrastructure concerns or data annotations.

**Acceptance Criteria:**
- [ ] All entities inherit from BaseEntity (Id, audit fields)
- [ ] Navigation properties defined
- [ ] No EF Core attributes or DataAnnotations
- [ ] XML documentation comments included

**Conventional Commit Examples:**
```bash
feat(domain): add Student entity with navigation properties
feat(domain): add Teacher entity with course relationship
feat(domain): add Course entity with enrollments
feat(domain): add Enrollment entity as join table
refactor(domain): extract BaseEntity for common properties
```

**PR Title:** `feat: Add core domain entities (Student, Teacher, Course, Enrollment)`

---

#### Task 1.2: Define Domain Interfaces
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/domain-interfaces`
**Base Branch:** `feature/domain-entities`
**Complexity:** Beginner

**Description:**
Create repository interfaces in Domain layer (no implementations yet).

**Acceptance Criteria:**
- [ ] IGenericRepository<T> interface created
- [ ] IStudentRepository, ITeacherRepository, etc. extend IGenericRepository
- [ ] IUnitOfWork interface defined
- [ ] Interfaces in SchoolManagement.Domain/Interfaces/

**Conventional Commit Examples:**
```bash
feat(domain): add IGenericRepository interface
feat(domain): add entity-specific repository interfaces
feat(domain): add IUnitOfWork interface
```

**PR Title:** `feat: Define domain repository interfaces`

---

#### Task 1.3: Add Value Objects (if applicable)
**Assigned to:** Esi (Beginner)
**Branch:** `feature/value-objects`
**Base Branch:** `feature/domain-entities`
**Complexity:** Intermediate

**Description:**
Create value objects for domain concepts like Email, Grade (optional but recommended).

**Acceptance Criteria:**
- [ ] Email value object with validation
- [ ] Grade value object with range validation (0-100)
- [ ] Value objects are immutable
- [ ] Equality based on value, not reference

**Conventional Commit Examples:**
```bash
feat(domain): add Email value object with validation
feat(domain): add Grade value object with range checks
```

**PR Title:** `feat: Add domain value objects for Email and Grade`

---

### Phase 2: Infrastructure Layer

#### Task 2.1: Create EF Core Entities (Persistence Models)
**Assigned to:** Ali (Advanced)
**Branch:** `feature/ef-entities`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Create EF Core-specific entity classes that map domain entities to database, with data annotations and configurations.

**Acceptance Criteria:**
- [ ] Separate namespace: SchoolManagement.Infrastructure.Persistence.Entities
- [ ] Entities can be mapped from/to domain entities
- [ ] Include DataAnnotations for EF Core
- [ ] Keep domain layer clean and independent

**Conventional Commit Examples:**
```bash
feat(infra): add EF Core entity models
feat(infra): add entity configurations with Fluent API
```

**PR Title:** `feat: Add EF Core persistence entities with configurations`

---

#### Task 2.2: Create DbContext with Fluent API
**Assigned to:** Ali (Advanced)
**Branch:** `feature/dbcontext`
**Base Branch:** `feature/ef-entities`
**Complexity:** Advanced

**Description:**
Create SchoolDbContext with DbSets and Fluent API configurations for all entities.

**Acceptance Criteria:**
- [ ] DbContext inherits from DbContext
- [ ] All entities configured in OnModelCreating
- [ ] Relationships, indexes, and constraints defined
- [ ] Connection string from appsettings
- [ ] Delete behavior configured (Restrict)

**Conventional Commit Examples:**
```bash
feat(infra): create SchoolDbContext with entity configurations
config(infra): add fluent API configurations for relationships
config(infra): configure delete behavior and indexes
```

**PR Title:** `feat: Implement DbContext with Fluent API configurations`

---

#### Task 2.3: Implement Generic Repository
**Assigned to:** Esi (Beginner)
**Branch:** `feature/generic-repository`
**Base Branch:** `feature/dbcontext`
**Complexity:** Intermediate

**Description:**
Implement IGenericRepository<T> with common CRUD operations using DbContext.

**Acceptance Criteria:**
- [ ] Implements GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync
- [ ] Uses DbSet<T> for operations
- [ ] Async/await pattern throughout
- [ ] Proper exception handling

**Conventional Commit Examples:**
```bash
feat(infra): implement GenericRepository with CRUD operations
test(infra): add unit tests for GenericRepository methods
```

**PR Title:** `feat: Implement generic repository pattern`

---

#### Task 2.4: Implement Entity-Specific Repositories
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/entity-repositories`
**Base Branch:** `feature/generic-repository`
**Complexity:** Beginner

**Description:**
Implement StudentRepository, TeacherRepository, etc. with entity-specific queries.

**Acceptance Criteria:**
- [ ] Each repository extends GenericRepository
- [ ] Implements entity-specific interface
- [ ] Includes custom query methods (e.g., GetStudentWithEnrollments)
- [ ] Uses Include() for eager loading

**Conventional Commit Examples:**
```bash
feat(infra): implement StudentRepository with custom queries
feat(infra): implement TeacherRepository with course loading
feat(infra): implement CourseRepository with relationships
feat(infra): implement EnrollmentRepository with filtering
```

**PR Title:** `feat: Implement entity-specific repositories`

---

#### Task 2.5: Implement Unit of Work Pattern
**Assigned to:** Ali (Advanced)
**Branch:** `feature/unit-of-work`
**Base Branch:** `feature/entity-repositories`
**Complexity:** Advanced

**Description:**
Implement IUnitOfWork to coordinate multiple repository operations within a single transaction.

**Acceptance Criteria:**
- [ ] Exposes all repository properties
- [ ] Implements SaveChangesAsync for transaction commit
- [ ] Implements IDisposable for proper cleanup
- [ ] Manages DbContext lifetime

**Conventional Commit Examples:**
```bash
feat(infra): implement UnitOfWork pattern for transaction management
refactor(infra): integrate repositories with UnitOfWork
```

**PR Title:** `feat: Implement Unit of Work pattern`

---

#### Task 2.6: Create Database Migrations
**Assigned to:** Esi (Beginner)
**Branch:** `feature/initial-migration`
**Base Branch:** `feature/unit-of-work`
**Complexity:** Beginner

**Description:**
Create initial EF Core migration and seed data for development.

**Acceptance Criteria:**
- [ ] Initial migration creates all tables
- [ ] Seed data includes 2 teachers, 3 students, 3 courses
- [ ] Migration can be applied successfully
- [ ] Database created in SQL Server

**Conventional Commit Examples:**
```bash
feat(infra): add initial database migration
feat(infra): add seed data for development environment
```

**PR Title:** `feat: Add initial database migration with seed data`

---

### Phase 3: Application Layer

#### Task 3.1: Create DTOs (Data Transfer Objects)
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/dtos`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Create request/response DTOs for all entities to avoid exposing domain entities directly.

**Acceptance Criteria:**
- [ ] DTOs for Create, Update, and Read operations
- [ ] DTOs in SchoolManagement.Application/DTOs/ folder
- [ ] Include validation attributes on DTOs
- [ ] Separate folder per entity (Students/, Teachers/, etc.)

**Conventional Commit Examples:**
```bash
feat(app): add Student DTOs (Create, Update, Response)
feat(app): add Teacher DTOs with validation attributes
feat(app): add Course DTOs with relationships
feat(app): add Enrollment DTOs with student/course references
```

**PR Title:** `feat: Add DTOs for all entities`

---

#### Task 3.2: Configure AutoMapper Profiles
**Assigned to:** Esi (Beginner)
**Branch:** `feature/automapper`
**Base Branch:** `feature/dtos`
**Complexity:** Beginner

**Description:**
Set up AutoMapper to map between domain entities, persistence entities, and DTOs.

**Acceptance Criteria:**
- [ ] AutoMapper package installed in Application project
- [ ] Mapping profiles created for each entity
- [ ] Bidirectional mappings (Entity <-> DTO)
- [ ] Custom mappings for complex properties

**Conventional Commit Examples:**
```bash
build(app): add AutoMapper.Extensions.Microsoft.DependencyInjection
feat(app): create AutoMapper profiles for domain entities
feat(app): configure DTO to entity mappings
```

**PR Title:** `feat: Configure AutoMapper for entity/DTO mapping`

---

#### Task 3.3: Implement Service Interfaces
**Assigned to:** Ali (Advanced)
**Branch:** `feature/service-interfaces`
**Base Branch:** `feature/automapper`
**Complexity:** Intermediate

**Description:**
Define service interfaces with business operation methods in Application layer.

**Acceptance Criteria:**
- [ ] IStudentService, ITeacherService, ICourseService, IEnrollmentService
- [ ] Methods return DTOs, not domain entities
- [ ] Async method signatures
- [ ] XML documentation comments

**Conventional Commit Examples:**
```bash
feat(app): add IStudentService interface
feat(app): add ITeacherService interface
feat(app): add ICourseService and IEnrollmentService interfaces
```

**PR Title:** `feat: Define application service interfaces`

---

#### Task 3.4: Implement Student Service
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/student-service`
**Base Branch:** `feature/service-interfaces`
**Complexity:** Intermediate

**Description:**
Implement IStudentService with CRUD operations and business logic using Unit of Work.

**Acceptance Criteria:**
- [ ] All CRUD operations implemented
- [ ] Uses IUnitOfWork for data access
- [ ] Uses AutoMapper for entity/DTO conversion
- [ ] Business validation included
- [ ] Proper exception handling

**Conventional Commit Examples:**
```bash
feat(app): implement StudentService CRUD operations
feat(app): add business validation for student operations
refactor(app): optimize student query with eager loading
```

**PR Title:** `feat: Implement StudentService with business logic`

---

#### Task 3.5: Implement Teacher Service
**Assigned to:** Esi (Beginner)
**Branch:** `feature/teacher-service`
**Base Branch:** `feature/service-interfaces`
**Complexity:** Intermediate

**Description:**
Implement ITeacherService with CRUD operations and teacher-specific business rules.

**Acceptance Criteria:**
- [ ] All CRUD operations implemented
- [ ] Email uniqueness validation
- [ ] Prevent deletion if courses assigned
- [ ] Uses Unit of Work and AutoMapper

**Conventional Commit Examples:**
```bash
feat(app): implement TeacherService CRUD operations
feat(app): add email uniqueness validation
feat(app): prevent teacher deletion with assigned courses
```

**PR Title:** `feat: Implement TeacherService with validation`

---

#### Task 3.6: Implement Course Service
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/course-service`
**Base Branch:** `feature/service-interfaces`
**Complexity:** Intermediate

**Description:**
Implement ICourseService with business logic for course management.

**Acceptance Criteria:**
- [ ] CRUD operations for courses
- [ ] Validate TeacherId exists before creating course
- [ ] Prevent deletion if enrollments exist
- [ ] Include teacher info in course responses

**Conventional Commit Examples:**
```bash
feat(app): implement CourseService CRUD operations
feat(app): add teacher validation for course creation
feat(app): prevent course deletion with active enrollments
```

**PR Title:** `feat: Implement CourseService with business rules`

---

#### Task 3.7: Implement Enrollment Service
**Assigned to:** Ali (Advanced)
**Branch:** `feature/enrollment-service`
**Base Branch:** `feature/service-interfaces`
**Complexity:** Advanced

**Description:**
Implement IEnrollmentService with complex business rules for student-course enrollments.

**Acceptance Criteria:**
- [ ] CRUD operations for enrollments
- [ ] Prevent duplicate enrollments (same student + course)
- [ ] Validate student and course exist
- [ ] Methods to get enrollments by student or by course
- [ ] Grade update with validation (0-100)

**Conventional Commit Examples:**
```bash
feat(app): implement EnrollmentService CRUD operations
feat(app): add duplicate enrollment prevention
feat(app): add grade validation and update logic
feat(app): add queries for student/course enrollment filtering
```

**PR Title:** `feat: Implement EnrollmentService with advanced business logic`

---

#### Task 3.8: Register Services in DI Container
**Assigned to:** Ali (Advanced)
**Branch:** `feature/di-registration`
**Base Branch:** `main` (merge after all services)
**Complexity:** Intermediate

**Description:**
Create extension methods to register all application services in dependency injection container.

**Acceptance Criteria:**
- [ ] Extension method in Application project
- [ ] Registers AutoMapper
- [ ] Registers all services with scoped lifetime
- [ ] Called from API Program.cs

**Conventional Commit Examples:**
```bash
feat(app): add DI extension method for application services
config(app): register AutoMapper and services in DI container
```

**PR Title:** `feat: Configure dependency injection for application layer`

---

### Phase 4: API Layer & Middleware

#### Task 4.1: Create API Controllers
**Assigned to:** Esi (Beginner)
**Branch:** `feature/students-controller`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Create StudentsController with RESTful endpoints using StudentService.

**Acceptance Criteria:**
- [ ] GET /api/students - list all
- [ ] GET /api/students/{id} - get by ID
- [ ] POST /api/students - create
- [ ] PUT /api/students/{id} - update
- [ ] DELETE /api/students/{id} - delete
- [ ] Proper HTTP status codes
- [ ] Uses dependency injection for services

**Conventional Commit Examples:**
```bash
feat(api): add StudentsController with GET endpoints
feat(api): add POST endpoint for student creation
feat(api): add PUT and DELETE endpoints for students
```

**PR Title:** `feat: Add StudentsController with RESTful endpoints`

**Similar Tasks:**
- Task 4.2: TeachersController (Assigned to: Shervin)
- Task 4.3: CoursesController (Assigned to: Esi)
- Task 4.4: EnrollmentsController (Assigned to: Ali - more complex with filtering)

---

#### Task 4.5: Implement Global Exception Middleware
**Assigned to:** Ali (Advanced)
**Branch:** `feature/exception-middleware`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Create middleware to catch all unhandled exceptions and return consistent JSON error responses.

**Acceptance Criteria:**
- [ ] Middleware catches all exceptions
- [ ] Returns JSON with error details
- [ ] Logs exception information
- [ ] Different responses for development vs production
- [ ] Registered in Program.cs pipeline

**Conventional Commit Examples:**
```bash
feat(api): add global exception handling middleware
feat(api): add error response models for consistent formatting
config(api): register exception middleware in pipeline
```

**PR Title:** `feat: Implement global exception handling middleware`

---

#### Task 4.6: Add Request/Response Logging Middleware
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/logging-middleware`
**Base Branch:** `feature/exception-middleware`
**Complexity:** Intermediate

**Description:**
Create middleware to log incoming requests and outgoing responses.

**Acceptance Criteria:**
- [ ] Logs request method, path, and timestamp
- [ ] Logs response status code and duration
- [ ] Uses ILogger for logging
- [ ] Registered before controllers in pipeline

**Conventional Commit Examples:**
```bash
feat(api): add request/response logging middleware
config(api): register logging middleware in pipeline
```

**PR Title:** `feat: Add request/response logging middleware`

---

#### Task 4.7: Configure Swagger/OpenAPI
**Assigned to:** Esi (Beginner)
**Branch:** `feature/swagger-config`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Configure Swashbuckle for API documentation with proper metadata.

**Acceptance Criteria:**
- [ ] Swagger UI available at /swagger
- [ ] API metadata (title, version, description)
- [ ] XML comments included in documentation
- [ ] Only enabled in Development environment

**Conventional Commit Examples:**
```bash
feat(api): configure Swagger/OpenAPI documentation
config(api): add XML documentation to Swagger UI
```

**PR Title:** `feat: Configure Swagger for API documentation`

---

#### Task 4.8: Configure CORS Policy
**Assigned to:** Ali (Advanced)
**Branch:** `feature/cors-policy`
**Base Branch:** `main`
**Complexity:** Intermediate

**Description:**
Configure secure CORS policy (not AllowAll) with specific origins from configuration.

**Acceptance Criteria:**
- [ ] CORS policy reads allowed origins from appsettings
- [ ] Specific methods and headers allowed
- [ ] Credentials support configured
- [ ] Different policies for development vs production

**Conventional Commit Examples:**
```bash
feat(api): add secure CORS policy configuration
config(api): configure environment-specific CORS origins
```

**PR Title:** `feat: Configure secure CORS policy`

---

### Phase 5: Cross-Cutting Concerns

#### Task 5.1: Integrate Serilog
**Assigned to:** Ali (Advanced)
**Branch:** `feature/serilog-integration`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Replace default logging with Serilog, configure file and console sinks with rolling policies.

**Acceptance Criteria:**
- [ ] Serilog packages installed
- [ ] Configured in Program.cs
- [ ] File sink with rolling policies (daily, size limits)
- [ ] Console sink with colored output
- [ ] Enrichers (environment, machine name, thread ID)
- [ ] Different log levels per environment

**Conventional Commit Examples:**
```bash
build(api): add Serilog packages for logging
feat(api): configure Serilog with file and console sinks
config(api): add Serilog enrichers and rolling policies
```

**PR Title:** `feat: Integrate Serilog for structured logging`

---

#### Task 5.2: Configure JWT Settings
**Assigned to:** Ali (Advanced)
**Branch:** `feature/jwt-settings`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Add JWT configuration in appsettings and create authentication infrastructure.

**Acceptance Criteria:**
- [ ] JWT settings in appsettings (secret, issuer, audience, expiry)
- [ ] JWT settings model class
- [ ] JWT token generation service interface and implementation
- [ ] Password hashing utility

**Conventional Commit Examples:**
```bash
feat(api): add JWT configuration settings
feat(api): implement JWT token generation service
feat(api): add password hashing utility
```

**PR Title:** `feat: Configure JWT authentication infrastructure`

---

#### Task 5.3: Create User Entity and Repository
**Assigned to:** Esi (Beginner)
**Branch:** `feature/user-entity`
**Base Branch:** `feature/jwt-settings`
**Complexity:** Intermediate

**Description:**
Add User entity to domain and infrastructure for authentication.

**Acceptance Criteria:**
- [ ] User entity in Domain (Username, PasswordHash, Email, Role)
- [ ] User persistence entity in Infrastructure
- [ ] UserRepository implementation
- [ ] Add Users DbSet to SchoolDbContext
- [ ] Create migration for Users table

**Conventional Commit Examples:**
```bash
feat(domain): add User entity for authentication
feat(infra): add UserRepository and DbContext configuration
feat(infra): add migration for Users table
```

**PR Title:** `feat: Add User entity for authentication`

---

#### Task 5.4: Implement Authentication Service
**Assigned to:** Ali (Advanced)
**Branch:** `feature/auth-service`
**Base Branch:** `feature/user-entity`
**Complexity:** Advanced

**Description:**
Create authentication service for user registration and login with JWT generation.

**Acceptance Criteria:**
- [ ] IAuthService interface
- [ ] Register method (creates user with hashed password)
- [ ] Login method (validates credentials, returns JWT)
- [ ] Uses JWT token service
- [ ] Password validation and hashing

**Conventional Commit Examples:**
```bash
feat(app): add IAuthService interface
feat(app): implement user registration with password hashing
feat(app): implement login with JWT token generation
```

**PR Title:** `feat: Implement authentication service with JWT`

---

#### Task 5.5: Create Auth Controller
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/auth-controller`
**Base Branch:** `feature/auth-service`
**Complexity:** Intermediate

**Description:**
Create AuthController with register and login endpoints.

**Acceptance Criteria:**
- [ ] POST /api/auth/register - user registration
- [ ] POST /api/auth/login - user login (returns JWT)
- [ ] DTOs for registration and login requests
- [ ] Proper HTTP status codes
- [ ] Error handling for invalid credentials

**Conventional Commit Examples:**
```bash
feat(api): add AuthController with register endpoint
feat(api): add login endpoint returning JWT token
feat(api): add authentication DTOs
```

**PR Title:** `feat: Add AuthController with register/login endpoints`

---

#### Task 5.6: Configure JWT Authentication Middleware
**Assigned to:** Ali (Advanced)
**Branch:** `feature/jwt-middleware`
**Base Branch:** `feature/auth-controller`
**Complexity:** Advanced

**Description:**
Configure ASP.NET Core authentication middleware to validate JWT tokens.

**Acceptance Criteria:**
- [ ] Authentication middleware configured in Program.cs
- [ ] JWT Bearer token validation parameters
- [ ] Authorization policies defined
- [ ] [Authorize] attribute protection on controllers
- [ ] Swagger configured to include JWT authorization

**Conventional Commit Examples:**
```bash
feat(api): configure JWT authentication middleware
config(api): add authorization policies
config(api): configure Swagger with JWT authorization
feat(api): protect API endpoints with [Authorize] attribute
```

**PR Title:** `feat: Configure JWT authentication and authorization`

---

### Phase 6: Testing & CI/CD

#### Task 6.1: Setup xUnit Test Project
**Assigned to:** Esi (Beginner)
**Branch:** `feature/test-project-setup`
**Base Branch:** `main`
**Complexity:** Beginner

**Description:**
Create xUnit test project with necessary packages for unit and integration testing.

**Acceptance Criteria:**
- [ ] xUnit test project created
- [ ] Packages: xUnit, Moq, FluentAssertions, Microsoft.EntityFrameworkCore.InMemory
- [ ] Project references to Application and Infrastructure
- [ ] Test folder structure (Unit/, Integration/)

**Conventional Commit Examples:**
```bash
test: create xUnit test project
build(test): add testing packages (xUnit, Moq, FluentAssertions)
test: setup test folder structure
```

**PR Title:** `test: Setup xUnit test project with dependencies`

---

#### Task 6.2: Write Sample Unit Tests
**Assigned to:** Shervin (Beginner)
**Branch:** `feature/unit-tests`
**Base Branch:** `feature/test-project-setup`
**Complexity:** Beginner

**Description:**
Write sample unit tests for StudentService to demonstrate testing approach.

**Acceptance Criteria:**
- [ ] Test class: StudentServiceTests
- [ ] Mock IUnitOfWork and dependencies
- [ ] Tests for GetAllAsync, GetByIdAsync, CreateAsync
- [ ] Use FluentAssertions for readable assertions
- [ ] All tests pass

**Conventional Commit Examples:**
```bash
test(unit): add StudentService unit tests
test(unit): add tests for CRUD operations
```

**PR Title:** `test: Add sample unit tests for StudentService`

---

#### Task 6.3: Write Sample Integration Tests
**Assigned to:** Ali (Advanced)
**Branch:** `feature/integration-tests`
**Base Branch:** `feature/test-project-setup`
**Complexity:** Advanced

**Description:**
Write integration tests using in-memory database to test full data flow.

**Acceptance Criteria:**
- [ ] Integration test class with WebApplicationFactory
- [ ] In-memory database configuration
- [ ] Tests for API endpoints (POST, GET, PUT, DELETE)
- [ ] Verify database state after operations
- [ ] All tests pass

**Conventional Commit Examples:**
```bash
test(integration): add API integration test infrastructure
test(integration): add student endpoint integration tests
```

**PR Title:** `test: Add integration tests with in-memory database`

---

#### Task 6.4: Create GitHub Actions Workflow
**Assigned to:** Ali (Advanced)
**Branch:** `feature/github-actions`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Create CI/CD pipeline using GitHub Actions to build, test, and validate PRs.

**Acceptance Criteria:**
- [ ] Workflow file in .github/workflows/ci.yml
- [ ] Triggers on push and pull_request to main
- [ ] Steps: Checkout, Setup .NET 7, Restore, Build, Test
- [ ] Runs on ubuntu-latest
- [ ] Fails build if tests fail

**Conventional Commit Examples:**
```bash
ci: add GitHub Actions workflow for CI/CD
ci: configure test execution in GitHub Actions
```

**PR Title:** `ci: Add GitHub Actions CI/CD pipeline`

---

### Phase 7: Optional Enhancement (MediatR/CQRS)

#### Task 7.1: Install and Configure MediatR
**Assigned to:** Ali (Advanced)
**Branch:** `feature/mediatr-setup`
**Base Branch:** `main`
**Complexity:** Advanced

**Description:**
Install MediatR packages and configure in Application layer.

**Acceptance Criteria:**
- [ ] MediatR package installed in Application project
- [ ] MediatR registered in DI container
- [ ] Folder structure: Commands/, Queries/, Handlers/

**Conventional Commit Examples:**
```bash
build(app): add MediatR packages
feat(app): configure MediatR in application layer
```

**PR Title:** `feat: Install and configure MediatR for CQRS`

---

#### Task 7.2: Create Sample CQRS Handlers
**Assigned to:** Ali (Advanced)
**Branch:** `feature/cqrs-handlers`
**Base Branch:** `feature/mediatr-setup`
**Complexity:** Advanced

**Description:**
Refactor StudentService to use CQRS pattern with MediatR commands and queries.

**Acceptance Criteria:**
- [ ] GetAllStudentsQuery and handler
- [ ] GetStudentByIdQuery and handler
- [ ] CreateStudentCommand and handler
- [ ] UpdateStudentCommand and handler
- [ ] DeleteStudentCommand and handler

**Conventional Commit Examples:**
```bash
feat(app): add GetAllStudentsQuery with handler
feat(app): add CreateStudentCommand with handler
refactor(app): migrate StudentService to CQRS pattern
```

**PR Title:** `feat: Implement CQRS for Student operations`

---

#### Task 7.3: Add FluentValidation Pipeline Behavior
**Assigned to:** Ali (Advanced)
**Branch:** `feature/fluentvalidation`
**Base Branch:** `feature/cqrs-handlers`
**Complexity:** Advanced

**Description:**
Add FluentValidation with MediatR pipeline behavior for automatic validation.

**Acceptance Criteria:**
- [ ] FluentValidation package installed
- [ ] Validators for commands (CreateStudentValidator, etc.)
- [ ] ValidationBehavior pipeline behavior
- [ ] Automatic validation before handler execution

**Conventional Commit Examples:**
```bash
build(app): add FluentValidation packages
feat(app): add command validators
feat(app): add validation pipeline behavior
```

**PR Title:** `feat: Add FluentValidation with MediatR pipeline`

---

## D. Step-by-Step Walkthroughs

### Walkthrough 1: Creating the Solution Structure (Task 0.1)

**For:** Ali (Advanced)

**Step 1: Create Solution and Projects**

Open terminal in your workspace directory:

```bash
# Create solution
dotnet new sln -n SchoolManagement

# Create projects
dotnet new classlib -n SchoolManagement.Domain -f net7.0
dotnet new classlib -n SchoolManagement.Application -f net7.0
dotnet new classlib -n SchoolManagement.Infrastructure -f net7.0
dotnet new webapi -n SchoolManagement.API -f net7.0
dotnet new xunit -n SchoolManagement.Tests -f net7.0

# Add projects to solution
dotnet sln add SchoolManagement.Domain/SchoolManagement.Domain.csproj
dotnet sln add SchoolManagement.Application/SchoolManagement.Application.csproj
dotnet sln add SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj
dotnet sln add SchoolManagement.API/SchoolManagement.API.csproj
dotnet sln add SchoolManagement.Tests/SchoolManagement.Tests.csproj
```

**Step 2: Configure Project References**

```bash
# Application references Domain
dotnet add SchoolManagement.Application/SchoolManagement.Application.csproj reference SchoolManagement.Domain/SchoolManagement.Domain.csproj

# Infrastructure references Domain and Application
dotnet add SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj reference SchoolManagement.Domain/SchoolManagement.Domain.csproj
dotnet add SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj reference SchoolManagement.Application/SchoolManagement.Application.csproj

# API references Application and Infrastructure
dotnet add SchoolManagement.API/SchoolManagement.API.csproj reference SchoolManagement.Application/SchoolManagement.Application.csproj
dotnet add SchoolManagement.API/SchoolManagement.API.csproj reference SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj

# Tests reference all except API
dotnet add SchoolManagement.Tests/SchoolManagement.Tests.csproj reference SchoolManagement.Domain/SchoolManagement.Domain.csproj
dotnet add SchoolManagement.Tests/SchoolManagement.Tests.csproj reference SchoolManagement.Application/SchoolManagement.Application.csproj
dotnet add SchoolManagement.Tests/SchoolManagement.Tests.csproj reference SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj
```

**Step 3: Add Required Packages**

**File:** `SchoolManagement.Infrastructure/SchoolManagement.Infrastructure.csproj`

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.20" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.20">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

**File:** `SchoolManagement.API/SchoolManagement.API.csproj`

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="7.0.19" />
  <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  <PackageReference Include="Serilog.AspNetCore" Version="7.0.0" />
  <PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.19" />
</ItemGroup>
```

**Step 4: Build to Verify**

```bash
dotnet build
```

Expected output: Build succeeded with 0 errors.

---

### Walkthrough 2: Creating Domain Entities (Task 1.1)

**For:** Ali (Advanced)

**Step 1: Create Base Entity**

**File:** `SchoolManagement.Domain/Entities/BaseEntity.cs`

```csharp
namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Base entity with common properties for all domain entities
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date and time when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the entity was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
```

**Step 2: Create Student Entity**

**File:** `SchoolManagement.Domain/Entities/Student.cs`

```csharp
namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student in the school management system
/// </summary>
public class Student : BaseEntity
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Collection of enrollments for this student
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
```

**Step 3: Create Teacher Entity**

**File:** `SchoolManagement.Domain/Entities/Teacher.cs`

```csharp
namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a teacher in the school management system
/// </summary>
public class Teacher : BaseEntity
{
    /// <summary>
    /// Full name of the teacher
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the teacher (must be unique)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date when the teacher was hired
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Collection of courses taught by this teacher
    /// </summary>
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
```

**Step 4: Create Course Entity**

**File:** `SchoolManagement.Domain/Entities/Course.cs`

```csharp
namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a course in the school management system
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// Title of the course
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the course
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the course starts
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Foreign key to the teacher teaching this course
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Navigation property to the teacher
    /// </summary>
    public virtual Teacher Teacher { get; set; } = null!;

    /// <summary>
    /// Collection of enrollments for this course
    /// </summary>
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
```

**Step 5: Create Enrollment Entity**

**File:** `SchoolManagement.Domain/Entities/Enrollment.cs`

```csharp
namespace SchoolManagement.Domain.Entities;

/// <summary>
/// Represents a student's enrollment in a course
/// </summary>
public class Enrollment : BaseEntity
{
    /// <summary>
    /// Date when the student enrolled in the course
    /// </summary>
    public DateTime EnrollDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Grade received by the student (0-100)
    /// </summary>
    public decimal? Grade { get; set; }

    /// <summary>
    /// Foreign key to the student
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Navigation property to the student
    /// </summary>
    public virtual Student Student { get; set; } = null!;

    /// <summary>
    /// Foreign key to the course
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Navigation property to the course
    /// </summary>
    public virtual Course Course { get; set; } = null!;
}
```

**Step 6: Build to Verify**

```bash
dotnet build SchoolManagement.Domain
```

---

### Walkthrough 3: Defining Repository Interfaces (Task 1.2)

**For:** Shervin (Beginner)

**Step 1: Create Generic Repository Interface**

**File:** `SchoolManagement.Domain/Interfaces/IGenericRepository.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Generic repository interface with common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get all entities asynchronously
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get entity by ID asynchronously
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Add new entity asynchronously
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update existing entity
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Delete entity
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Check if entity exists by ID
    /// </summary>
    Task<bool> ExistsAsync(int id);
}
```

**Step 2: Create Student Repository Interface**

**File:** `SchoolManagement.Domain/Interfaces/IStudentRepository.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Student-specific repository interface
/// </summary>
public interface IStudentRepository : IGenericRepository<Student>
{
    /// <summary>
    /// Get student with all enrollments and course details
    /// </summary>
    Task<Student?> GetWithEnrollmentsAsync(int id);

    /// <summary>
    /// Get all active students
    /// </summary>
    Task<IEnumerable<Student>> GetActiveStudentsAsync();
}
```

**Step 3: Create Teacher Repository Interface**

**File:** `SchoolManagement.Domain/Interfaces/ITeacherRepository.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Teacher-specific repository interface
/// </summary>
public interface ITeacherRepository : IGenericRepository<Teacher>
{
    /// <summary>
    /// Get teacher with all courses
    /// </summary>
    Task<Teacher?> GetWithCoursesAsync(int id);

    /// <summary>
    /// Get teacher by email address
    /// </summary>
    Task<Teacher?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if email is already in use
    /// </summary>
    Task<bool> EmailExistsAsync(string email, int? excludeTeacherId = null);
}
```

**Step 4: Create Course Repository Interface**

**File:** `SchoolManagement.Domain/Interfaces/ICourseRepository.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Course-specific repository interface
/// </summary>
public interface ICourseRepository : IGenericRepository<Course>
{
    /// <summary>
    /// Get course with teacher and enrollment details
    /// </summary>
    Task<Course?> GetWithDetailsAsync(int id);

    /// <summary>
    /// Get all courses taught by a specific teacher
    /// </summary>
    Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
}
```

**Step 5: Create Enrollment Repository Interface**

**File:** `SchoolManagement.Domain/Interfaces/IEnrollmentRepository.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Enrollment-specific repository interface
/// </summary>
public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    /// <summary>
    /// Get all enrollments for a specific student
    /// </summary>
    Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);

    /// <summary>
    /// Get all enrollments for a specific course
    /// </summary>
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);

    /// <summary>
    /// Check if student is already enrolled in a course
    /// </summary>
    Task<bool> IsEnrolledAsync(int studentId, int courseId);

    /// <summary>
    /// Get enrollment with student and course details
    /// </summary>
    Task<Enrollment?> GetWithDetailsAsync(int id);
}
```

**Step 6: Create Unit of Work Interface**

**File:** `SchoolManagement.Domain/Interfaces/IUnitOfWork.cs`

```csharp
namespace SchoolManagement.Domain.Interfaces;

/// <summary>
/// Unit of Work interface for managing transactions across multiple repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Student repository
    /// </summary>
    IStudentRepository Students { get; }

    /// <summary>
    /// Teacher repository
    /// </summary>
    ITeacherRepository Teachers { get; }

    /// <summary>
    /// Course repository
    /// </summary>
    ICourseRepository Courses { get; }

    /// <summary>
    /// Enrollment repository
    /// </summary>
    IEnrollmentRepository Enrollments { get; }

    /// <summary>
    /// Save all changes to the database asynchronously
    /// </summary>
    Task<int> SaveChangesAsync();
}
```

---

### Walkthrough 4: Creating DTOs (Task 3.1)

**For:** Shervin (Beginner)

**Step 1: Create Student DTOs**

**File:** `SchoolManagement.Application/DTOs/Students/StudentCreateDto.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for creating a new student
/// </summary>
public class StudentCreateDto
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is active
    /// </summary>
    public bool IsActive { get; set; } = true;
}
```

**File:** `SchoolManagement.Application/DTOs/Students/StudentUpdateDto.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for updating an existing student
/// </summary>
public class StudentUpdateDto
{
    /// <summary>
    /// Full name of the student
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is active
    /// </summary>
    public bool IsActive { get; set; }
}
```

**File:** `SchoolManagement.Application/DTOs/Students/StudentResponseDto.cs`

```csharp
namespace SchoolManagement.Application.DTOs.Students;

/// <summary>
/// DTO for student response
/// </summary>
public class StudentResponseDto
{
    /// <summary>
    /// Student ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the student
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Indicates whether the student is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Number of enrollments
    /// </summary>
    public int EnrollmentCount { get; set; }

    /// <summary>
    /// Date when the student was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
```

**Step 2: Create Similar DTOs for Other Entities**

Follow the same pattern for:
- `TeacherCreateDto`, `TeacherUpdateDto`, `TeacherResponseDto`
- `CourseCreateDto`, `CourseUpdateDto`, `CourseResponseDto`
- `EnrollmentCreateDto`, `EnrollmentUpdateDto`, `EnrollmentResponseDto`

---

## E. Clean Architecture Scaffolding

### Project Structure

```
SchoolManagement/
│
├── SchoolManagement.Domain/                    # Core business entities and logic
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   ├── Student.cs
│   │   ├── Teacher.cs
│   │   ├── Course.cs
│   │   ├── Enrollment.cs
│   │   └── User.cs
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs
│   │   ├── IStudentRepository.cs
│   │   ├── ITeacherRepository.cs
│   │   ├── ICourseRepository.cs
│   │   ├── IEnrollmentRepository.cs
│   │   ├── IUserRepository.cs
│   │   └── IUnitOfWork.cs
│   └── ValueObjects/                           # Optional
│       ├── Email.cs
│       └── Grade.cs
│
├── SchoolManagement.Application/               # Business logic and use cases
│   ├── DTOs/
│   │   ├── Students/
│   │   │   ├── StudentCreateDto.cs
│   │   │   ├── StudentUpdateDto.cs
│   │   │   └── StudentResponseDto.cs
│   │   ├── Teachers/
│   │   ├── Courses/
│   │   ├── Enrollments/
│   │   └── Auth/
│   ├── Interfaces/
│   │   ├── IStudentService.cs
│   │   ├── ITeacherService.cs
│   │   ├── ICourseService.cs
│   │   ├── IEnrollmentService.cs
│   │   ├── IAuthService.cs
│   │   └── IJwtTokenService.cs
│   ├── Services/
│   │   ├── StudentService.cs
│   │   ├── TeacherService.cs
│   │   ├── CourseService.cs
│   │   ├── EnrollmentService.cs
│   │   ├── AuthService.cs
│   │   └── JwtTokenService.cs
│   ├── Mapping/
│   │   └── MappingProfile.cs
│   ├── Extensions/
│   │   └── ServiceCollectionExtensions.cs
│   └── [Optional] Commands/ Queries/ Handlers/ # MediatR CQRS
│
├── SchoolManagement.Infrastructure/            # Data access and external concerns
│   ├── Persistence/
│   │   ├── Entities/                           # EF Core entities (if separated)
│   │   │   ├── StudentEntity.cs
│   │   │   └── ...
│   │   ├── Configurations/
│   │   │   ├── StudentConfiguration.cs
│   │   │   ├── TeacherConfiguration.cs
│   │   │   ├── CourseConfiguration.cs
│   │   │   └── EnrollmentConfiguration.cs
│   │   └── SchoolDbContext.cs
│   ├── Repositories/
│   │   ├── GenericRepository.cs
│   │   ├── StudentRepository.cs
│   │   ├── TeacherRepository.cs
│   │   ├── CourseRepository.cs
│   │   ├── EnrollmentRepository.cs
│   │   ├── UserRepository.cs
│   │   └── UnitOfWork.cs
│   ├── Migrations/
│   ├── Extensions/
│   │   └── ServiceCollectionExtensions.cs
│   └── Utilities/
│       └── PasswordHasher.cs
│
├── SchoolManagement.API/                       # Web API layer
│   ├── Controllers/
│   │   ├── StudentsController.cs
│   │   ├── TeachersController.cs
│   │   ├── CoursesController.cs
│   │   ├── EnrollmentsController.cs
│   │   └── AuthController.cs
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   └── RequestLoggingMiddleware.cs
│   ├── Models/                                 # API-specific models
│   │   └── ErrorResponse.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
└── SchoolManagement.Tests/                    # Testing project
    ├── Unit/
    │   ├── Services/
    │   │   ├── StudentServiceTests.cs
    │   │   └── ...
    │   └── Repositories/
    └── Integration/
        ├── Controllers/
        │   ├── StudentsControllerTests.cs
        │   └── ...
        └── Helpers/
            └── TestWebApplicationFactory.cs
```

### Project Dependencies

```
Domain (no dependencies)
  ↑
Application (depends on Domain)
  ↑
Infrastructure (depends on Domain, Application)
  ↑
API (depends on Application, Infrastructure)
  ↑
Tests (depends on Domain, Application, Infrastructure)
```

### Responsibility Explanation

**Domain Layer:**
- Core business entities with business rules
- Domain interfaces (repositories, services)
- No dependencies on other layers
- Framework-agnostic (no EF Core, no ASP.NET)

**Application Layer:**
- Business logic and use cases
- DTOs for data transfer
- Service interfaces and implementations
- Orchestrates domain objects
- Depends only on Domain layer

**Infrastructure Layer:**
- Data access (EF Core, DbContext)
- Repository implementations
- External service integrations
- Persistence concerns
- Depends on Domain and Application

**API Layer:**
- HTTP endpoints (controllers)
- Request/response handling
- Middleware (exception handling, logging)
- Authentication/authorization
- Depends on Application and Infrastructure

**Tests Layer:**
- Unit tests (isolated, mocked dependencies)
- Integration tests (database, API endpoints)
- Test utilities and helpers

### Separation of Domain and EF Entities

**Option 1: Same Classes (Simpler)**
- Use domain entities directly with EF Core
- Add Fluent API configurations in Infrastructure
- Pros: Less code, simpler mapping
- Cons: Domain has slight coupling to persistence

**Option 2: Separate Classes (Purist)**
- Domain entities remain pure
- Create separate EF entities in Infrastructure
- Map between domain and EF entities
- Pros: Complete separation, domain stays clean
- Cons: More code, additional mapping layer

**Recommended for this project:** Option 1 with Fluent API (balances Clean Architecture principles with pragmatism for a university project).

---

## F. UnitOfWork & GenericRepository

### Generic Repository Implementation

**File:** `SchoolManagement.Infrastructure/Repositories/GenericRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation with common CRUD operations
/// </summary>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly SchoolDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(SchoolDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public virtual void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}
```

### Entity-Specific Repository Example

**File:** `SchoolManagement.Infrastructure/Repositories/StudentRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

/// <summary>
/// Student repository with custom query methods
/// </summary>
public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(SchoolDbContext context) : base(context)
    {
    }

    public async Task<Student?> GetWithEnrollmentsAsync(int id)
    {
        return await _dbSet
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.Teacher)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Student>> GetActiveStudentsAsync()
    {
        return await _dbSet
            .Where(s => s.IsActive)
            .OrderBy(s => s.FullName)
            .ToListAsync();
    }
}
```

### Unit of Work Implementation

**File:** `SchoolManagement.Infrastructure/Repositories/UnitOfWork.cs`

```csharp
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation for managing transactions
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _context;
    private IStudentRepository? _students;
    private ITeacherRepository? _teachers;
    private ICourseRepository? _courses;
    private IEnrollmentRepository? _enrollments;

    public UnitOfWork(SchoolDbContext context)
    {
        _context = context;
    }

    public IStudentRepository Students
    {
        get { return _students ??= new StudentRepository(_context); }
    }

    public ITeacherRepository Teachers
    {
        get { return _teachers ??= new TeacherRepository(_context); }
    }

    public ICourseRepository Courses
    {
        get { return _courses ??= new CourseRepository(_context); }
    }

    public IEnrollmentRepository Enrollments
    {
        get { return _enrollments ??= new EnrollmentRepository(_context); }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### DI Registration in Infrastructure

**File:** `SchoolManagement.Infrastructure/Extensions/ServiceCollectionExtensions.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<SchoolDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(SchoolDbContext).Assembly.FullName)));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        return services;
    }
}
```

---

## G. Exception Middleware

**File:** `SchoolManagement.API/Middleware/ExceptionHandlingMiddleware.cs`

```csharp
using System.Net;
using System.Text.Json;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Middleware;

/// <summary>
/// Global exception handling middleware
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Message = "An error occurred while processing your request.",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        // Customize response based on exception type
        switch (exception)
        {
            case ArgumentException argEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = argEx.Message;
                break;

            case KeyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "The requested resource was not found.";
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Message = "You are not authorized to perform this action.";
                break;

            case InvalidOperationException invalidOpEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = invalidOpEx.Message;
                break;

            default:
                // For unknown exceptions, return generic message in production
                if (!_environment.IsDevelopment())
                {
                    response.Message = "An unexpected error occurred. Please try again later.";
                }
                else
                {
                    // In development, include detailed error information
                    response.Message = exception.Message;
                    response.Details = exception.StackTrace;
                }
                break;
        }

        context.Response.StatusCode = response.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
```

**File:** `SchoolManagement.API/Models/ErrorResponse.cs`

```csharp
namespace SchoolManagement.API.Models;

/// <summary>
/// Standard error response model
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detailed error information (development only)
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
```

**Registration in Program.cs:**

```csharp
// Add after app is built
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

## H. Logging with Serilog

**File:** `SchoolManagement.API/Program.cs` (Serilog Configuration)

```csharp
using Serilog;
using Serilog.Events;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/schoolmanagement-.log",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 10_485_760, // 10MB
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting SchoolManagement API");

    var builder = WebApplication.CreateBuilder(args);

    // Replace default logging with Serilog
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add Infrastructure and Application layers
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    // Configure HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Add Serilog request logging
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
        };
    });

    app.UseHttpsRedirection();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
```

**File:** `SchoolManagement.API/appsettings.json` (Serilog Settings)

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/schoolmanagement-.log",
          "rollingInterval": "Day",
          "rollOnFileSizeLimit": true,
          "fileSizeLimitBytes": 10485760,
          "retainedFileCountLimit": 30
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  },
  "AllowedHosts": "*"
}
```

**Usage in Controllers:**

```csharp
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAll()
    {
        _logger.LogInformation("Retrieving all students");
        var students = await _studentService.GetAllAsync();
        _logger.LogInformation("Retrieved {Count} students", students.Count());
        return Ok(students);
    }
}
```

---

## I. JWT Authentication

### Step 1: JWT Settings Configuration

**File:** `SchoolManagement.API/appsettings.json`

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "SchoolManagementAPI",
    "Audience": "SchoolManagementClient",
    "ExpiryMinutes": 60
  }
}
```

**File:** `SchoolManagement.Application/Settings/JwtSettings.cs`

```csharp
namespace SchoolManagement.Application.Settings;

/// <summary>
/// JWT authentication settings
/// </summary>
public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryMinutes { get; set; } = 60;
}
```

### Step 2: JWT Token Service

**File:** `SchoolManagement.Application/Interfaces/IJwtTokenService.cs`

```csharp
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

/// <summary>
/// Interface for JWT token generation
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generate JWT token for a user
    /// </summary>
    string GenerateToken(User user);
}
```

**File:** `SchoolManagement.Application/Services/JwtTokenService.cs`

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Settings;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services;

/// <summary>
/// JWT token generation service
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

### Step 3: Password Hasher Utility

**File:** `SchoolManagement.Infrastructure/Utilities/PasswordHasher.cs`

```csharp
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Infrastructure.Utilities;

/// <summary>
/// Utility for password hashing and verification
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Hash a password using SHA256
    /// </summary>
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    /// <summary>
    /// Verify a password against a hash
    /// </summary>
    public static bool VerifyPassword(string password, string passwordHash)
    {
        var hashedInput = HashPassword(password);
        return hashedInput == passwordHash;
    }
}
```

### Step 4: Authentication Service

**File:** `SchoolManagement.Application/DTOs/Auth/RegisterDto.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "User";
}
```

**File:** `SchoolManagement.Application/DTOs/Auth/LoginDto.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.DTOs.Auth;

public class LoginDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
```

**File:** `SchoolManagement.Application/DTOs/Auth/AuthResponseDto.cs`

```csharp
namespace SchoolManagement.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
```

**File:** `SchoolManagement.Application/Interfaces/IAuthService.cs`

```csharp
using SchoolManagement.Application.DTOs.Auth;

namespace SchoolManagement.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}
```

**File:** `SchoolManagement.Application/Services/AuthService.cs`

```csharp
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Utilities;

namespace SchoolManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if username already exists
        var existingUser = await _unitOfWork.Users.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
            throw new ArgumentException("Username already exists");

        // Check if email already exists
        var existingEmail = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);
        if (existingEmail != null)
            throw new ArgumentException("Email already exists");

        // Create new user with hashed password
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = PasswordHasher.HashPassword(registerDto.Password),
            Role = registerDto.Role
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password");

        // Verify password
        if (!PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password");

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }
}
```

### Step 5: Configure JWT in Program.cs

**File:** `SchoolManagement.API/Program.cs`

```csharp
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Settings;

// ... other code ...

// Configure JWT Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddAuthorization();

// ... after app is built ...

app.UseAuthentication();
app.UseAuthorization();
```

### Step 6: Protect Controllers

```csharp
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires JWT authentication
public class StudentsController : ControllerBase
{
    // All endpoints require authentication

    [AllowAnonymous] // This endpoint is public
    [HttpGet("public")]
    public IActionResult GetPublicInfo()
    {
        return Ok("This is public information");
    }
}
```

---

## J. Testing & CI/CD

### Unit Test Example

**File:** `SchoolManagement.Tests/Unit/Services/StudentServiceTests.cs`

```csharp
using Moq;
using FluentAssertions;
using SchoolManagement.Application.Services;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;

namespace SchoolManagement.Tests.Unit.Services;

public class StudentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly StudentService _studentService;

    public StudentServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _studentService = new StudentService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student { Id = 1, FullName = "John Doe" },
            new Student { Id = 2, FullName = "Jane Smith" }
        };

        var studentDtos = new List<StudentResponseDto>
        {
            new StudentResponseDto { Id = 1, FullName = "John Doe" },
            new StudentResponseDto { Id = 2, FullName = "Jane Smith" }
        };

        _unitOfWorkMock.Setup(u => u.Students.GetAllAsync())
            .ReturnsAsync(students);

        _mapperMock.Setup(m => m.Map<IEnumerable<StudentResponseDto>>(students))
            .Returns(studentDtos);

        // Act
        var result = await _studentService.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(studentDtos);
        _unitOfWorkMock.Verify(u => u.Students.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenStudentExists_ShouldReturnStudent()
    {
        // Arrange
        var student = new Student { Id = 1, FullName = "John Doe" };
        var studentDto = new StudentResponseDto { Id = 1, FullName = "John Doe" };

        _unitOfWorkMock.Setup(u => u.Students.GetByIdAsync(1))
            .ReturnsAsync(student);

        _mapperMock.Setup(m => m.Map<StudentResponseDto>(student))
            .Returns(studentDto);

        // Act
        var result = await _studentService.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be("John Doe");
    }

    [Fact]
    public async Task GetByIdAsync_WhenStudentDoesNotExist_ShouldThrowException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.Students.GetByIdAsync(999))
            .ReturnsAsync((Student?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _studentService.GetByIdAsync(999));
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ShouldCreateStudent()
    {
        // Arrange
        var createDto = new StudentCreateDto
        {
            FullName = "John Doe",
            BirthDate = new DateTime(2000, 1, 1)
        };

        var student = new Student
        {
            Id = 1,
            FullName = "John Doe",
            BirthDate = new DateTime(2000, 1, 1)
        };

        var responseDto = new StudentResponseDto
        {
            Id = 1,
            FullName = "John Doe",
            BirthDate = new DateTime(2000, 1, 1)
        };

        _mapperMock.Setup(m => m.Map<Student>(createDto))
            .Returns(student);

        _unitOfWorkMock.Setup(u => u.Students.AddAsync(student))
            .ReturnsAsync(student);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
            .ReturnsAsync(1);

        _mapperMock.Setup(m => m.Map<StudentResponseDto>(student))
            .Returns(responseDto);

        // Act
        var result = await _studentService.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be("John Doe");
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
```

### Integration Test Example

**File:** `SchoolManagement.Tests/Integration/Controllers/StudentsControllerTests.cs`

```csharp
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using SchoolManagement.Application.DTOs.Students;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Tests.Integration.Controllers;

public class StudentsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public StudentsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SchoolDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Add in-memory database
                services.AddDbContext<SchoolDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                // Seed test data
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
                db.Database.EnsureCreated();
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/students");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        var createDto = new StudentCreateDto
        {
            FullName = "Test Student",
            BirthDate = new DateTime(2000, 1, 1),
            IsActive = true
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/students", createDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<StudentResponseDto>();
        result.Should().NotBeNull();
        result!.FullName.Should().Be("Test Student");
    }
}
```

### GitHub Actions CI/CD Workflow

**File:** `.github/workflows/ci.yml`

```yaml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  build-and-test:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3

    - name: Setup .NET 7
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '7.0.x'

    - name: Restore dependencies
      run: dotnet restore

    - name: Build solution
      run: dotnet build --configuration Release --no-restore

    - name: Run tests
      run: dotnet test --configuration Release --no-build --verbosity normal --logger "trx;LogFileName=test-results.trx"

    - name: Publish test results
      uses: EnricoMi/publish-unit-test-result-action@v2
      if: always()
      with:
        files: '**/test-results.trx'

    - name: Code coverage (optional)
      run: |
        dotnet test --configuration Release --no-build --collect:"XPlat Code Coverage"

  code-quality:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3

    - name: Setup .NET 7
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '7.0.x'

    - name: Install dotnet-format
      run: dotnet tool install -g dotnet-format

    - name: Check code formatting
      run: dotnet format --verify-no-changes --verbosity diagnostic
```

---

## K. Conventional Commits & PR Templates

### Conventional Commit Cheat Sheet

**Format:** `<type>(<scope>): <subject>`

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, no logic change)
- `refactor`: Code refactoring (no feature/bug change)
- `test`: Adding or updating tests
- `chore`: Maintenance tasks (dependencies, build config)
- `perf`: Performance improvements
- `ci`: CI/CD configuration changes
- `build`: Build system or dependency changes

**Scopes (examples):**
- `domain`: Domain layer
- `app`: Application layer
- `infra`: Infrastructure layer
- `api`: API layer
- `auth`: Authentication/authorization
- `students`: Student-related code
- `tests`: Testing code

**Examples:**
```bash
feat(domain): add Student entity with navigation properties
fix(api): correct validation logic in EnrollmentController
docs(readme): update setup instructions for .NET 7
refactor(app): extract common mapping logic to base service
test(unit): add StudentService unit tests
chore(deps): upgrade EF Core to 7.0.20
perf(infra): optimize student query with eager loading
ci: add GitHub Actions workflow for automated testing
build(api): add Serilog packages
style(domain): fix code formatting in entities
```

**Breaking Changes:**
```bash
feat(auth)!: migrate to JWT-only authentication

BREAKING CHANGE: Cookie-based authentication no longer supported
```

### Pull Request Template

**File:** `.github/pull_request_template.md`

```markdown
## Summary
<!-- Brief description of what this PR does -->

## Type of Change
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update
- [ ] Refactoring (no functional changes)
- [ ] Performance improvement
- [ ] Test addition/update

## Related Issues
<!-- Link to related issues, e.g., Closes #123 -->

## Changes Made
<!-- List the specific changes in bullet points -->
-
-
-

## Testing
<!-- Describe the tests you ran to verify your changes -->
- [ ] Unit tests pass
- [ ] Integration tests pass
- [ ] Manual testing completed

### Test Evidence
<!-- Add screenshots, logs, or test results if applicable -->

## Checklist
- [ ] My code follows the project's coding standards
- [ ] I have performed a self-review of my code
- [ ] I have commented my code where necessary
- [ ] I have updated the documentation accordingly
- [ ] My changes generate no new warnings
- [ ] I have added tests that prove my fix/feature works
- [ ] New and existing tests pass locally
- [ ] I have used conventional commit messages
- [ ] I have updated the CHANGELOG (if applicable)

## Breaking Changes
<!-- List any breaking changes and migration steps -->

## Additional Notes
<!-- Any additional information reviewers should know -->

## Screenshots (if applicable)
<!-- Add screenshots for UI changes -->

---

**Reviewer Guidelines:**
- [ ] Code quality and readability
- [ ] Test coverage adequate
- [ ] Documentation updated
- [ ] Security considerations addressed
- [ ] Performance impact evaluated
```

### Code Review Checklist

**For Reviewers:**

1. **Functionality**
   - [ ] Code does what it's supposed to do
   - [ ] Edge cases handled
   - [ ] Error handling implemented

2. **Code Quality**
   - [ ] Follows Clean Architecture principles
   - [ ] SOLID principles applied
   - [ ] DRY principle followed
   - [ ] Naming is clear and consistent
   - [ ] No code duplication

3. **Testing**
   - [ ] Unit tests cover critical paths
   - [ ] Tests are meaningful (not just for coverage)
   - [ ] Integration tests where appropriate

4. **Security**
   - [ ] No sensitive data exposed
   - [ ] Input validation present
   - [ ] SQL injection prevented (parameterized queries)
   - [ ] Authentication/authorization applied

5. **Performance**
   - [ ] No obvious performance issues
   - [ ] Database queries optimized
   - [ ] Async/await used properly

6. **Documentation**
   - [ ] XML comments for public APIs
   - [ ] README updated if needed
   - [ ] Complex logic explained

---

## L. Git Workflow Example

### Complete Workflow for a Feature

**Scenario:** Esi is implementing the TeacherService (Task 3.5)

**Step 1: Update Local Repository**

```bash
# Ensure you're on main branch
git checkout main

# Pull latest changes
git pull origin main
```

**Step 2: Create Feature Branch**

```bash
# Create and switch to feature branch
git checkout -b feature/teacher-service

# Verify you're on the correct branch
git branch
```

**Step 3: Implement the Feature**

```bash
# Create the service file
# (implement TeacherService.cs)

# Check status frequently
git status
```

**Step 4: Incremental Commits**

```bash
# Stage specific files
git add SchoolManagement.Application/Services/TeacherService.cs

# Commit with conventional commit message
git commit -m "feat(app): implement TeacherService CRUD operations"

# Continue working...
# Add email validation logic

git add SchoolManagement.Application/Services/TeacherService.cs
git commit -m "feat(app): add email uniqueness validation"

# Add deletion prevention

git add SchoolManagement.Application/Services/TeacherService.cs
git commit -m "feat(app): prevent teacher deletion with assigned courses"
```

**Step 5: Review Changes Before Pushing**

```bash
# View commit history
git log --oneline

# View diff of last commit
git diff HEAD~1

# View all changes in this branch
git diff main..feature/teacher-service
```

**Step 6: Push to Remote**

```bash
# Push branch to remote (first time)
git push -u origin feature/teacher-service

# Subsequent pushes
git push
```

**Step 7: Create Pull Request**

Go to GitHub and create PR:
- Base branch: `main`
- Compare branch: `feature/teacher-service`
- Fill in PR template
- Assign reviewers (Ali for beginners)
- Add labels: `enhancement`, `beginner-friendly`

**PR Title:**
```
feat: Implement TeacherService with validation
```

**PR Description:**
```markdown
## Summary
Implements ITeacherService with CRUD operations and teacher-specific business rules.

## Type of Change
- [x] New feature

## Changes Made
- Implemented all CRUD operations for teachers
- Added email uniqueness validation
- Added prevention of teacher deletion when courses are assigned
- Used Unit of Work and AutoMapper

## Testing
- [x] Manual testing completed
- All operations tested via Swagger

## Checklist
- [x] Code follows project standards
- [x] Self-review completed
- [x] Conventional commits used
```

**Step 8: Address Review Comments**

```bash
# Make changes based on feedback
# Edit files...

# Commit changes
git add .
git commit -m "fix(app): address PR review comments"

# Push updates
git push
```

**Step 9: After PR Approval and Merge**

```bash
# Switch back to main
git checkout main

# Pull merged changes
git pull origin main

# Delete local feature branch (optional)
git branch -d feature/teacher-service

# Delete remote feature branch (if not auto-deleted)
git push origin --delete feature/teacher-service
```

### Branch Naming Conventions

```
feature/<description>     - New features
fix/<description>         - Bug fixes
chore/<description>       - Maintenance tasks
docs/<description>        - Documentation
refactor/<description>    - Code refactoring
test/<description>        - Testing additions

Examples:
feature/student-service
fix/enrollment-validation-bug
chore/update-dependencies
docs/api-documentation
refactor/extract-base-service
test/integration-tests
```

### Common Git Commands Quick Reference

```bash
# Status and Info
git status                 # Show working tree status
git log --oneline         # Show commit history
git diff                  # Show unstaged changes
git diff --staged         # Show staged changes

# Branching
git branch                # List branches
git branch -a             # List all branches (including remote)
git checkout -b <name>    # Create and switch to new branch
git branch -d <name>      # Delete local branch

# Committing
git add <file>            # Stage specific file
git add .                 # Stage all changes
git commit -m "message"   # Commit with message
git commit --amend        # Amend last commit

# Remote Operations
git fetch                 # Fetch from remote
git pull                  # Fetch and merge
git push                  # Push to remote
git push -u origin <branch> # Push and set upstream

# Undoing Changes
git restore <file>        # Discard changes in working directory
git restore --staged <file> # Unstage file
git reset HEAD~1          # Undo last commit (keep changes)
git reset --hard HEAD~1   # Undo last commit (discard changes)

# Stashing
git stash                 # Stash changes
git stash pop             # Apply stashed changes
git stash list            # List stashes
```

---

## M. Onboarding Notes for Beginners

### For Shervin (Beginner)

**Recommended Starting Tasks (Easy to Intermediate):**

1. **Task 0.3: Development Environment Documentation**
   - Learn: Documentation writing, project setup
   - Skills: Technical writing, environment configuration
   - Support: Ali can review for accuracy

2. **Task 1.2: Define Domain Interfaces**
   - Learn: Interface design, generic types
   - Skills: C# interfaces, repository pattern concepts
   - Support: Follow Ali's entity examples

3. **Task 3.1: Create DTOs**
   - Learn: Data transfer objects, validation attributes
   - Skills: C# classes, data annotations
   - Support: Clear pattern to follow for all entities

4. **Task 3.4: Implement Student Service**
   - Learn: Service layer, dependency injection, async/await
   - Skills: Business logic implementation
   - Support: Pair programming with Ali recommended

5. **Task 3.6: Implement Course Service**
   - Learn: Building on Student Service experience
   - Skills: Business validation, repository usage
   - Support: Similar to StudentService pattern

**Learning Resources:**
- Clean Architecture: [Microsoft Docs - Clean Architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- EF Core: [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- ASP.NET Core: [ASP.NET Core Fundamentals](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/)

**Tips:**
- Don't hesitate to ask questions in PR comments
- Use descriptive variable names
- Write XML comments as you code
- Test your code manually before creating PR
- Read Ali's code to learn patterns

---

### For Esi (Beginner)

**Recommended Starting Tasks (Easy to Intermediate):**

1. **Task 0.2: Configure Git Repository**
   - Learn: Git basics, .gitignore, documentation
   - Skills: Version control fundamentals
   - Support: Straightforward task to build confidence

2. **Task 1.3: Add Value Objects**
   - Learn: Value objects, immutability, validation
   - Skills: Object-oriented design principles
   - Support: Clear examples available online

3. **Task 2.3: Implement Generic Repository**
   - Learn: Generic classes, EF Core, async operations
   - Skills: Data access patterns
   - Support: Follow interface defined by Shervin

4. **Task 3.2: Configure AutoMapper**
   - Learn: Object mapping, configuration
   - Skills: Library integration, dependency injection
   - Support: Clear documentation available

5. **Task 3.5: Implement Teacher Service**
   - Learn: Service pattern, validation logic
   - Skills: Business rules implementation
   - Support: Similar to StudentService (Shervin's work)

6. **Task 4.1: Create StudentsController**
   - Learn: REST APIs, HTTP methods, controller patterns
   - Skills: API development
   - Support: Clear pattern from requirements

**Learning Resources:**
- Git: [Git Handbook](https://guides.github.com/introduction/git-handbook/)
- Repository Pattern: [Martin Fowler - Repository](https://martinfowler.com/eaaCatalog/repository.html)
- AutoMapper: [AutoMapper Documentation](https://docs.automapper.org/)
- REST APIs: [RESTful API Design](https://restfulapi.net/)

**Tips:**
- Start with git workflow - practice with small commits
- Read existing code before implementing similar features
- Use breakpoints to debug and understand flow
- Ask Ali for code review on early PRs
- Take notes on patterns you discover

---

### For Ali (Experienced)

**Recommended Tasks (Intermediate to Advanced):**

**Phase 0-1 (Foundational - Lead Role):**
1. Task 0.1: Create Solution Structure
2. Task 1.1: Create Core Domain Entities
3. Task 3.8: Register Services in DI Container

**Phase 2-3 (Architecture - Lead Role):**
4. Task 2.1: Create EF Core Entities
5. Task 2.2: Create DbContext with Fluent API
6. Task 2.5: Implement Unit of Work Pattern
7. Task 3.3: Implement Service Interfaces

**Phase 4-5 (Advanced Features - Lead Role):**
8. Task 3.7: Implement Enrollment Service (most complex)
9. Task 4.5: Implement Global Exception Middleware
10. Task 4.8: Configure CORS Policy
11. Task 5.1: Integrate Serilog
12. Task 5.2: Configure JWT Settings
13. Task 5.4: Implement Authentication Service
14. Task 5.6: Configure JWT Authentication Middleware

**Phase 6-7 (Testing & Enhancement - Lead Role):**
15. Task 6.3: Write Integration Tests
16. Task 6.4: Create GitHub Actions Workflow
17. All Phase 7 tasks (MediatR/CQRS)

**Mentorship Responsibilities:**
- Review ALL PRs from Shervin and Esi
- Provide constructive feedback with examples
- Pair programming sessions for complex tasks
- Code quality enforcement
- Architecture decisions and guidance
- Weekly sync meetings with team

**Tips:**
- Set up clear code standards early
- Create example implementations for beginners to follow
- Be patient with beginners - remember your first projects
- Encourage questions and experimentation
- Document architectural decisions
- Focus on teaching, not just delivering

---

### Pair Programming Recommendations

**Suggested Pairs:**

1. **Ali + Shervin:** Task 3.4 (Student Service)
   - Ali guides service implementation
   - Shervin drives, Ali navigates

2. **Ali + Esi:** Task 2.3 (Generic Repository)
   - Ali explains repository pattern
   - Esi implements with guidance

3. **Shervin + Esi:** Task 4.2 & 4.3 (Controllers)
   - Both implement controllers together
   - Learn from each other's approaches

**Pair Programming Tips:**
- Switch driver/navigator every 30 minutes
- Navigator actively reviews and suggests
- Driver explains what they're doing
- Take breaks every hour
- Commit frequently during sessions

---

### Weekly Team Meetings

**Suggested Agenda:**

**Monday (Sprint Planning - 1 hour):**
- Review last week's accomplishments
- Plan week's tasks and PRs
- Identify blockers
- Assign new tasks

**Wednesday (Mid-week Sync - 30 minutes):**
- Quick status updates
- Address blockers
- Mini code review sessions
- Q&A

**Friday (Retrospective - 45 minutes):**
- What went well
- What can be improved
- Learning highlights
- Next week preview

---

## N. Final Deliverables

### Deliverable Checklist

#### Phase 0-1: Foundation
- [ ] .NET 7 solution with 5 projects created
- [ ] All project references configured correctly
- [ ] Git repository initialized with proper .gitignore
- [ ] README with setup instructions complete
- [ ] Domain entities implemented (Student, Teacher, Course, Enrollment)
- [ ] Domain interfaces defined (repositories, UnitOfWork)
- [ ] Value objects created (Email, Grade)

#### Phase 2: Infrastructure
- [ ] DbContext implemented with Fluent API
- [ ] Generic repository implemented
- [ ] Entity-specific repositories implemented
- [ ] Unit of Work pattern implemented
- [ ] Initial database migration created
- [ ] Seed data configured
- [ ] Infrastructure services registered in DI

#### Phase 3: Application Layer
- [ ] DTOs created for all entities
- [ ] AutoMapper configured with mapping profiles
- [ ] Service interfaces defined
- [ ] All service implementations complete (Student, Teacher, Course, Enrollment)
- [ ] Application services registered in DI

#### Phase 4: API Layer
- [ ] All controllers implemented (Students, Teachers, Courses, Enrollments)
- [ ] Global exception handling middleware working
- [ ] Request/response logging middleware implemented
- [ ] Swagger configured and documented
- [ ] Secure CORS policy configured

#### Phase 5: Cross-Cutting Concerns
- [ ] Serilog integrated and logging to file + console
- [ ] JWT configuration set up
- [ ] User entity and repository implemented
- [ ] Authentication service implemented
- [ ] Auth controller with register/login endpoints
- [ ] JWT authentication middleware configured
- [ ] Protected endpoints with [Authorize] attribute

#### Phase 6: Testing & CI/CD
- [ ] xUnit test project created
- [ ] Sample unit tests written and passing
- [ ] Sample integration tests written and passing
- [ ] GitHub Actions CI/CD pipeline configured
- [ ] All tests passing in CI pipeline

#### Phase 7: Optional Enhancement
- [ ] MediatR installed and configured (if applicable)
- [ ] Sample CQRS handlers implemented (if applicable)
- [ ] FluentValidation integrated (if applicable)

---

### Recommended Merge Sequence

**Week 1:**
```
main ← Task 0.1 (Solution Structure - Ali)
main ← Task 0.2 (Git Setup - Esi)
main ← Task 0.3 (Dev Docs - Shervin)
```

**Week 2:**
```
main ← Task 1.1 (Domain Entities - Ali)
main ← Task 1.2 (Interfaces - Shervin, based on 1.1)
main ← Task 1.3 (Value Objects - Esi, based on 1.1)
```

**Week 3-4:**
```
main ← Task 2.1 (EF Entities - Ali)
main ← Task 2.2 (DbContext - Ali, based on 2.1)
main ← Task 2.3 (Generic Repo - Esi, based on 2.2)
main ← Task 2.4 (Entity Repos - Shervin, based on 2.3)
main ← Task 2.5 (Unit of Work - Ali, based on 2.4)
main ← Task 2.6 (Migration - Esi, based on 2.5)
```

**Week 5-6:**
```
main ← Task 3.1 (DTOs - Shervin)
main ← Task 3.2 (AutoMapper - Esi, based on 3.1)
main ← Task 3.3 (Service Interfaces - Ali)
main ← Task 3.4, 3.5, 3.6, 3.7 (Services - parallel, based on 3.2 & 3.3)
main ← Task 3.8 (DI Registration - Ali, after all services merged)
```

**Week 7-8:**
```
main ← Task 4.1, 4.2, 4.3, 4.4 (Controllers - parallel after 3.8)
main ← Task 4.5 (Exception Middleware - Ali)
main ← Task 4.6 (Logging Middleware - Shervin, based on 4.5)
main ← Task 4.7 (Swagger - Esi)
main ← Task 4.8 (CORS - Ali)
```

**Week 9:**
```
main ← Task 5.1 (Serilog - Ali)
main ← Task 5.2 (JWT Settings - Ali, parallel with 5.1)
main ← Task 5.3 (User Entity - Esi, based on 5.2)
main ← Task 5.4 (Auth Service - Ali, based on 5.3)
main ← Task 5.5 (Auth Controller - Shervin, based on 5.4)
main ← Task 5.6 (JWT Middleware - Ali, based on 5.5)
```

**Week 10:**
```
main ← Task 6.1 (Test Project - Esi)
main ← Task 6.2 (Unit Tests - Shervin, based on 6.1)
main ← Task 6.3 (Integration Tests - Ali, based on 6.1)
main ← Task 6.4 (CI/CD - Ali)
```

**Week 11-12 (Optional):**
```
main ← Task 7.1 (MediatR Setup - Ali)
main ← Task 7.2 (CQRS Handlers - Ali, based on 7.1)
main ← Task 7.3 (FluentValidation - Ali, based on 7.2)
```

---

### Validation Gates

**Before Each Merge:**
1. **Build Check:** `dotnet build` succeeds with 0 errors
2. **Test Check:** `dotnet test` all tests pass
3. **Code Review:** At least 1 approval (Ali for beginner PRs)
4. **Merge Conflicts:** Resolved correctly
5. **CI Pipeline:** GitHub Actions passes

**Before Phase Completion:**
1. All tasks in phase merged to main
2. Documentation updated
3. Manual testing completed
4. Demo prepared for stakeholders

**Before Project Completion:**
1. All deliverables checked off
2. End-to-end testing completed
3. Documentation finalized
4. Deployment guide prepared
5. User guide created

---

### Success Criteria

**Technical:**
- [ ] Solution builds without errors or warnings
- [ ] All tests pass (unit + integration)
- [ ] API endpoints functional and documented
- [ ] Authentication working correctly
- [ ] Logging captures all important events
- [ ] Exception handling prevents crashes
- [ ] Database migrations apply successfully

**Architectural:**
- [ ] Clean Architecture principles followed
- [ ] Separation of concerns maintained
- [ ] Dependency injection used throughout
- [ ] Repository and Unit of Work patterns implemented
- [ ] Domain layer independent of infrastructure

**Team:**
- [ ] All team members contributed meaningfully
- [ ] Beginners showed skill progression
- [ ] Code reviews conducted professionally
- [ ] Git workflow followed consistently
- [ ] Documentation maintained throughout

**Quality:**
- [ ] Code is readable and well-commented
- [ ] Naming conventions consistent
- [ ] No obvious security vulnerabilities
- [ ] Performance is acceptable
- [ ] Error messages are helpful

---

### Project Completion Celebration

**When all deliverables are done:**
1. Final demo to stakeholders
2. Team retrospective on entire project
3. Individual skill assessments and feedback
4. Lessons learned documentation
5. Next project planning (if applicable)

**For Beginners (Shervin & Esi):**
- Showcase your growth journey
- Identify areas of expertise developed
- Plan continued learning path
- Update portfolios with project

**For Mentor (Ali):**
- Reflect on teaching effectiveness
- Document mentorship lessons learned
- Provide references/recommendations
- Continue supporting team growth

---

## Appendix: Quick Command Reference

### .NET CLI Commands

```bash
# Solution Management
dotnet new sln -n <SolutionName>
dotnet sln add <ProjectPath>
dotnet sln list

# Project Management
dotnet new webapi -n <ProjectName> -f net7.0
dotnet new classlib -n <ProjectName> -f net7.0
dotnet new xunit -n <ProjectName> -f net7.0

# Project References
dotnet add <Project> reference <ReferencedProject>
dotnet list reference

# Package Management
dotnet add package <PackageName> --version <Version>
dotnet remove package <PackageName>
dotnet list package

# Build and Run
dotnet restore
dotnet build
dotnet run --project <ProjectPath>
dotnet watch run

# Database Migrations
dotnet ef migrations add <MigrationName> --project <InfraProject> --startup-project <APIProject>
dotnet ef database update --project <InfraProject> --startup-project <APIProject>
dotnet ef migrations remove --project <InfraProject> --startup-project <APIProject>
dotnet ef database drop --project <InfraProject> --startup-project <APIProject>

# Testing
dotnet test
dotnet test --logger "console;verbosity=detailed"
dotnet test --collect:"XPlat Code Coverage"
```

### SQL Server Commands

```sql
-- Check database exists
SELECT name FROM sys.databases WHERE name = 'SchoolDb';

-- View tables
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';

-- View migration history
SELECT * FROM __EFMigrationsHistory;

-- Quick data check
SELECT COUNT(*) FROM Students;
SELECT COUNT(*) FROM Teachers;
SELECT COUNT(*) FROM Courses;
SELECT COUNT(*) FROM Enrollments;
```

---

**End of Refactor Plan**

**Total Tasks:** ~45 tasks across 7 phases
**Estimated Duration:** 10-12 weeks (core implementation)
**Team Size:** 3 developers (1 experienced, 2 beginners)
**Target Framework:** .NET 7 with EF Core 7.0.20

**Next Steps:**
1. Review this plan with the entire team
2. Set up development environments
3. Create GitHub repository
4. Begin with Phase 0: Project Setup
5. Hold weekly meetings as outlined
6. Track progress and adjust timeline as needed

**Good luck with the refactor! 🚀**
