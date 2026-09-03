namespace Mizan.Application.Platform.Subscriptions;

public sealed record SubscriptionDto(
    int Id,
    int TenantId,
    int PlanId,
    string Status,
    string BillingCycle,
    long StorageLimitBytes,
    decimal Price,
    string PriceCurrency,
    DateTime StartedAt,
    DateTime CurrentPeriodStart,
    DateTime CurrentPeriodEnd,
    DateTime? CanceledAt
);