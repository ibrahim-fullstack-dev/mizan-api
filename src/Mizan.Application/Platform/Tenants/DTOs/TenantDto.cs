namespace Mizan.Application.Platform.Tenants.DTOs;

public sealed record TenantDto(
    int Id,
    string Name,
    string SubDomain,
    string SchemaName,
    string Status);