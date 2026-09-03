using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Domain.Platform.Plans;

namespace Mizan.Application.Platform.Subscriptions.RenewSubscription;

public sealed class RenewSubscriptionHandler
    : ICommandHandler<RenewSubscriptionCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public RenewSubscriptionHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        RenewSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var subscription = await _context.Subscriptions
            .FirstOrDefaultAsync(
                subscription => subscription.Id == command.SubscriptionId,
                cancellationToken);

        if (subscription is null)
            throw new KeyNotFoundException(
                $"Subscription with ID {command.SubscriptionId} was not found.");

        var plan = await _context.Plans
            .FirstOrDefaultAsync(
                plan => plan.Id == subscription.PlanId,
                cancellationToken);

        if (plan is null)
            throw new KeyNotFoundException(
                $"Plan with ID {subscription.PlanId} was not found.");

        if (plan.Status != PlanStatus.Active)
            throw new InvalidOperationException(
                "Cannot renew a subscription with an inactive plan.");

        var price = subscription.BillingCycle == BillingCycle.Monthly
            ? plan.MonthlyPrice
            : plan.YearlyPrice;

        subscription.Renew(
            command.PeriodStart,
            command.PeriodEnd,
            price,
            plan.StorageLimit);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}