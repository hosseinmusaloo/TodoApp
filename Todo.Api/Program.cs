using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Todo.Api.Data;
using Todo.Api.Tasks;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddOpenApi();

    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();

        app.Map("/", () => Results.Redirect("/scalar/v1"));
    }

    await EnsureDatabaseCreated(app);

    app.UseHttpsRedirection();

    app.MapGroup("/api")
       .MapTasksEndpoints();

    app.Run();
}

static async System.Threading.Tasks.Task EnsureDatabaseCreated(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}