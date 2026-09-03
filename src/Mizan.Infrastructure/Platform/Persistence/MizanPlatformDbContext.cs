// src/Mizan.Infrastructure/Persistence/Platform/MizanPlatformDbContext.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Platform.Storage;
using Mizan.Domain.Platform.Subscriptions;
using Mizan.Domain.Platform.Tenants;

namespace Mizan.Infrastructure.Platform.Persistence;

public sealed class MizanPlatformDbContext
    : DbContext, IPlatformDbContext
{
    private IDbContextTransaction? _transaction;

    public MizanPlatformDbContext(
        DbContextOptions<MizanPlatformDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<Plan> Plans => Set<Plan>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<StorageUsage> StorageUsages => Set<StorageUsage>();

    public async Task BeginTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException(
                "A database transaction is already in progress.");

        _transaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "No database transaction is in progress.");

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            return;

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task ExecuteSqlAsync(
        string sql,
        CancellationToken cancellationToken = default)
    {
        await Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MizanPlatformDbContext).Assembly);
    }
}
