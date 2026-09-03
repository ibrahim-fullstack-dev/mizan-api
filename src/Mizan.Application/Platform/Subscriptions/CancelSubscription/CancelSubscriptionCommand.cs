using Mizan.Application.Common.Abstractions.Messaging;
namespace Mizan.Application.Platform.Subscriptions.CancelSubscription;

public sealed record CancelSubscriptionCommand(
    int SubscriptionId): ICommand<Unit>;