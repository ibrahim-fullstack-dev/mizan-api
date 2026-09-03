namespace Mizan.Domain.Shared.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message)
        : base(message) // to pass the message to the base class constructor.
    {
    }
}