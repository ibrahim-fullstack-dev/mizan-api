using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Plans.CreatePlan;

public sealed record CreatePlanCommand(
    string Name,
    string? Description,
    long StorageLimitBytes,
    decimal MonthlyPrice,
    string MonthlyPriceCurrency,
    decimal YearlyPrice,
    string YearlyPriceCurrency
) : ICommand<int>;