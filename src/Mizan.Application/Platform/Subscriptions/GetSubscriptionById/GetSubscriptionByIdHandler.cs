using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Platform.Subscriptions;

namespace Mizan.Application.Platform.Subscriptions.GetSubscriptionById;

public sealed class GetSubscriptionByIdHandler
{
    private readonly IPlatformDbContext _context;

    public GetSubscriptionByIdHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionDto?> Handle(
        GetSubscriptionByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .AsNoTracking()
            .Where(subscription => subscription.Id == query.Id)
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
                subscription.CanceledAt
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}