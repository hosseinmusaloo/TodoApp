# TodoApp
A lightweight and efficient Todo List Web API built using Minimal APIs for simplicity and performance.

## How to Run the Project

### ✅ Requirements
- [.NET SDK 9.0+](https://dotnet.microsoft.com/)
- PostgreSQL database (Configured in appsettings.Development.json)

### ▶️ Run the Project

1. In the root folder, run the following:
   ```bash
   dotnet run --project Todo.Api
   ```
2. The API will start at:
   ```
   https://localhost:7034/
   ```
3. If you're in development mode, you can access:
   - Scalar UI: https://localhost:7034/scalar/v1
   - OpenAPI JSON: https://localhost:7034/openapi/v1.json


4. The database is automatically created and updated using EF Core migrations at Program.cs.
