using Mizan.Domain.Enums;

namespace Mizan.Domain.Entities.Platform;

public class Tenant
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string SubDomain { get; private set; } = null!;

    public string SchemaName { get; private set; } = null!;

    public TenantStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    public StorageUsage? StorageUsage { get; private set; }

    public ICollection<Subscription> Subscriptions { get; private set; }
        = new List<Subscription>();
}