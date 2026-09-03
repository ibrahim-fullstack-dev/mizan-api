using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Domain.Shared.ValueObjects;

namespace Mizan.Application.Platform.Storage.UpdateStorageUsage;

public sealed class UpdateStorageUsageHandler : ICommandHandler<UpdateStorageUsageCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public UpdateStorageUsageHandler(
        IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateStorageUsageCommand command,
        CancellationToken cancellationToken = default)
    {
        var storageUsage = await _context.StorageUsages
            .FirstOrDefaultAsync(
                storage => storage.TenantId == command.TenantId,
                cancellationToken);

        if (storageUsage is null)
            throw new KeyNotFoundException(
                $"Storage usage for tenant with ID {command.TenantId} was not found.");

        var used = StorageSize.FromBytes(command.UsedBytes);

        storageUsage.UpdateUsage(used);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
