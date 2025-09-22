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
            .NotEmpty().WithMessage("Habit Value is required");
    }
}

public class UpdateHabitDtoValidator : AbstractValidator<UpdateHabitDto>
{
    public UpdateHabitDtoValidator()
    {
        RuleFor(habit => habit.HabitValue)
            .NotEmpty().WithMessage("Habit Value is required");
    }
}
