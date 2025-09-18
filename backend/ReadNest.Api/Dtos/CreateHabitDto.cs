namespace ReadNest.Dtos;

public record CreateHabitDto(
    Guid Id,
    HabitTypes HabitType,
    int HabitValue
);
