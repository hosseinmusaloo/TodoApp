using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Common;
using Todo.Api.Data;

namespace Todo.Api.Tasks.DeleteTask;

public class DeleteTaskEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapDelete("/{id:guid}", Handle)
        .WithSummary("Deletes a task");

    private static async Task<Results<Ok, NotFound>> Handle(Guid id, AppDbContext db, CancellationToken ct)
    {
        var rowsAffected = await db.Tasks
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);

        return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
    }
}