using Microsoft.EntityFrameworkCore;
using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Platform.Storage;
using Mizan.Domain.Platform.Subscriptions;
using Mizan.Domain.Platform.Tenants;

namespace Mizan.Application.Common.Interfaces;

public interface IPlatformDbContext
{
    DbSet<Tenant> Tenants { get; }

    DbSet<Plan> Plans { get; }

    DbSet<Subscription> Subscriptions { get; }

    DbSet<StorageUsage> StorageUsages { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(
        CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(
        CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default);

    Task ExecuteSqlAsync(
        string sql,
        CancellationToken cancellationToken = default);
}
