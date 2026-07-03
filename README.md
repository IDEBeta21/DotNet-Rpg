# DotNet RPG API

An ASP.NET Core 8 Web API for managing RPG characters with JWT authentication. Built as an educational project for learning WebAPI development with .NET and Docker on Linux.

---

## Features

- JWT-based user authentication (register & login)
- Character CRUD operations scoped per authenticated user
- PostgreSQL database with Entity Framework Core
- AutoMapper for DTO mapping
- Swagger / OpenAPI documentation UI
- Docker Compose setup (API + PostgreSQL containers)
- HTTPS support with SSL certificates
- Unit tests with xUnit, Moq, and FluentAssertions

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8.0 |
| Language | C# / .NET 8.0 |
| Database | PostgreSQL (Npgsql EF Core 8.0.4) |
| ORM | Entity Framework Core 8.0.8 |
| Authentication | JWT Bearer 8.0.8 |
| Object Mapping | AutoMapper 12.0.1 |
| API Docs | Swashbuckle (Swagger) 6.7.3 |
| Testing | xUnit 2.9.0, Moq 4.20.70, FluentAssertions 6.12.0 |
| Containerization | Docker + Docker Compose |

---

## Project Structure

```
DotNet-Rpg/
├── apps/
│   └── api/
│       ├── DotNetRPG.API/
│       │   ├── Controllers/        # AuthController, CharacterController, HealthCheckController
│       │   ├── Data/               # DbContext, AuthRepository
│       │   ├── Models/
│       │   │   ├── Dtos/           # Request/response DTOs
│       │   │   ├── Character.cs
│       │   │   ├── User.cs
│       │   │   └── RpgClass.cs     # Character class enum
│       │   ├── Services/
│       │   │   └── CharacterService/
│       │   ├── Migrations/
│       │   ├── Program.cs
│       │   └── Dockerfile
│       ├── DotNetRPG.UnitTests/
│       └── DotNetRPGServices.sln
├── https/                          # SSL certificates (git-ignored)
├── docker-compose.yml
├── .env.example
└── README.md
```

---

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) and Docker Compose
- OpenSSL — required on Linux with .NET 8 for certificate generation

---

## Getting Started

### 1. Environment Setup

```bash
cp .env.example .env
```

Edit `.env` if you need to change database credentials or the certificate password.

### 2. HTTPS Certificate Setup

#### Windows

```bash
dotnet dev-certs https -ep https\aspnetcore.pfx -p <PASSWORD_CREDENTIAL>
dotnet dev-certs https --trust
```

#### Ubuntu / Linux — .NET 9 and higher

```bash
mkdir https
dotnet dev-certs https -ep https/aspnetcore.pfx -p <PASSWORD_CREDENTIAL>
dotnet dev-certs https --trust
```

#### Ubuntu / Linux — .NET 8 or lower

```bash
mkdir https && cd https

# Generate certificate files
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
  -keyout aspnetcore.key -out aspnetcore.crt \
  -subj "/CN=localhost"

# Convert to .pfx (you will be prompted for a password)
openssl pkcs12 -export -out aspnetcore.pfx \
  -inkey aspnetcore.key -in aspnetcore.crt

# Optional: remove intermediate files
rm aspnetcore.key aspnetcore.crt

cd ..
dotnet dev-certs https --trust
```

> **Note:** Use the password defined in your `.env` file (see `PASSWORD_CREDENTIAL` in `.env.example`).

### 3. Run with Docker

```bash
docker compose up --build -d
```

This starts two containers:
- `dotnet-rpg-api` — ASP.NET Core API on ports 5000 (HTTP) and 5001 (HTTPS)
- `dotnet-rpg-db` — PostgreSQL on port 5434

### 4. Run Locally (without Docker)

Ensure a PostgreSQL instance is running and your connection string in `appsettings.Development.json` is correct, then:

```bash
cd apps/api

# Apply migrations
dotnet ef database update --project DotNetRPG.API

# Start the API
dotnet run --project DotNetRPG.API
```

---

## API Reference

### Auth

| Method | Endpoint | Auth Required | Description |
|--------|----------|:---:|-------------|
| `POST` | `/auth/Register` | No | Register a new user |
| `POST` | `/auth/Login` | No | Login and receive a JWT token |

### Characters

| Method | Endpoint | Auth Required | Description |
|--------|----------|:---:|-------------|
| `GET` | `/api/character/GetAllCharacters` | JWT | Get all characters for the current user |
| `POST` | `/api/character/GetCharacterById` | JWT | Get a single character by ID |
| `POST` | `/api/character/AddCharacter` | JWT | Create a new character |
| `PUT` | `/api/character/UpdateCharacter` | JWT | Update an existing character |
| `DELETE` | `/api/character/DeleteCharacterById` | JWT | Delete a character |

### Health Check

| Method | Endpoint | Auth Required | Description |
|--------|----------|:---:|-------------|
| `GET` | `/api/healthcheck/dbcheck` | No | Verify database connectivity |

---

## Swagger UI

Once the API is running, open the interactive docs at:

```
https://localhost:5001/swagger
```

Use the **Authorize** button to provide your JWT token before testing protected endpoints.

---

## Running Tests

```bash
cd apps/api/DotNetRPG.UnitTests
dotnet test
```

---

## Database Migrations

Install the EF Core CLI tool if you haven't already:

```bash
dotnet tool install --global dotnet-ef
```

Apply migrations:

```bash
cd apps/api
dotnet ef database update --project DotNetRPG.API
```

---

## Notes

This project was created to enhance my knowledge in WebAPI development with .NET and Docker on Linux. It serves as both a learning exercise and a personal reference for common patterns and scenarios in .NET API development.

Feel free to use it as a reference or starting point for your own projects.
