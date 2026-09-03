using Microsoft.AspNetCore.Mvc;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Platform.Tenants.CreateTenant;
using Mizan.Application.Platform.Tenants.DeactivateTenant;
using Mizan.Application.Platform.Tenants.ReactivateTenant;
using Mizan.Application.Platform.Tenants.SuspendTenant;
using Mizan.Application.Platform.Tenants.UpdateTenant;

namespace Mizan.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public sealed class TenantsController : ControllerBase
{
    private readonly ICommandExecutor _commandExecutor;

    public TenantsController(ICommandExecutor commandExecutor)
    {
        _commandExecutor = commandExecutor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTenantCommand command,
        CancellationToken cancellationToken)
    {
        var tenantId = await _commandExecutor.ExecuteAsync<
            CreateTenantCommand,
            int>(
            command,
            cancellationToken);

        return Ok(tenantId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateTenantCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(
                "Route ID does not match command ID.");

        await _commandExecutor.ExecuteAsync<
            UpdateTenantCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/suspend")]
    public async Task<IActionResult> Suspend(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new SuspendTenantCommand(id);

        await _commandExecutor.ExecuteAsync<
            SuspendTenantCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/reactivate")]
    public async Task<IActionResult> Reactivate(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new ReactivateTenantCommand(id);

        await _commandExecutor.ExecuteAsync<
            ReactivateTenantCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/deactivate")]
    public async Task<IActionResult> Deactivate(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeactivateTenantCommand(id);

        await _commandExecutor.ExecuteAsync<
            DeactivateTenantCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }
}