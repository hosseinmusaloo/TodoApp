using Todo.Api.Common.Extensions;
using Todo.Api.Tasks.CreateTask;
using Todo.Api.Tasks.DeleteTask;
using Todo.Api.Tasks.GetTaskById;
using Todo.Api.Tasks.GetTasks;
using Todo.Api.Tasks.UpdateTask;

namespace Todo.Api.Tasks;

public static class TaskEndpoints
{
    public static void MapTasksEndpoints(this IEndpointRouteBuilder app)
    {
        const string basePath = "/tasks";
        const string tag = "Tasks";

        var group = app
            .MapGroup(basePath)
            .WithTags(tag);

        group
            .MapEndpoint<GetTasksEndpoint>()
            .MapEndpoint<GetTaskByIdEndpoint>()
            .MapEndpoint<CreateTaskEndpoint>()
            .MapEndpoint<UpdateTaskEndpoint>()
            .MapEndpoint<DeleteTaskEndpoint>();
    }
}