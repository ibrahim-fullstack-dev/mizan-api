// src/Mizan.Application/Platform/Tenants/CreateTenant/CreateTenantHandler.cs

using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Tenants;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.CreateTenant;

public sealed class CreateTenantHandler: ICommandHandler<CreateTenantCommand,int>
{
    private readonly IPlatformDbContext _context;

    public CreateTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateTenantCommand command,
        CancellationToken cancellationToken = default)
    {
        await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            var tenant = Tenant.Create(
                command.Name,
                command.SubDomain);

            _context.Tenants.Add(tenant);

            await _context.SaveChangesAsync(cancellationToken);

            tenant.AssignSchemaName();

            await _context.ExecuteSqlAsync(
                $"CREATE SCHEMA \"{tenant.SchemaName}\"",
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _context.CommitTransactionAsync(cancellationToken);

            return tenant.Id;
        }
        catch
        {
            await _context.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }
}
