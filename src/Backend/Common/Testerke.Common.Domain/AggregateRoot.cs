using Testerke.Common.Domain.Abstractions;

namespace Testerke.Common.Domain;

/// <summary>
///     Provides a base class for aggregate roots that implements the <see cref="IAggregateRoot" /> interface.
/// </summary>
/// <typeparam name="TId">The type of the strongly typed identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : TypedId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///     Constructs an instance of <see cref="AggregateRoot{TId}" /> with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier for this aggregate root.</param>
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    /// <summary>
    ///     Constructs a new instance of the <see cref="AggregateRoot{TId}" /> class.
    /// </summary>
    protected AggregateRoot()
    {
    }

    /// <inheritdoc />
    public IReadOnlyList<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    /// <summary>
    ///     Registers a domain event to be raised later.
    /// </summary>
    /// <param name="domainEvent"><see cref="IDomainEvent" /> instance containing event data.</param>
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}