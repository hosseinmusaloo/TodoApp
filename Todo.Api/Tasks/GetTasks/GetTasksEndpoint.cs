using Microsoft.EntityFrameworkCore;
using Todo.Api.Common;
using Todo.Api.Data;

namespace Todo.Api.Tasks.GetTasks;

public class GetTasksEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/", Handle)
        .WithSummary("Gets all tasks");

    private static async Task<IList<TaskResponse>> Handle(AppDbContext db, CancellationToken ct)
    {
        var response = await db.Tasks
            .AsNoTracking()
            .Select(t => new TaskResponse(
               t.Id,
               t.Title,
               t.IsCompleted))
            .ToListAsync(ct);

        return response;
    }
}