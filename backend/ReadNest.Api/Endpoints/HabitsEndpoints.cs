using ReadNest.Repositories;
using ReadNest.Mapping;
using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Endpoints;

public static class HabitsEndpoints
{
    public static void MapHabitsEndpoints(this IEndpointRouteBuilder app)
    {
        var habitsGroup = app.MapGroup("/habits");

        habitsGroup.MapGet("/", async (IHabitRepository repo) =>
        {
            var habits = await repo.GetAllHabits();
            return Results.Ok(habits.Select(HabitMapping.ToDto));
        });

        habitsGroup.MapGet("/{id}", async (Guid id, IHabitRepository repo) =>
        {
            var habit = await repo.GetHabitById(id);
            return habit is null ? Results.NotFound() : Results.Ok(habit.ToDto());
        });

        habitsGroup.MapPost("/", async (CreateHabitDto newHabit, IHabitRepository repo) =>
        {
            Habit habit = newHabit.ToEntity();
            var createdHabit = await repo.AddHabit(habit);

            return Results.Ok(createdHabit.ToDto());
        });

        habitsGroup.MapPut("/{id}", async (Guid id, UpdateHabitDto habit, IHabitRepository repo) =>
        {
            var success = await repo.UpdateHabit(id, habit);

            return success ? Results.NoContent() : Results.NotFound();
        });

        habitsGroup.MapDelete("/{id}", async (Guid id, IHabitRepository repo) =>
        {
            var success = await repo.DeleteHabit(id);

            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
