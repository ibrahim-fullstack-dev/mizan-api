// src/Mizan.Infrastructure/Persistence/Configurations/TenantConfiguration.cs

using Microsoft.EntityFrameworkCore; // EF Core
using Microsoft.EntityFrameworkCore.Metadata.Builders; // EF Builders Configuration.
using Mizan.Domain.Platform.Tenants;

namespace Mizan.Infrastructure.Platform.Persistence.Configurations;

public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants", "platform");

        builder.HasKey(tenant => tenant.Id);

        builder.Property(tenant => tenant.Id)
            .ValueGeneratedOnAdd();

        builder.Property(tenant => tenant.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(tenant => tenant.SubDomain)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(tenant => tenant.SchemaName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(tenant => tenant.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(tenant => tenant.SubDomain)
            .IsUnique();

        builder.HasIndex(tenant => tenant.SchemaName)
            .IsUnique();
    }
}
