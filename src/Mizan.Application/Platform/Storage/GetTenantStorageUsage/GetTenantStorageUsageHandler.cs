using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Storage;

namespace Mizan.Application.Platform.Storage.GetTenantStorageUsage;

public sealed class GetTenantStorageUsageHandler
{
    private readonly IPlatformDbContext _context;

    public GetTenantStorageUsageHandler(
        IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<StorageUsage> Handle(
        GetTenantStorageUsageQuery query,
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

        return storageUsage;
    }
}
