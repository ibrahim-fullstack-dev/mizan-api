// src/Mizan.Domain/Platform/Tenants/Tenant.cs

using Mizan.Domain.Shared.Primitives;
using Mizan.Domain.Shared.Exceptions;


namespace Mizan.Domain.Platform.Tenants;

public sealed class Tenant : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public string SubDomain { get; private set; } = null!;

    public string? SchemaName { get; private set; }

    public TenantStatus Status { get; private set; }

    // EF Core constructor
    private Tenant()
    {
    }

    private Tenant(
        string name,
        string subDomain)
    {
        Name = name;
        SubDomain = subDomain;
        Status = TenantStatus.Active;
    }

    public static Tenant Create(
        string name,
        string subDomain)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Tenant name is required.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(subDomain))
            throw new ArgumentException(
                "Tenant subdomain is required.",
                nameof(subDomain));

        return new Tenant(
            name.Trim(),
            subDomain.Trim().ToLowerInvariant());
    }

    public void AssignSchemaName()
    {
        if (Id <= 0)
            throw new DomainException(
                "Tenant must be saved before assigning a schema name.");

        SchemaName = $"tenant_{Id}";
    }

    public void UpdateDetails(
        string name,
        string subDomain)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Tenant name is required.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(subDomain))
            throw new ArgumentException(
                "Tenant subdomain is required.",
                nameof(subDomain));

        Name = name.Trim();
        SubDomain = subDomain.Trim().ToLowerInvariant();
    }

    public void Suspend()
    {
        if (Status == TenantStatus.Suspended)
            return;

        if (Status != TenantStatus.Active)
            throw new DomainException(
                "Only an active tenant can be suspended.");

        Status = TenantStatus.Suspended;


    }

    public void Reactivate()
    {
        if (Status == TenantStatus.Active)
            return;

        if (Status != TenantStatus.Suspended &&
            Status != TenantStatus.Inactive)
            throw new DomainException(
                "Only a suspended or inactive tenant can be reactivated.");

        Status = TenantStatus.Active;

    }

    public void Deactivate()
    {
        if (Status == TenantStatus.Inactive)
            return;

        if (Status != TenantStatus.Active &&
            Status != TenantStatus.Suspended)
            throw new DomainException(
                "Only an active or suspended tenant can be deactivated.");

        Status = TenantStatus.Inactive;


    }
}
