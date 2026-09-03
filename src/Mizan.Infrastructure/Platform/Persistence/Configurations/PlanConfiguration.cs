using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Infrastructure.Platform.Persistence.Configurations;

public sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("plans", "platform");

        builder.HasKey(plan => plan.Id);

        builder.Property(plan => plan.Id)
            .ValueGeneratedOnAdd();

        builder.Property(plan => plan.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(plan => plan.Description)
            .HasMaxLength(500);

        // StorageSize → BIGINT
        builder.Property(plan => plan.StorageLimit)
            .HasConversion(
                storageSize => storageSize.Bytes,
                bytes => StorageSize.FromBytes(bytes))
            .HasColumnName("storage_limit_bytes")
            .IsRequired();

        // Money → amount + currency
        builder.ComplexProperty(
            plan => plan.MonthlyPrice,
            money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("monthly_price_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(value => value.Currency)
                    .HasColumnName("monthly_price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

        builder.ComplexProperty(
            plan => plan.YearlyPrice,
            money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("yearly_price_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(value => value.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("yearly_price_currency")
                    .IsRequired();
            });

        builder.Property(plan => plan.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(plan => plan.Name)
            .IsUnique();
    }
}
