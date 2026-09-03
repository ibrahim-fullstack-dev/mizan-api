using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Subscriptions.RenewSubscription;

public sealed record RenewSubscriptionCommand(
    int SubscriptionId,
    DateTime PeriodStart,
    DateTime PeriodEnd)
    : ICommand<Unit>;