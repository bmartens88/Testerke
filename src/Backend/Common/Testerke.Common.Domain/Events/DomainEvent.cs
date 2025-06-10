using Testerke.Common.Domain.Abstractions;

namespace Testerke.Common.Domain.Events;

/// <summary>
///     Provides a base class for domain events.
/// </summary>
/// <param name="OccurredOnUtc">The moment in time the domain event occurred.</param>
public abstract record DomainEvent(DateTime OccurredOnUtc) : IDomainEvent
{
    /// <summary>
    ///     Constructs a new instance of the <see cref="DomainEvent" /> class with the default of UTC 'now'.
    /// </summary>
    protected DomainEvent()
        : this(DateTime.UtcNow)
    {
    }
}