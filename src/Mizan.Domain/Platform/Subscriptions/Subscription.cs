// src/Mizan.Domain/Platform/Subscriptions/Subscription.cs

using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Platform.Tenants;
using Mizan.Domain.Shared.Exceptions;
using Mizan.Domain.Shared.Primitives;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Domain.Platform.Subscriptions;

public sealed class Subscription : AggregateRoot
{
    public int TenantId { get; private set; }

    public int PlanId { get; private set; }

    public SubscriptionStatus Status { get; private set; }

    public BillingCycle BillingCycle { get; private set; }

    public StorageSize StorageLimit { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public DateTime StartedAt { get; private set; }

    public DateTime CurrentPeriodStart { get; private set; }

    public DateTime CurrentPeriodEnd { get; private set; }

    public DateTime? CanceledAt { get; private set; }

    // Navigation Properties
    public Tenant Tenant { get; private set; } = null!;

    public Plan Plan { get; private set; } = null!;

    // EF Core constructor
    private Subscription()
    {
    }

    private Subscription(
        int tenantId,
        int planId,
        BillingCycle billingCycle,
        Money price,
        StorageSize storageLimit,
        DateTime periodStart,
        DateTime periodEnd)
    {
        TenantId = tenantId;
        PlanId = planId;
        BillingCycle = billingCycle;
        Price = price;
        StorageLimit = storageLimit;
        StartedAt = periodStart;
        CurrentPeriodStart = periodStart;
        CurrentPeriodEnd = periodEnd;
        Status = SubscriptionStatus.Active;
    }

    public static Subscription Create(
        int tenantId,
        int planId,
        BillingCycle billingCycle,
        Money price,
        StorageSize storageLimit,
        DateTime periodStart,
        DateTime periodEnd)
    {
        if (tenantId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(tenantId),
                "Tenant ID must be greater than zero.");

        if (planId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(planId),
                "Plan ID must be greater than zero.");

        ArgumentNullException.ThrowIfNull(price);
        ArgumentNullException.ThrowIfNull(storageLimit);

        if (periodEnd <= periodStart)
            throw new ArgumentException(
                "Period end must be later than period start.",
                nameof(periodEnd));

        return new Subscription(
            tenantId,
            planId,
            billingCycle,
            price,
            storageLimit,
            periodStart,
            periodEnd);
    }

    public void Cancel(DateTime now)
    {
        if (Status != SubscriptionStatus.Active)
            throw new DomainException(
                "Only an active subscription can be canceled.");

        if (CurrentPeriodEnd <= now)
            throw new DomainException(
                "An expired subscription cannot be canceled.");

        Status = SubscriptionStatus.Canceled;
        CanceledAt = now;
    }

    public void Reactivate(DateTime now)
    {
        if (Status != SubscriptionStatus.Canceled)
            throw new DomainException(
                "Only a canceled subscription can be reactivated.");

        if (CurrentPeriodEnd <= now)
            throw new DomainException(
                "An expired subscription cannot be reactivated.");

        Status = SubscriptionStatus.Active;
        CanceledAt = null;
    }

    public void Expire(DateTime now)
    {
        if (Status != SubscriptionStatus.Active &&
            Status != SubscriptionStatus.Canceled)
            throw new DomainException(
                "Only an active or canceled subscription can expire.");

        if (CurrentPeriodEnd > now)
            throw new DomainException(
                "The subscription period has not ended yet.");

        Status = SubscriptionStatus.Expired;
    }

    public void Renew(
        DateTime periodStart,
        DateTime periodEnd,
        Money price,
        StorageSize storageLimit)
    {
        if (Status != SubscriptionStatus.Expired)
            throw new DomainException(
                "Only an expired subscription can be renewed.");

        if (periodEnd <= periodStart)
            throw new ArgumentException(
                "Period end must be later than period start.",
                nameof(periodEnd));

        ArgumentNullException.ThrowIfNull(price);
        ArgumentNullException.ThrowIfNull(storageLimit);

        Status = SubscriptionStatus.Active;

        CurrentPeriodStart = periodStart;
        CurrentPeriodEnd = periodEnd;

        Price = price;
        StorageLimit = storageLimit;

        CanceledAt = null;
    }

}