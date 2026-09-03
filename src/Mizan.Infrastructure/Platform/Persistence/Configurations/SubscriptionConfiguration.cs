using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Platform.Subscriptions;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Infrastructure.Platform.Persistence.Configurations;

public sealed class SubscriptionConfiguration
    : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions", "platform");

        builder.HasKey(subscription => subscription.Id);

        builder.Property(subscription => subscription.Id)
            .ValueGeneratedOnAdd();

        builder.Property(subscription => subscription.TenantId)
            .IsRequired();

        builder.Property(subscription => subscription.PlanId)
            .IsRequired();

        builder.Property(subscription => subscription.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(subscription => subscription.BillingCycle)
            .IsRequired()
            .HasConversion<int>();

        // StorageSize → BIGINT
        builder.Property(subscription => subscription.StorageLimit)
            .HasConversion(
                storageSize => storageSize.Bytes,
                bytes => StorageSize.FromBytes(bytes))
            .HasColumnName("storage_limit_bytes")
            .IsRequired();

        // Money → amount + currency
        builder.ComplexProperty(
            subscription => subscription.Price,
            money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("price_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(value => value.Currency)
                    .HasColumnName("price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

        builder.Property(subscription => subscription.StartedAt)
            .IsRequired();

        builder.Property(subscription => subscription.CurrentPeriodStart)
            .IsRequired();

        builder.Property(subscription => subscription.CurrentPeriodEnd)
            .IsRequired();

        builder.Property(subscription => subscription.CanceledAt)
            .IsRequired(false);

        // Tenant → Subscriptions
        builder.HasOne(subscription => subscription.Tenant)
            .WithMany()
            .HasForeignKey(subscription => subscription.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        // Plan → Subscriptions
        builder.HasOne(subscription => subscription.Plan)
            .WithMany()
            .HasForeignKey(subscription => subscription.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(subscription => subscription.TenantId);

        builder.HasIndex(subscription => subscription.PlanId);

        builder.HasIndex(subscription => subscription.Status);
    }
}
