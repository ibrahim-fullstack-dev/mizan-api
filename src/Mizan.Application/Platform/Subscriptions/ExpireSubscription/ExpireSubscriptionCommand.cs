using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Subscriptions.ExpireSubscription;


public sealed record ExpireSubscriptionCommand(
    int SubscriptionId): ICommand<Unit>;