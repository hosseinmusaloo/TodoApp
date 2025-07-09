using Todo.Api.Tasks.CreateTask;

namespace Todo.Api.Tests;

public static class TestData
{
    public static CreateTaskRequest CreateTask() => new("Read book", "Read 30 pages", DateTime.Now.AddDays(1));
}