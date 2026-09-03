using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Platform.Storage;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Infrastructure.Platform.Persistence.Configurations;

public sealed class StorageUsageConfiguration
    : IEntityTypeConfiguration<StorageUsage>
{
    public void Configure(EntityTypeBuilder<StorageUsage> builder)
    {
        builder.ToTable("storage_usage", "platform");

        builder.HasKey(storageUsage => storageUsage.Id);

        builder.Property(storageUsage => storageUsage.Id)
            .ValueGeneratedOnAdd();

        builder.Property(storageUsage => storageUsage.TenantId)
            .IsRequired();

        // StorageSize → BIGINT
        builder.Property(storageUsage => storageUsage.Used)
            .HasConversion(
                storageSize => storageSize.Bytes,
                bytes => StorageSize.FromBytes(bytes))
            .HasColumnName("used_bytes")
            .IsRequired();

        builder.Property(storageUsage => storageUsage.LastCalculatedAt)
            .IsRequired();

        // Tenant 1 : 1 StorageUsage
        builder.HasOne(storageUsage => storageUsage.Tenant)
            .WithOne()
            .HasForeignKey<StorageUsage>(
                storageUsage => storageUsage.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Enforce 1 : 1 at database level
        builder.HasIndex(storageUsage => storageUsage.TenantId)
            .IsUnique();
    }
}
