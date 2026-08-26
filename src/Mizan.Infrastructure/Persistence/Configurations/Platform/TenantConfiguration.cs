using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.Platform;

namespace Mizan.Infrastructure.Persistence.Configurations.Platform;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants", "platform");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.SubDomain)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.SubDomain)
            .IsUnique();

        builder.Property(x => x.SchemaName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.SchemaName)
            .IsUnique();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}