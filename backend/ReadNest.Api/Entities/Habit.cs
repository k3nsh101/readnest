public class Habit
{
    public required Guid Id { get; set; }
    public HabitTypes HabitType { get; set; }
    public required int HabitValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
