using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Enums;
using Mizan.Domain.Platform.Subscriptions;

namespace Mizan.Application.Platform.Storage.GetTenantStorageStatus;

public sealed class GetTenantStorageStatusHandler
{
    private readonly IPlatformDbContext _context;

    public GetTenantStorageStatusHandler(
        IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<StorageStatus> Handle(
        GetTenantStorageStatusQuery query,
        CancellationToken cancellationToken = default)
    {
        var storageUsage = await _context.StorageUsages
            .AsNoTracking()
            .FirstOrDefaultAsync(
                storage => storage.TenantId == query.TenantId,
                cancellationToken);

        if (storageUsage is null)
            throw new KeyNotFoundException(
                $"Storage usage for tenant with ID {query.TenantId} was not found.");

        var subscription = await _context.Subscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(
                subscription =>
                    subscription.TenantId == query.TenantId &&
                    subscription.Status == SubscriptionStatus.Active,
                cancellationToken);

        if (subscription is null)
            throw new InvalidOperationException(
                "The tenant does not have an active subscription.");

        return storageUsage.GetStatus(
            subscription.StorageLimit);
    }
}
