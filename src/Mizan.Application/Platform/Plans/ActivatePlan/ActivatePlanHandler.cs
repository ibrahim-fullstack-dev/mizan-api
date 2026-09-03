using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Plans.ActivatePlan;

public sealed class ActivatePlanHandler: ICommandHandler<ActivatePlanCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public ActivatePlanHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        ActivatePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await _context.Plans
            .FirstOrDefaultAsync(
                plan => plan.Id == command.PlanId,
                cancellationToken);

        if (plan is null)
            throw new KeyNotFoundException(
                $"Plan with ID {command.PlanId} was not found.");

        plan.Activate();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
