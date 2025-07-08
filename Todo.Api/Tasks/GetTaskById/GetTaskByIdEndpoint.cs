using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Common;
using Todo.Api.Data;

namespace Todo.Api.Tasks.GetTaskById;

public class GetTaskByIdEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/{id:guid}", Handle)
        .WithSummary("Gets a task by id");

    private static async Task<Results<Ok<TaskDetailResponse>, NotFound>> Handle(
        Guid id,
        AppDbContext db,
        CancellationToken ct)
    {
        var task = await db.Tasks
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TaskDetailResponse(
                t.Id,
                t.Title,
                t.Description,
                t.IsCompleted,
                t.DueDate))
            .FirstOrDefaultAsync(ct);

        return task is not null ? TypedResults.Ok(task) : TypedResults.NotFound();
    }
}