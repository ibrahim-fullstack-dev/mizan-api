namespace Mizan.Application.Common.DTOs.Platform;

public sealed record StorageUsageDto(
    int TenantId,
    long UsedBytes,
    long StorageLimitBytes,
    decimal UsagePercentage,
    string Status,
    DateTime LastCalculatedAt);