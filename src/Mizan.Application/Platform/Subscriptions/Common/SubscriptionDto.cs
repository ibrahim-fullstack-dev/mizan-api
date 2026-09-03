using Mizan.Domain.Platform.Plans;
using Mizan.Domain.Platform.Subscriptions;

namespace Mizan.Application.Platform.Subscriptions.Common;

public sealed record SubscriptionDto(
    int Id,
    int TenantId,
    int PlanId,
    SubscriptionStatus Status,
    BillingCycle BillingCycle,
    long StorageLimitBytes,
    decimal Price,
    DateTime StartedAt,
    DateTime? CurrentPeriodStart,
    DateTime? CurrentPeriodEnd,
    DateTime? CanceledAt
    );
