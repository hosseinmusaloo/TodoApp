using System.Net;
using System.Net.Http.Json;
using Todo.Api.Tasks.GetTaskById;

namespace Todo.Api.Tests.Tasks;

public class GetTaskByIdTests(TodoWebAppFactory factory) : IntegrationTestFixture(factory)
{
    [Fact]
    public async Task GetById_ShouldSucceed_WhenTaskExists()
    {
        //Arrange
        var client = CreateClient();

        //Create a Task
        var request = TestData.CreateTask();
        var createResponse = await client.PostAsJsonAsync("api/tasks", request);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(createdTask);

        //Act
        var response = await client.GetAsync($"/api/tasks/{createdTask.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var task = await response.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(task);
        Assert.Equal(createdTask.Id, task.Id);
        Assert.Equal(createdTask.Title, task.Title);
        Assert.Equal(createdTask.Description, task.Description);
    }
}