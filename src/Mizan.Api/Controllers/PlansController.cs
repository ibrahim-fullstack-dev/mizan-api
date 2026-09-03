using Microsoft.AspNetCore.Mvc;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Platform.Plans.ActivatePlan;
using Mizan.Application.Platform.Plans.CreatePlan;
using Mizan.Application.Platform.Plans.DeactivatePlan;
using Mizan.Application.Platform.Plans.UpdatePlan;

namespace Mizan.Api.Controllers;

[ApiController]
[Route("api/plans")]
public sealed class PlansController : ControllerBase
{
    private readonly ICommandExecutor _commandExecutor;

    public PlansController(ICommandExecutor commandExecutor)
    {
        _commandExecutor = commandExecutor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePlanCommand command,
        CancellationToken cancellationToken)
    {
        var planId = await _commandExecutor.ExecuteAsync<
            CreatePlanCommand,
            int>(
            command,
            cancellationToken);

        return Ok(planId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePlanCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PlanId)
            return BadRequest(
                "Route ID does not match command ID.");

        await _commandExecutor.ExecuteAsync<
            UpdatePlanCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/activate")]
    public async Task<IActionResult> Activate(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new ActivatePlanCommand(id);

        await _commandExecutor.ExecuteAsync<
            ActivatePlanCommand,
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
        var command = new DeactivatePlanCommand(id);

        await _commandExecutor.ExecuteAsync<
            DeactivatePlanCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }
}