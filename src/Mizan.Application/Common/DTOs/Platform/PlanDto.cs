namespace Mizan.Application.Common.DTOs.Platform;

public sealed record PlanDto(
    int Id,
    string Name,
    string? Description,
    long StorageLimitBytes,
    decimal MonthlyPrice,
    decimal YearlyPrice,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);