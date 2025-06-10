namespace Testerke.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for an aggregate root in the domain model.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    ///     Returns a copy of all registered domain events for this aggregate root and clears the list of events.
    /// </summary>
    /// <returns>A read-only list of the domain events that were recorded.</returns>
    /// <remarks>
    ///     This method has the side effect of clearing the internal list of domain events after they are returned.
    ///     This ensures that events are processed only once.
    /// </remarks>
    IReadOnlyList<IDomainEvent> PopDomainEvents();
}