using FluentValidation;

namespace Mizan.Application.Platform.Subscriptions.CreateSubscription;

public sealed class CreateSubscriptionCommandValidator
    : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(command => command.TenantId)
            .GreaterThan(0);

        RuleFor(command => command.PlanId)
            .GreaterThan(0);

        RuleFor(command => command.PeriodStart)
            .NotEmpty();

        RuleFor(command => command.PeriodEnd)
            .NotEmpty()
            .GreaterThan(command => command.PeriodStart);

        RuleFor(command => command.BillingCycle)
            .IsInEnum();
    }
}