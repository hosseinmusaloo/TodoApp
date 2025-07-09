using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Todo.Api.Tests;

public abstract class IntegrationTestFixture(TodoWebAppFactory factory) : IClassFixture<TodoWebAppFactory>
{
    public HttpClient CreateClient() => factory.CreateClient();

    protected async Task CleanupDatabaseAsync()
    {
        using var scope = factory.Services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("Default");
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(
            """
                DO $$
                BEGIN
                    TRUNCATE TABLE "Tasks";
                END $$;
            """, connection);

        await command.ExecuteNonQueryAsync();
    }
}