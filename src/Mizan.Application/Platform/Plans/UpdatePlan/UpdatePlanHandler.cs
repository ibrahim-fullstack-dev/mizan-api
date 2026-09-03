using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Application.Platform.Plans.UpdatePlan;

public sealed class UpdatePlanHandler : ICommandHandler<UpdatePlanCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public UpdatePlanHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdatePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await _context.Plans
            .FirstOrDefaultAsync(
                plan => plan.Id == command.PlanId,
                cancellationToken);

        if (plan is null)
            throw new KeyNotFoundException(
                $"Plan with ID {command.PlanId} was not found.");

        var storageLimit = StorageSize.FromBytes(command.StorageLimitBytes);

        var monthlyPrice = Money.Create(
            command.MonthlyPrice,
            command.MonthlyPriceCurrency);

        var yearlyPrice = Money.Create(
            command.YearlyPrice,
            command.YearlyPriceCurrency);

        plan.UpdateDetails(
            command.Name,
            command.Description,
            storageLimit,
            monthlyPrice,
            yearlyPrice);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
