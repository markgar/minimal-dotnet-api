# Minimal .NET API

A .NET 10 minimal API with a single health endpoint.

## Build & Run

```bash
dotnet restore
dotnet run
```

The API listens on `http://localhost:5000` by default.

## Develop

```bash
dotnet watch run   # hot-reload during development
dotnet test        # run tests (if present)
```

See [REQUIREMENTS.md](REQUIREMENTS.md) for feature requirements and [SPEC.md](SPEC.md) for technical decisions.
