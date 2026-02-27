# Milestone 1: Scaffolding & Health Endpoint

> **Validates:** `GET /health` returns HTTP 200 with JSON body `{"status":"healthy"}`. The service must start cleanly via `dotnet run`. `dotnet test` must exit 0 with at least one passing test.

> **Reference files:** `Program.cs` (entry point and route registrations), `*.csproj` (project file targeting net10.0)

- [ ] Create a .NET 10 minimal API project at the repository root using `dotnet new web -n Api --framework net10.0` and confirm the generated `.csproj` targets `net10.0`
- [ ] Register `GET /health` in `Program.cs` that returns HTTP 200 with JSON body `{ "status": "healthy" }` using `Results.Ok(new { status = "healthy" })`
- [ ] Add an xUnit test project using `dotnet new xunit -n Api.Tests --framework net10.0` alongside the main project
- [ ] Add a project reference from `Api.Tests` to `Api` using `dotnet add Api.Tests/Api.Tests.csproj reference Api/Api.csproj`
- [ ] Write one placeholder test in `Api.Tests` named `PlaceholderTest` with a single fact `ApplicationStarts_PlaceholderPasses` that asserts `Assert.True(true)`
