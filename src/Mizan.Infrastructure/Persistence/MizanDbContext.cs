// src/Mizan.Infrastructure/Persistence/MizanDbContext.cs
using Microsoft.EntityFrameworkCore;
using Mizan.Domain.Entities.Platform;

namespace Mizan.Infrastructure.Persistence;

public class MizanDbContext : DbContext
{
    public MizanDbContext(DbContextOptions<MizanDbContext> options)
        : base(options)
    {
    }

    // Platform tables
    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<Plan> Plans => Set<Plan>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<StorageUsage> StorageUsages => Set<StorageUsage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MizanDbContext).Assembly);
    }
}