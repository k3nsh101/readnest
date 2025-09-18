using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public interface IHabitRepository
{
    public Task<List<Habit>> GetAllHabits();
    public Task<Habit?> GetHabitById(Guid id);
    public Task<Habit> AddHabit(Habit habit);
    public Task<bool> UpdateHabit(Guid id, UpdateHabitDto updateHabitDto);
    public Task<bool> DeleteHabit(Guid id);
}
