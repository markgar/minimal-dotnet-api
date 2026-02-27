# Review Themes

Last updated: Scaffolding & Health Endpoint

1. **Scaffold cleanup** — Always strip scaffold defaults (placeholder routes, `launchBrowser: true`, auto-generated comments) before committing; they expand the API surface and mislead readers.
2. **Simultaneous documentation updates** — When changing project structure or conventions, update ALL affected docs (`SPEC.md`, `copilot-instructions.md`, `README.md`) in the same commit; partial updates leave stale references that compound across milestones.
