using System.Net;
using System.Net.Http.Json;
using Todo.Api.Tasks.GetTaskById;

namespace Todo.Api.Tests.Tasks;

public class DeleteTaskTests(TodoWebAppFactory factory) : IntegrationTestFixture(factory)
{
    [Fact]
    public async Task Delete_ShouldSucceed_WhenTaskExists()
    {
        //Arrange
        var client = CreateClient();

        //Create a Task
        var request = TestData.CreateTask();
        var createResponse = await client.PostAsJsonAsync("api/tasks", request);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(createdTask);

        //Act
        var response = await client.DeleteAsync($"/api/tasks/{createdTask.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //Verify the deletion
        var getResponse = await client.GetAsync($"/api/tasks/{createdTask.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}