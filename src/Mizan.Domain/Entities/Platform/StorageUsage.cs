namespace Mizan.Domain.Entities.Platform;

public class StorageUsage
{
    public int Id { get; private set; }

    // Foreign Key
    public int TenantId { get; private set; }

    public long UsedBytes { get; private set; }

    public DateTime LastCalculatedAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    // Navigation Property
    public Tenant Tenant { get; private set; } = null!;
}