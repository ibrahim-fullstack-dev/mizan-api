using FluentValidation;

namespace Mizan.Application.Platform.Plans.CreatePlan;

public sealed class CreatePlanCommandValidator
    : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Description)
            .MaximumLength(500)
            .When(command => command.Description is not null);

        RuleFor(command => command.StorageLimitBytes)
            .GreaterThan(0);

        RuleFor(command => command.MonthlyPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(command => command.YearlyPrice)
            .GreaterThanOrEqualTo(0);
    }
}