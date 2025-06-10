using Testerke.Common.Domain.Abstractions;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Provides a base class for handling domain events.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain events to handle.</typeparam>
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <inheritdoc />
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}