using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Plans;

namespace Mizan.Application.Platform.Plans.GetPlan;

public sealed class GetPlanHandler
{
    private readonly IPlatformDbContext _context;

    public GetPlanHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Plan?> Handle(
        GetPlanQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _context.Plans
            .AsNoTracking()
            .FirstOrDefaultAsync(
                plan => plan.Id == query.PlanId,
                cancellationToken);
    }
}
