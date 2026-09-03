using Mizan.Application.Common.Abstractions.Messaging;

using Mizan.Domain.Platform.Plans;

namespace Mizan.Application.Platform.Subscriptions.CreateSubscription;

public sealed record CreateSubscriptionCommand(
    int TenantId,
    int PlanId,
    BillingCycle BillingCycle,
    DateTime PeriodStart,
    DateTime PeriodEnd): ICommand<int>;
