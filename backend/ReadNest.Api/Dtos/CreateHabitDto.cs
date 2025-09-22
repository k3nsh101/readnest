namespace ReadNest.Dtos;

public record CreateHabitDto(
    HabitTypes HabitType,
    int HabitValue
);
