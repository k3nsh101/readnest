using FluentValidation;
using ReadNest.Dtos;

namespace ReadNest.Validators;

public class CreateHabitDtoValidator : AbstractValidator<CreateHabitDto>
{
    public CreateHabitDtoValidator()
    {
        RuleFor(habit => habit.HabitType)
            .IsInEnum().WithMessage("Invalid Habit Type");

        RuleFor(habit => habit.HabitValue)
            .NotEmpty();
    }
}

public class UpdateHabitDtoValidator : AbstractValidator<CreateHabitDto>
{
    public UpdateHabitDtoValidator()
    {
        RuleFor(habit => habit.HabitType)
            .IsInEnum();

        RuleFor(habit => habit.HabitValue)
            .NotEmpty();
    }
}
