namespace Mizan.Application.Common.Abstractions.Messaging.Commands;

public readonly record struct Unit
{
    public static readonly Unit Value = new();
}
