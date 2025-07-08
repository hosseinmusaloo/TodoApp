using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Api.Common;
using Todo.Api.Common.Extensions;
using Todo.Api.Data;
using Todo.Api.Tasks.GetTaskById;

namespace Todo.Api.Tasks.CreateTask;

public class CreateTaskEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/", Handle)
        .WithSummary("Creates a new task")
        .WithRequestValidation<CreateTaskRequest>();

    private static async Task<Created<TaskDetailResponse>> Handle(
        CreateTaskRequest request, 
        AppDbContext db,
        CancellationToken ct)
    {
        var todo = new Task
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            DueDate = request.DueDate
        };

        db.Tasks.Add(todo);
        await db.SaveChangesAsync(ct);

        return TypedResults.Created($"/tasks/{todo.Id}",
            new TaskDetailResponse(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.IsCompleted,
                todo.DueDate
            ));
    }
}