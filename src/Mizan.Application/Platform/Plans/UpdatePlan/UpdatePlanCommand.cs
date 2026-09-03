using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Plans.UpdatePlan;

public sealed record UpdatePlanCommand(
    int PlanId,
    string Name,
    string? Description,
    long StorageLimitBytes,
    decimal MonthlyPrice,
    string MonthlyPriceCurrency,
    decimal YearlyPrice,
    string YearlyPriceCurrency
) : ICommand<Unit>;