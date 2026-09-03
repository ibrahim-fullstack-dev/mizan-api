using Microsoft.AspNetCore.Mvc;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Platform.Storage.UpdateStorageUsage;

namespace Mizan.Api.Controllers;

[ApiController]
[Route("api/storage")]
public sealed class StorageController : ControllerBase
{
    private readonly ICommandExecutor _commandExecutor;

    public StorageController(
        ICommandExecutor commandExecutor)
    {
        _commandExecutor = commandExecutor;
    }

    [HttpPut("{tenantId:int}/usage")]
    public async Task<IActionResult> UpdateUsage(
        int tenantId,
        UpdateStorageUsageCommand command,
        CancellationToken cancellationToken)
    {
        if (tenantId != command.TenantId)
            return BadRequest(
                "Route tenant ID does not match command tenant ID.");

        await _commandExecutor.ExecuteAsync<
            UpdateStorageUsageCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }
}