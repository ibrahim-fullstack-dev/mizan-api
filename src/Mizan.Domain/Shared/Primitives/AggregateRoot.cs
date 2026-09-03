// src/Shared/Primitives/AggregateRoot.cs

using Mizan.Domain.Shared.Events;

namespace Mizan.Domain.Shared.Primitives;

/// <summary>
/// Base class for all aggregate roots.
/// </summary>
public abstract class AggregateRoot : Entity
{
    // Static reference to the domain events list.
    private readonly List<IDomainEvent> _domainEvents = [];

    // Readonly collection of domain events.
    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}