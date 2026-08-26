using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mizan.Domain.Entities.Platform;

namespace Mizan.Infrastructure.Persistence.Configurations.Platform;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions", "platform");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.BillingCycle)
            .IsRequired();

        builder.Property(x => x.StartedAt)
            .IsRequired();

        builder.Property(x => x.CurrentPeriodStart)
            .IsRequired(false);

        builder.Property(x => x.CurrentPeriodEnd)
            .IsRequired(false);

        builder.Property(x => x.CanceledAt)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Plan)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}