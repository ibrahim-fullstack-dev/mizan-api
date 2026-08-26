using Mizan.Domain.Enums;

namespace Mizan.Domain.Entities.Platform;

public class Subscription
{
    public int Id { get; private set; }

    // Foreign Keys
    public int TenantId { get; private set; }

    public int PlanId { get; private set; }

    public SubscriptionStatus Status { get; private set; }

    public BillingCycle BillingCycle { get; private set; }

    public DateTime StartedAt { get; private set; }

    public DateTime? CurrentPeriodStart { get; private set; }

    public DateTime? CurrentPeriodEnd { get; private set; }

    public DateTime? CanceledAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    public Tenant Tenant { get; private set; } = null!;

    public Plan Plan { get; private set; } = null!;
}