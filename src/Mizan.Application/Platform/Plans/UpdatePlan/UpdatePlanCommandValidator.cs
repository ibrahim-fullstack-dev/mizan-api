using FluentValidation;

namespace Mizan.Application.Platform.Plans.UpdatePlan;

public sealed class UpdatePlanCommandValidator
    : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanCommandValidator()
    {
        RuleFor(command => command.PlanId)
            .GreaterThan(0);

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