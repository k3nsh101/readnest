using System.Net.Http.Json;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ReadNest.Dtos;
using ReadNest.Entities;
using ReadNest.Data;

namespace ReadNest.Tests.Integration.Endpoint;

public class HabitEndpointTests
{
    [Fact]
    public async Task AddHabit_ReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var habit = new CreateHabitDto(HabitTypes.PagesPerDay, 30);
        var response = await client.PostAsJsonAsync("/habits", habit);
        var result = await response.Content.ReadFromJsonAsync<HabitDto>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(HabitTypes.PagesPerDay, result.HabitType);
        Assert.Equal(30, result.HabitValue);
    }

    [Fact]
    public async Task GetAllHabits_ReturnSuccessAndEmptyList_WhenEmpty()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/habits");
        var result = await response.Content.ReadFromJsonAsync<List<Habit>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllHabits_ReturnSuccessAndAllHabits()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var timestamp = DateTime.UtcNow;
        var habit1 = new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = HabitTypes.PagesPerDay,
            HabitValue = 30,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        var habit2 = new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = HabitTypes.PagesPerDay,
            HabitValue = 50,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        db.Habits.Add(habit1);
        db.Habits.Add(habit2);
        db.SaveChanges();

        var response = await client.GetAsync("/habits");
        var result = await response.Content.ReadFromJsonAsync<List<HabitDto>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Collection(result,
            h =>
            {
                Assert.Equal(habit1.Id, h.Id);
                Assert.Equal(habit1.HabitType, h.HabitType);
                Assert.Equal(habit1.HabitValue, h.HabitValue);
            },
            h =>
            {
                Assert.Equal(habit2.Id, h.Id);
                Assert.Equal(habit2.HabitType, h.HabitType);
                Assert.Equal(habit2.HabitValue, h.HabitValue);
            });
    }

    [Fact]
    public async Task UpdateHabit_ShouldReturnNoContent_WhenExists()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var timestamp = DateTime.UtcNow;
        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = HabitTypes.PagesPerDay,
            HabitValue = 30,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        db.Habits.Add(habit);
        db.SaveChanges();

        var jsonContent = System.Text.Json.JsonSerializer.Serialize(new
        {
            HabitValue = 50
        });
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/habits/{habit.Id}", httpContent);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateHabit_ShouldReturnNotFound_WhenHabitNotExists()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var timestamp = DateTime.UtcNow;

        var jsonContent = System.Text.Json.JsonSerializer.Serialize(new
        {
            HabitValue = 50
        });
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/habits/{Guid.NewGuid()}", httpContent);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteHabit_ShouldReturnNoContent_WhenExists()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var timestamp = DateTime.UtcNow;
        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = HabitTypes.PagesPerDay,
            HabitValue = 30,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        db.Habits.Add(habit);
        db.SaveChanges();

        var response = await client.DeleteAsync($"/habits/{habit.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteHabit_ShouldReturnNotFound_WhenHabitNotFound()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.DeleteAsync($"/habit/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
