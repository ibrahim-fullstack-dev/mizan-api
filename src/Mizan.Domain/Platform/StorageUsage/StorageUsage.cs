// src/Mizan.Domain/Platform/Storage/StorageUsage.cs

using Mizan.Domain.Platform.Enums;
using Mizan.Domain.Platform.Tenants;
using Mizan.Domain.Shared.Primitives;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Domain.Platform.Storage;

public sealed class StorageUsage : Entity
{
    public int TenantId { get; private set; }

    public StorageSize Used { get; private set; } = null!;

    public DateTime LastCalculatedAt { get; private set; }

    // Navigation Property
    public Tenant Tenant { get; private set; } = null!;

    // EF Core constructor
    private StorageUsage()
    {
    }

    private StorageUsage(
        int tenantId,
        StorageSize used)
    {
        TenantId = tenantId;
        Used = used;
        LastCalculatedAt = DateTime.UtcNow;
    }

    public static StorageUsage Create(int tenantId)
    {
        if (tenantId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(tenantId),
                "Tenant ID must be greater than zero.");

        return new StorageUsage(
            tenantId,
            StorageSize.FromBytes(0));
    }

    /// <summary>
    /// Replaces the current usage with the actual usage
    /// calculated by the storage provider.
    /// </summary>
    public void UpdateUsage(StorageSize used)
    {
        ArgumentNullException.ThrowIfNull(used);

        Used = used;
        LastCalculatedAt = DateTime.UtcNow;
    }

    public void Increase(StorageSize amount)
    {
        ArgumentNullException.ThrowIfNull(amount);

        Used = StorageSize.FromBytes(
            checked(Used.Bytes + amount.Bytes));
    }

    public void Decrease(StorageSize amount)
    {
        ArgumentNullException.ThrowIfNull(amount);

        if (amount.Bytes > Used.Bytes)
            throw new InvalidOperationException(
                "Storage usage cannot become negative.");

        Used = StorageSize.FromBytes(
            Used.Bytes - amount.Bytes);
    }

    public bool HasReachedLimit(StorageSize storageLimit)
    {
        ArgumentNullException.ThrowIfNull(storageLimit);

        return Used.Bytes >= storageLimit.Bytes;
    }

    public StorageStatus GetStatus(StorageSize storageLimit)
    {
        ArgumentNullException.ThrowIfNull(storageLimit);

        if (Used.Bytes >= storageLimit.Bytes)
            return StorageStatus.Full;

        if (storageLimit.Bytes == 0)
            return StorageStatus.Full;

        var usagePercentage =
            (decimal)Used.Bytes / storageLimit.Bytes * 100;

        if (usagePercentage >= 90)
            return StorageStatus.Warning90;

        if (usagePercentage >= 80)
            return StorageStatus.Warning80;

        return StorageStatus.Normal;
    }
}
