using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Plans;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Application.Platform.Plans.CreatePlan;

public sealed class CreatePlanHandler : ICommandHandler<CreatePlanCommand, int>
{
    private readonly IPlatformDbContext _context;

    public CreatePlanHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreatePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var storageLimit = StorageSize.FromBytes(command.StorageLimitBytes);

        var monthlyPrice = Money.Create(
            command.MonthlyPrice,
            command.MonthlyPriceCurrency);

        var yearlyPrice = Money.Create(
            command.YearlyPrice,
            command.YearlyPriceCurrency);

        var plan = Plan.Create(
            command.Name,
            command.Description,
            storageLimit,
            monthlyPrice,
            yearlyPrice);

        _context.Plans.Add(plan);

        await _context.SaveChangesAsync(cancellationToken);

        return plan.Id;
    }
}
