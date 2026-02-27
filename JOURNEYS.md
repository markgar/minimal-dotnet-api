# User Journey Tests

## J-1: Basic health check smoke test
<!-- after: 1 -->
<!-- covers: health.endpoint -->
<!-- tags: smoke -->
Start the service with `dotnet run` → wait for the Kestrel startup log confirming the app is listening → send `GET /health` → verify HTTP 200 response → verify JSON body is exactly `{ "status": "healthy" }`.

## J-2: Service starts cleanly with no errors
<!-- after: 1 -->
<!-- covers: service.startup -->
Run `dotnet run` on a stock .NET 10 install → observe stdout/stderr for any error or exception messages → confirm the process reaches the "Now listening on" line without crashing → send `GET /health` to confirm the endpoint is live.

## J-3: Health endpoint returns correct JSON content type
<!-- after: 1 -->
<!-- covers: health.endpoint, api.response -->
Start the service → send `GET /health` with `Accept: application/json` header → verify HTTP 200 → verify `Content-Type` response header contains `application/json` → verify body parses as valid JSON with field `status` equal to `"healthy"`.

## J-4: Unsupported HTTP methods on /health are rejected
<!-- after: 1 -->
<!-- covers: health.endpoint, api.errors -->
Start the service → send `POST /health` → verify the response is NOT 200 (expect 405 Method Not Allowed) → send `DELETE /health` → verify 405 → send `PUT /health` → verify 405 → confirm that only `GET` is accepted.

## J-5: Unknown routes return 404
<!-- after: 1 -->
<!-- covers: api.errors, service.startup -->
Start the service → send `GET /` → verify HTTP 404 → send `GET /status` → verify 404 → send `GET /healthz` → verify 404 → confirm that `/health` still returns 200, demonstrating only registered routes respond.

## J-6: Repeated requests return consistent responses
<!-- after: 1 -->
<!-- covers: health.endpoint, api.response -->
Start the service → send `GET /health` ten times in sequence → verify every response has HTTP 200 → verify every response body is identical (`{ "status": "healthy" }`) → confirm no degradation or variance across calls.

## J-7: Service configuration loads without secrets or external dependencies
<!-- after: 1 -->
<!-- covers: service.startup, service.config -->
Ensure no external databases, caches, or secret stores are configured → run `dotnet run` → observe that the service starts using only `appsettings.json` and `appsettings.Development.json` → send `GET /health` → verify 200, confirming the app needs nothing beyond the .NET SDK.

## J-8: Health endpoint is publicly accessible (no auth required)
<!-- after: 1 -->
<!-- covers: health.endpoint, api.auth -->
Start the service → send `GET /health` with no `Authorization` header → verify HTTP 200 (not 401 or 403) → send `GET /health` with a random `Authorization: Bearer invalid-token` header → verify still HTTP 200, confirming no authentication is enforced.

## J-9: Response body structure validation
<!-- after: 1 -->
<!-- covers: api.response, health.endpoint -->
Start the service → send `GET /health` → parse the JSON response body → verify the object has exactly one key: `status` → verify the value of `status` is the string `"healthy"` → verify no extra fields are present (e.g., no `timestamp`, `version`, or `uptime`).

## J-10: Logging output during request handling
<!-- after: 1 -->
<!-- covers: service.startup, service.logging -->
Start the service with default console logging enabled → send `GET /health` → observe stdout for a request log line referencing the `/health` path → verify no ERROR or CRITICAL level log entries appear for a normal health request → confirm the service handles the request and logs it cleanly.
