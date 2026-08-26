using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.Platform;

namespace Mizan.Infrastructure.Persistence.Configurations.Platform;

public class StorageUsageConfiguration : IEntityTypeConfiguration<StorageUsage>
{
    public void Configure(EntityTypeBuilder<StorageUsage> builder)
    {
        builder.ToTable("StorageUsages", "platform");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UsedBytes)
            .IsRequired();

        builder.Property(x => x.LastCalculatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Tenant)
            .WithOne(x => x.StorageUsage)
            .HasForeignKey<StorageUsage>(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}