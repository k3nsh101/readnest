using Microsoft.EntityFrameworkCore;
using ReadNest.Repositories;

namespace ReadNest.Tests.Integration.Repo;

public class HabitRepositoryTests
{
    [Fact]
    public async Task GetAllHabits_WhenNoHabits_ReturnEmptyList()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var repo = new HabitRepository(context);

        var habits = await repo.GetAllHabits();

        Assert.Empty(habits);
    }

    [Fact]
    public async Task GetAllHabits_ReturnAllHabits()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var repo = new HabitRepository(context);
        var datetime = DateTime.UtcNow;

        var habit1 = new Habit()
        {
            Id = Guid.NewGuid(),
            HabitType = 0,
            HabitValue = 30,
            CreatedAt = datetime,
            UpdatedAt = datetime
        };

        context.Habits.Add(habit1);
        await context.SaveChangesAsync();

        var habits = await repo.GetAllHabits();

        Assert.NotEmpty(habits);
        Assert.Single(habits);
        Assert.Equal([habit1], habits);
    }

    [Fact]
    public async Task AddHabit_ShouldSaveHabit()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var repo = new HabitRepository(context);
        var datetime = DateTime.UtcNow;
        var habit = new Habit()
        {
            Id = Guid.NewGuid(),
            HabitType = 0,
            HabitValue = 30,
            CreatedAt = datetime,
            UpdatedAt = datetime
        };

        var result = await repo.AddHabit(habit);
        var savedHabit = await context.Habits.FirstOrDefaultAsync();

        Assert.NotNull(savedHabit);
        Assert.Equal(habit, savedHabit);
    }

    [Fact]
    public async Task DeleteHabit_ShouldRemoveHabit_WhenExists()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var datetime = DateTime.UtcNow;
        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = 0,
            HabitValue = 30,
            CreatedAt = datetime,
            UpdatedAt = datetime
        };

        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        var repo = new HabitRepository(context);

        var result = await repo.DeleteHabit(habit.Id);
        var allHabits = await context.Habits.ToListAsync();

        Assert.True(result);
        Assert.Empty(allHabits);
    }

    [Fact]
    public async Task DeleteHabit_ShouldReturnFalse_WhenHabitNotFound()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var repo = new HabitRepository(context);

        var result = await repo.DeleteHabit(Guid.NewGuid());

        Assert.False(result);
    }
}
