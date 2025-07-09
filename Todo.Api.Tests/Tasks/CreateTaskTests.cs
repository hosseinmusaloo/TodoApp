using System.Net;
using System.Net.Http.Json;
using Todo.Api.Tasks.CreateTask;
using Todo.Api.Tasks.GetTaskById;

namespace Todo.Api.Tests.Tasks;

public class CreateTaskTests(TodoWebAppFactory factory) : IntegrationTestFixture(factory)
{
    [Fact]
    public async Task Create_ShouldSucceed_WithValidParameters()
    {
        //Arrange
        await CleanupDatabaseAsync();
        var request = new CreateTaskRequest(
            "Read book",
            "Read 30 pages",
            DateTime.Now.AddDays(1)
        );

        var client = CreateClient();

        //Act
        var response = await client.PostAsJsonAsync("api/tasks", request);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(await response.Content.ReadFromJsonAsync<TaskDetailResponse>());
    }
}