// src/Shared/Primitives/Entity.cs

namespace Mizan.Domain.Shared.Primitives;

/// <summary>
/// Base class for all domain entities.
/// </summary>
public abstract class Entity
{
    public int Id { get; private set; }

}