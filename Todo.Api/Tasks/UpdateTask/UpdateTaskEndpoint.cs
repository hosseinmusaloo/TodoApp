using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Common;
using Todo.Api.Common.Extensions;
using Todo.Api.Data;

namespace Todo.Api.Tasks.UpdateTask;

public class UpdateTaskEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPut("/{id:guid}", Handle)
        .WithSummary("Updates a task")
        .WithRequestValidation<UpdateTaskRequest>();

    private static async Task<Results<Ok, NotFound, BadRequest>> Handle(
        Guid id,
        UpdateTaskRequest request,
        AppDbContext db,
        CancellationToken ct)
    {
        if (id != request.Id)
            return TypedResults.BadRequest();

        var rowsAffected = await db.Tasks
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(updates => updates
                .SetProperty(t => t.Title, request.Title)
                .SetProperty(t => t.Description, request.Description)
                .SetProperty(t => t.IsCompleted, request.IsCompleted), ct);

        return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
    }
}