using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Platform.Subscriptions;
using Mizan.Domain.Platform.Tenants;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Subscriptions.CreateSubscription;

public sealed class CreateSubscriptionHandler : ICommandHandler<CreateSubscriptionCommand, int>
{
    private readonly IPlatformDbContext _context;

    public CreateSubscriptionHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(
                tenant => tenant.Id == command.TenantId,
                cancellationToken);

        if (tenant is null)
            throw new KeyNotFoundException(
                $"Tenant with ID {command.TenantId} was not found.");

        if (tenant.Status != TenantStatus.Active)
            throw new InvalidOperationException(
                "Only an active tenant can create a subscription.");

        var plan = await _context.Plans
            .FirstOrDefaultAsync(
                plan => plan.Id == command.PlanId,
                cancellationToken);

        if (plan is null)
            throw new KeyNotFoundException(
                $"Plan with ID {command.PlanId} was not found.");

        if (!plan.Status.Equals(PlanStatus.Active))
            throw new InvalidOperationException(
                "Cannot subscribe to an inactive plan.");

        var now = DateTime.UtcNow;

        var hasCurrentSubscription =
     await _context.Subscriptions.AnyAsync(
        subscription =>
            subscription.TenantId == command.TenantId &&
            subscription.CurrentPeriodEnd > now &&
            (subscription.Status == SubscriptionStatus.Active ||
             subscription.Status == SubscriptionStatus.Canceled),
        cancellationToken);

        if (hasCurrentSubscription)
            throw new InvalidOperationException(
                "The tenant already has a current subscription.");

        var price = command.BillingCycle == BillingCycle.Monthly
            ? plan.MonthlyPrice
            : plan.YearlyPrice;

        var subscription = Subscription.Create(
            command.TenantId,
            command.PlanId,
            command.BillingCycle,
            price,
            plan.StorageLimit,
            command.PeriodStart,
            command.PeriodEnd);

        _context.Subscriptions.Add(subscription);

        await _context.SaveChangesAsync(cancellationToken);

        return subscription.Id;
    }
}
