using System.Net;
using System.Net.Http.Json;
using Todo.Api.Tasks.GetTaskById;
using Todo.Api.Tasks.UpdateTask;

namespace Todo.Api.Tests.Tasks;

public class UpdateTaskTests(TodoWebAppFactory factory) : IntegrationTestFixture(factory)
{
    [Fact]
    public async Task Update_ShouldSucceed_WithValidParameters()
    {
        //Arrange
        await CleanupDatabaseAsync();
        var client = CreateClient();

        //Create a Task
        var request = TestData.CreateTask();
        var createResponse = await client.PostAsJsonAsync("api/tasks", request);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(createdTask);

        //Update the Task
        var updateRequest = new UpdateTaskRequest(
            createdTask.Id,
            "Update Resume",
            "Add the latest project",
            true
        );

        //Act
        var response = await client.PutAsJsonAsync($"/api/tasks/{createdTask.Id}", updateRequest);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //Verify the update
        var getResponse = await client.GetAsync($"/api/tasks/{createdTask.Id}");
        var updatedTask = await getResponse.Content.ReadFromJsonAsync<TaskDetailResponse>();
        Assert.NotNull(updatedTask);
        Assert.Equal(updateRequest.Id, updatedTask.Id);
        Assert.Equal(updateRequest.Title, updatedTask.Title);
        Assert.Equal(updateRequest.Description, updatedTask.Description);
    }
}