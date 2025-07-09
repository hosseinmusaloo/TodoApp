using System.Net;
using System.Net.Http.Json;
using Todo.Api.Tasks.GetTaskById;
using Todo.Api.Tasks.GetTasks;

namespace Todo.Api.Tests.Tasks;

public class GetTasksTests(TodoWebAppFactory factory) : IntegrationTestFixture(factory)
{
    [Fact]
    public async Task Get_ShouldReturnTasks_WhenTasksExist()
    {
        //Arrange
        var client = CreateClient();
        
        //Create a Task
        var request = TestData.CreateTask();
        var createResponse = await client.PostAsJsonAsync("api/tasks", request);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(createdTask);

        //Act
        var response = await client.GetAsync("/api/tasks");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(await response.Content.ReadFromJsonAsync<List<TaskResponse>>());
    }
}