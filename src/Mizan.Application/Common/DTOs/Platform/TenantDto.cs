namespace Mizan.Application.Common.DTOs.Platform;

public sealed record TenantDto(
    int Id,
    string Name,
    string SubDomain,
    string SchemaName,
    string Status);