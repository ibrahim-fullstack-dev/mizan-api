// src/Mizan.Application/Common/Abstractions/Messaging/ICommandExecutor.cs
namespace Mizan.Application.Common.Abstractions.Messaging;

/// <summary>
/// Defines a command executor.
/// </summary>
public interface ICommandExecutor
{
    Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>;
}
