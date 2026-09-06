using Mizan.Application.Common.Abstractions.Messaging.Commands;
namespace Mizan.Application.Platform.Subscriptions.CancelSubscription;

public sealed record CancelSubscriptionCommand(
    int SubscriptionId) : ICommand<Unit>;