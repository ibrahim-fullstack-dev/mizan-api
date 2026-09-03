using Microsoft.EntityFrameworkCore;

using Mizan.Application.Common.Interfaces;

using Mizan.Application.Platform.Subscriptions;

namespace Mizan.Application.Platform.Subscriptions.GetTenantSubscriptions;

public sealed class GetTenantSubscriptionsHandler
{
    private readonly IPlatformDbContext _context;

    public GetTenantSubscriptionsHandler(
        IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<SubscriptionDto>> Handle(
        GetTenantSubscriptionsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .AsNoTracking()
            .Where(subscription =>
                subscription.TenantId == query.TenantId)
            .OrderByDescending(subscription =>
                subscription.StartedAt)
            .Select(subscription => new SubscriptionDto(
                subscription.Id,
                subscription.TenantId,
                subscription.PlanId,
                subscription.Status.ToString(),
                subscription.BillingCycle.ToString(),
                subscription.StorageLimit.Bytes,
                subscription.Price.Amount,
                subscription.Price.Currency,
                subscription.StartedAt,
                subscription.CurrentPeriodStart,
                subscription.CurrentPeriodEnd,
                subscription.CanceledAt))
            .ToListAsync(cancellationToken);
    }
}