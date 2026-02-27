# Copilot Instructions

## About this codebase

This software is written with assistance from GitHub Copilot. The code is structured to be readable, modifiable, and extendable by Copilot (and other LLM-based agents). Every design decision should reinforce that.

### Guidelines for LLM-friendly code

- **Flat, explicit control flow.** Prefer straightforward if/else and early returns over deeply nested logic, complex inheritance hierarchies, or metaprogramming. Every function should be understandable from its source alone.
- **Small, single-purpose functions.** Keep functions short (ideally under ~40 lines). Each function does one thing with a clear name that describes it. This gives the LLM better context boundaries.
- **Descriptive naming over comments.** Variable and function names should make intent obvious. Use comments only when *why* isn't clear from the code — never to explain *what*.
- **Colocate related logic.** Keep constants, helpers, and the code that uses them close together (or in the same small file). Avoid scattering related pieces across many modules — LLMs work best when relevant context is nearby.
- **Consistent patterns.** When multiple functions do similar things, structure them identically. Consistent shape lets the LLM reliably extend the pattern.
- **No magic.** Avoid decorators that hide behavior, dynamic attribute access, implicit registration, or monkey-patching. Everything should be traceable by reading the code top-to-bottom.
- **Graceful error handling.** Wrap I/O and external calls in try/except (or the language's equivalent). Never let a transient failure crash the main workflow. Log the error and continue.
- **Minimal dependencies.** Only add a dependency when it provides substantial value. Fewer deps mean less surface area for the LLM to misunderstand.
- **One concept per file.** Each module owns a single concern. Don't mix unrelated responsibilities in the same file.
- **Design for testability.** Separate pure decision logic from I/O and subprocess calls so core functions can be tested without mocking. Pass dependencies as arguments rather than hard-coding them inside functions when practical. Keep side-effect-free helpers (parsing, validation, data transforms) in their own functions so they can be unit tested directly.

### Documentation maintenance

- When completing a task that changes the project structure, key files, architecture, or conventions, update `.github/copilot-instructions.md` to reflect the change.
- Keep the project-specific sections (Project structure, Key files, Architecture, Conventions) accurate and current.
- Never modify the coding guidelines or testing conventions sections above.
- This file is a **style guide**, not a spec. Describe file **roles** (e.g. 'server entry point'), not implementation details (e.g. 'uses List<T> with auto-incrementing IDs'). Conventions describe coding **patterns** (e.g. 'consistent JSON error envelope'), not implementation choices (e.g. 'store data in a static variable'). SPEC.md covers what to build — this file covers how to write code that fits the project.

## Project structure

This is a single-project .NET 10 minimal API. All application code lives at the repository root — there are no subdirectories for layers, services, or modules. The `.csproj` project file and `Program.cs` entry point sit alongside configuration files at the top level.

## Key files

- `SPEC.md` — technical specification and acceptance criteria
- `REQUIREMENTS.md` — original project requirements
- `BACKLOG.md` — prioritised work backlog
- `JOURNEYS.md` — user journey descriptions

## Architecture

The service is a single-layer ASP.NET Core minimal API with no separation between application and infrastructure concerns. All route handlers are registered directly in `Program.cs`, eliminating the MVC controller pipeline entirely. Request handling flows from the ASP.NET Core middleware stack straight into inline lambda handlers. The built-in dependency injection container is used only for framework-provided services such as `ILogger`. There are no external dependencies, datastores, or inter-process calls.

## Testing conventions

- **Use the project's test framework.** Plain functions with descriptive names.
- **Test the contract, not the implementation.** A test should describe expected behavior in terms a user would understand — not mirror the code's internal branching. If the test would break when you refactor internals without changing behavior, it's too tightly coupled.
- **Name tests as behavioral expectations.** `test_expired_token_triggers_refresh` not `test_check_token_returns_false`. The test name should read like a requirement.
- **Use realistic inputs.** Feed real-looking data, not minimal one-line synthetic strings. Edge cases should be things that could actually happen — corrupted inputs, empty files, missing fields.
- **Prefer regression tests.** When a bug is found, write the test that would have caught it before fixing it. This is the highest-value test you can write.
- **Don't test I/O wrappers.** Functions that just read a file and call a pure helper don't need their own tests — test the pure helper directly.
- **No mocking unless unavoidable.** Extract pure functions for testability so you don't need mocks. If you find yourself mocking, consider whether you should be testing a different function.

## Conventions

- **Inline route handlers.** All endpoint logic is registered directly via `app.MapGet(...)` and similar methods — no controller classes, no action filters.
- **Consistent JSON response shape.** All API responses return a plain anonymous object serialised to JSON; error responses follow the same envelope pattern as success responses.
- **Minimal middleware.** Only add middleware that is explicitly required. Avoid registering services or pipeline components that are not used.
- **Built-in logging only.** Use the injected `ILogger<T>` from the framework; no third-party logging libraries.
- **Configuration via `appsettings.json`.** Read configuration through the built-in `IConfiguration` abstraction; no custom config classes unless complexity warrants it.
