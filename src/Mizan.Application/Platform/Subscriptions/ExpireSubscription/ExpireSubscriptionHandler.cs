using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Subscriptions.ExpireSubscription;

public sealed class ExpireSubscriptionHandler: ICommandHandler<ExpireSubscriptionCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public ExpireSubscriptionHandler(
        IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        ExpireSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var subscription = await _context.Subscriptions
            .FirstOrDefaultAsync(
                subscription =>
                    subscription.Id == command.SubscriptionId,
                cancellationToken);

        if (subscription is null)
            throw new KeyNotFoundException(
                $"Subscription with ID {command.SubscriptionId} was not found.");

        subscription.Expire(DateTime.UtcNow);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
