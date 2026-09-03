// src/Mizan.Application/Common/Abstractions/Messaging/ICommandHandler.cs
namespace Mizan.Application.Common.Abstractions.Messaging;

/// <summary>
/// Defines a command handler.
/// </summary>
/// <typeparam name="TCommand"> The type of the command. </typeparam>
/// <typeparam name="TResult"> The type of the result. </typeparam>
public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(
        TCommand command,
        CancellationToken cancellationToken = default);
}
