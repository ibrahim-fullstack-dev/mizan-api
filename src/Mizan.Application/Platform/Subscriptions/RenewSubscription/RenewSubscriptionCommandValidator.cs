using FluentValidation;

namespace Mizan.Application.Platform.Subscriptions.RenewSubscription;

public sealed class RenewSubscriptionCommandValidator
    : AbstractValidator<RenewSubscriptionCommand>
{
    public RenewSubscriptionCommandValidator()
    {
        RuleFor(command => command.SubscriptionId)
            .GreaterThan(0);

        RuleFor(command => command.PeriodStart)
            .NotEmpty();

        RuleFor(command => command.PeriodEnd)
            .NotEmpty()
            .GreaterThan(command => command.PeriodStart);
    }
}