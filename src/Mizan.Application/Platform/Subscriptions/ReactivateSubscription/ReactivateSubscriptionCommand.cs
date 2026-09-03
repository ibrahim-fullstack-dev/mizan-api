using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Subscriptions.ReactivateSubscription;

public sealed record ReactivateSubscriptionCommand(
    int SubscriptionId): ICommand<Unit>;