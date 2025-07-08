namespace Todo.Api.Tasks.UpdateTask;

public record UpdateTaskRequest(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted
);