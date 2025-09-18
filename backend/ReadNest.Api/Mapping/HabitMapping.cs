using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Mapping;

public static class HabitMapping
{
    public static Habit ToEntity(this CreateHabitDto habit)
    {
        var timestamp = DateTime.UtcNow;
        return new Habit
        {
            Id = Guid.NewGuid(),
            HabitType = habit.HabitType,
            HabitValue = habit.HabitValue,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };
    }

    public static HabitDto ToDto(this Habit habit)
    {
        return new(
            habit.Id,
            habit.HabitType,
            habit.HabitValue
        );
    }
}
