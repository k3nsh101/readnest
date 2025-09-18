namespace ReadNest.Dtos;

public record HabitDto(
    Guid Id,
    HabitTypes HabitType,
    int HabitValue
);
