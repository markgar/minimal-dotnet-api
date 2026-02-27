# Technical Specification

## Summary

A lightweight .NET 10 minimal API exposing a single health-check endpoint. The goal is the simplest possible deployable HTTP service — no persistence, no auth, no framework overhead beyond the .NET runtime itself.

## Tech Stack

- **Runtime**: .NET 10
- **Framework**: ASP.NET Core minimal API (`dotnet new web`)
- **Language**: C# 13
- **Test framework**: xUnit (if tests are added)
- **No ORM, no database, no authentication library**

## Architecture

- Single-project structure — no layers, no separate class library
- All route handlers registered directly in `Program.cs`
- No controllers, no MVC pipeline
- Dependency injection used only for built-in ASP.NET Core services

### Project Structure

```
/
├── Program.cs          # Entry point; all route registrations
├── *.csproj            # Project file targeting net10.0
├── appsettings.json    # Default ASP.NET Core config (kept minimal)
└── appsettings.Development.json
```

## Cross-Cutting Concerns

- **Authentication**: None. All endpoints are public.
- **Multi-tenancy**: Not applicable.
- **Error handling**: Default ASP.NET Core exception middleware; no custom error handler needed for this scope.
- **Logging**: Built-in ASP.NET Core `ILogger`; default console sink.
- **Configuration**: `appsettings.json` only; no secrets management required.
- **CORS**: Not configured (single-origin use case).

## Acceptance Criteria

- **Health endpoint is reachable**: `GET /health` responds with HTTP 200 and a JSON body `{ "status": "healthy" }`.
- **Service starts cleanly**: `dotnet run` starts without errors on a stock .NET 10 install.
- **No external dependencies required**: the service runs with only the .NET SDK installed.
