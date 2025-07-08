namespace Todo.Api.Tasks.GetTaskById;

public record TaskDetailResponse(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime DueDate
);