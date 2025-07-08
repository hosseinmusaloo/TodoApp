namespace Todo.Api.Tasks.GetTasks;

public record TaskResponse(
    Guid Id,
    string Title,
    bool IsCompleted
);