using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.Platform;

namespace Mizan.Infrastructure.Persistence.Configurations.Platform;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans", "platform");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.StorageLimitBytes)
            .IsRequired();

        builder.Property(x => x.MonthlyPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.YearlyPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}