# .NET + Angular Full-Stack Starter

A production-ready full-stack application template with .NET 9 backend, Angular 20 frontend, and SQL Server - fully containerized with Docker.

## ⭐ Features

- ✅ **122 Passing Tests** (50 backend + 72 frontend)
- ✅ **Clean Architecture** - Separation of concerns with 4 layers
- ✅ **Docker Compose** - One-command setup for all services
- ✅ **Comprehensive Templates** - Battle-tested documentation for both stacks
- ✅ **E2E Testing** - Playwright integration tests
- ✅ **Production-Ready** - Nginx, health checks, migrations

## 🏗️ Architecture

**Backend - Clean Architecture** with 4 layers:
- **PersonApi.Domain**: Entities and domain models
- **PersonApi.Application**: DTOs, Services, Business Logic
- **PersonApi.Infrastructure**: EF Core, DbContext, Repositories
- **PersonApi.API**: Controllers, Middleware, API endpoints

**Frontend - Angular 20**:
- Standalone components (no NgModules)
- Reactive Forms with validation
- HTTP service with proper error handling
- Component-based architecture

## 🚀 Tech Stack

### Backend
- **.NET 9.0** - Latest ASP.NET Core
- **Entity Framework Core 9** - ORM with SQL Server
- **SQL Server (Azure SQL Edge)** - ARM64 compatible for M-series Mac
- **xUnit, Moq, FluentAssertions** - Unit testing
- **Testcontainers** - Integration testing

### Frontend
- **Angular 20** - Latest with standalone components
- **TypeScript 5.9** - Strict typing
- **RxJS 7.8** - Reactive programming
- **Bootstrap 5.3** - UI styling
- **Jasmine/Karma** - Unit testing
- **Playwright** - E2E testing

### DevOps
- **Docker & Docker Compose** - Containerization
- **Nginx** - Production web server
- **Swagger/OpenAPI** - API documentation
- **Make** - Build automation

## 📋 Prerequisites

- .NET 9 SDK
- Node.js 20+ & npm
- Docker Desktop
- Make (optional, for automation)

## 🛠️ Quick Start

### 1. Clone Repository

```bash
git clone https://github.com/maddinenisri/dotnet-angular-starter.git
cd dotnet-angular-starter
```

### 2. Setup Dependencies

```bash
make setup
```

### 3. Start All Services

```bash
make docker-up
```

This command will:
- Build and start SQL Server (Azure SQL Edge)
- Build and start .NET 9 API
- Build and start Angular 20 app
- Run EF Core migrations
- Initialize database

**Access Points:**
- **API**: http://localhost:5001
- **Swagger UI**: http://localhost:5001/swagger
- **Angular App**: http://localhost:4200
- **SQL Server**: localhost:1433 (sa/YourStrong@Password123)

### 4. Stop Services

```bash
make docker-down
```

## 📖 Available Make Commands

```bash
make help          # Show all available commands
make setup         # Install dependencies
make build         # Build .NET solution
make test          # Run all tests (122 tests)
make docker-up     # Start all services with Docker
make docker-down   # Stop all services
make migrate       # Run EF Core migrations
make run-api       # Run API locally (without Docker)
make run-web       # Run Angular app locally
make clean         # Clean build artifacts
make db-reset      # Reset database (WARNING: Deletes data!)
```

## 🗂️ Project Structure

```
dotnet-angular-starter/
├── src/
│   ├── PersonApi.API/              # Web API layer
│   ├── PersonApi.Application/       # Business logic layer
│   ├── PersonApi.Domain/            # Domain entities
│   └── PersonApi.Infrastructure/    # Data access layer
├── tests/
│   ├── PersonApi.UnitTests/         # 37 unit tests
│   └── PersonApi.IntegrationTests/  # 13 integration tests (Testcontainers)
├── web/
│   └── person-app/                  # Angular 20 frontend
│       ├── src/app/                 # Application source
│       ├── e2e/                     # Playwright E2E tests
│       └── Dockerfile               # Multi-stage build
├── docker/
│   └── sql/                         # SQL initialization scripts
├── docs/                            # Product and feature requirements
│   └── floorplan-builder-requirements.md
├── DOTNET_APPLICATION_TEMPLATE.md   # .NET template (50 tests)
├── ANGULAR_APPLICATION_TEMPLATE.md  # Angular template (72 tests)
├── docker-compose.yml               # Docker orchestration
├── Makefile                         # Build automation
└── README.md                        # This file
```

## 🔌 API Endpoints

### Persons API

- `GET /api/persons` - Get all persons (paginated)
  - Query params: `pageNumber` (default: 1), `pageSize` (default: 10)
- `GET /api/persons/{id}` - Get person by ID
- `POST /api/persons` - Create new person
- `PUT /api/persons/{id}` - Update person
- `DELETE /api/persons/{id}` - Delete person
- `GET /api/persons/search?name={name}` - Search by name

### Health & Documentation

- `GET /health` - Health check endpoint
- `GET /swagger` - Swagger UI (interactive API docs)

## 🗃️ Person Entity

```json
{
  "id": 1,
  "name": "John Doe",
  "age": 30,
  "dateOfBirth": "1994-01-15T00:00:00Z",
  "skills": ["C#", ".NET", "Angular"],
  "createdAt": "2025-10-10T10:00:00Z",
  "updatedAt": "2025-10-10T11:00:00Z"
}
```

## 🧪 Testing

### Run All Tests (122 Total)

```bash
make test
```

**Test Coverage:**
- **Backend**: 50 tests passing
  - 37 unit tests (Controllers, Services, Repositories)
  - 13 integration tests (API endpoints with Testcontainers)
- **Frontend**: 72 tests passing
  - PersonService: 24 tests
  - PersonListComponent: 14 tests
  - PersonFormComponent: 34 tests

### Backend Tests Only

```bash
# Unit tests
dotnet test tests/PersonApi.UnitTests

# Integration tests (uses Testcontainers)
dotnet test tests/PersonApi.IntegrationTests
```

### Frontend Tests

```bash
cd web/person-app

# Unit tests
npm test

# Unit tests (headless)
npm test -- --browsers=ChromeHeadless --watch=false

# E2E tests (requires running application)
npm run test:e2e
```

## 🐳 Docker Configuration

### Services

1. **db** (SQL Server - Azure SQL Edge)
   - Port: 1433
   - User: sa
   - Password: YourStrong@Password123
   - ARM64 native support for Apple Silicon

2. **api** (.NET 9 API)
   - Port: 5001
   - Auto-runs migrations on startup
   - Health check enabled

3. **web** (Angular 20 + Nginx)
   - Port: 4200
   - Production build with Nginx
   - API proxy configured

## 🔧 Development Workflow

### Running Locally (Without Docker)

#### 1. Start SQL Server Only
```bash
docker-compose up db -d
```

#### 2. Run Migrations
```bash
make migrate
```

#### 3. Run API
```bash
make run-api
# API available at http://localhost:5001
```

#### 4. Run Angular App (in another terminal)
```bash
make run-web
# App available at http://localhost:4200
```

## 🗄️ Database Management

### View Migrations
```bash
dotnet ef migrations list --project src/PersonApi.Infrastructure --startup-project src/PersonApi.API
```

### Add New Migration
```bash
dotnet ef migrations add MigrationName --project src/PersonApi.Infrastructure --startup-project src/PersonApi.API
```

### Update Database
```bash
make migrate
```

### Reset Database (Deletes All Data)
```bash
make db-reset
```

## 📚 Documentation Templates

This repository includes comprehensive documentation templates:

### DOTNET_APPLICATION_TEMPLATE.md
- Complete .NET 9 setup guide
- Clean Architecture implementation
- Testing strategies (Unit + Integration)
- Docker configuration
- Best practices (validated with 50 passing tests)

### ANGULAR_APPLICATION_TEMPLATE.md
- Angular 20 standalone components guide
- Reactive Forms implementation
- Testing strategies (Unit + E2E)
- Common pitfalls and solutions
- Battle-tested patterns (validated with 72 passing tests)

## 🔐 Security Notes

**⚠️ For Development Only:**
- SA password is hardcoded for development
- CORS configured for localhost only

**For Production:**
- Use Azure Key Vault or environment secrets
- Update CORS policy in `Program.cs`
- Use HTTPS with valid certificates
- Implement authentication (JWT)

## 🌐 CORS Configuration

The API allows requests from:
- `http://localhost:4200` (Angular dev server)

To add more origins, update `Program.cs`:
```csharp
policy.WithOrigins("http://localhost:4200", "https://your-domain.com")
```

## 🐛 Troubleshooting

### SQL Server Connection Issues (Apple Silicon)
- Uses `azure-sql-edge` for ARM64 compatibility
- Allocate 8+ GB RAM to Docker Desktop

### Port Already in Use (5001 or 4200)
```bash
# Find process
sudo lsof -i :5001
sudo lsof -i :4200

# Kill process
kill -9 <PID>
```

### EF Core Tools Not Found
```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:~/.dotnet/tools"
```

### Angular Build Issues
```bash
cd web/person-app
rm -rf node_modules package-lock.json
npm install
```

### Docker Build Failures
```bash
# Clean Docker system
docker system prune -a

# Rebuild all services
make docker-down
make docker-up
```

## 📊 Testing the API

### Using Swagger UI
Navigate to http://localhost:5001/swagger

### Using curl
```bash
# Health check
curl http://localhost:5001/health

# Get all persons
curl http://localhost:5001/api/persons

# Create person
curl -X POST http://localhost:5001/api/persons \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "age": 30,
    "dateOfBirth": "1994-01-15",
    "skills": ["C#", ".NET", "Docker"]
  }'

# Get person by ID
curl http://localhost:5001/api/persons/1

# Update person
curl -X PUT http://localhost:5001/api/persons/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe Updated",
    "age": 31,
    "dateOfBirth": "1994-01-15",
    "skills": ["C#", ".NET", "Docker", "Angular"]
  }'

# Search persons
curl http://localhost:5001/api/persons/search?name=John

# Delete person
curl -X DELETE http://localhost:5001/api/persons/1
```

## ✅ Project Status

- ✅ **Backend API** - Complete with Clean Architecture
- ✅ **Frontend** - Angular 20 with standalone components
- ✅ **Docker Configuration** - Multi-container orchestration
- ✅ **Database Migrations** - EF Core with SQL Server
- ✅ **Unit Tests** - 72 frontend + 37 backend = 109 total
- ✅ **Integration Tests** - 13 tests with Testcontainers
- ✅ **E2E Tests** - Playwright framework configured
- ✅ **Documentation** - Comprehensive templates for both stacks
- ✅ **Build Automation** - Makefile with common tasks

**Total: 122 Passing Tests**

## 🚀 Future Enhancements

Potential additions (not implemented):
- 🔐 JWT Authentication & Authorization
- 📊 Structured Logging (Serilog)
- 🔍 Application Insights / OpenTelemetry
- 🚀 CI/CD Pipeline (GitHub Actions)
- ☁️ Cloud Deployment (Azure/AWS)
- 📦 NuGet Package Publishing
- 🌍 Internationalization (i18n)

## 📝 License

MIT License - Feel free to use this template for your projects.

## 👥 Contributing

This is a starter template. Feel free to:
- Fork the repository
- Submit issues
- Create pull requests
- Customize for your needs

## 📞 Support

For questions or issues:
- Review the documentation templates (`DOTNET_APPLICATION_TEMPLATE.md`, `ANGULAR_APPLICATION_TEMPLATE.md`)
- Check existing GitHub issues
- Create a new issue with details

---

**Built with .NET 9, Angular 20, and modern cloud-native practices**

**Repository**: https://github.com/maddinenisri/dotnet-angular-starter
