using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;

namespace Todo.Api.Tests;

public class TodoWebAppFactory : WebApplicationFactory<Program>
{
    // Currently using a hardcoded local PostgreSQL DB.
    // Replace with Testcontainers for full test isolation in the future.
    private readonly NpgsqlConnection _npgsqlConnection =
        new("Host=localhost;Port=5432;Database=todo-app-testing-db;Username=postgres;password=password"); 

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Default", _npgsqlConnection.ConnectionString);
    }
}