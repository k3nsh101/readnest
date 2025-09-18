using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly AppDbContext _appDbContext;

    public HabitRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Habit>> GetAllHabits()
    {
        return await _appDbContext.Habits.ToListAsync();
    }

    public async Task<Habit?> GetHabitById(Guid id)
    {
        var habit = await _appDbContext.Habits.FindAsync(id);

        if (habit == null)
            return null;

        return habit;
    }

    public async Task<Habit> AddHabit(Habit newHabit)
    {
        _appDbContext.Habits.Add(newHabit);
        await _appDbContext.SaveChangesAsync();

        return newHabit;
    }

    public async Task<bool> UpdateHabit(Guid id, UpdateHabitDto updateHabitDto)
    {
        var habit = await _appDbContext.Habits.FindAsync(id);

        if (habit == null)
            return false;

        habit.HabitValue = updateHabitDto.HabitValue;
        habit.UpdatedAt = DateTime.UtcNow;

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteHabit(Guid id)
    {
        var habit = await _appDbContext.Habits.FindAsync(id);

        if (habit == null)
        {
            return false;
        }

        _appDbContext.Habits.Remove(habit);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
