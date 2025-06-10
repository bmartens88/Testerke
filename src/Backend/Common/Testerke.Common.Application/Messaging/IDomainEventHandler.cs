using Testerke.Common.Domain.Abstractions;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Defines a contract for handling domain events.
/// </summary>
public interface IDomainEventHandler
{
    /// <summary>
    ///     Asynchronously handles the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> that represents the asynchronous handling operation.</returns>
    Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
///     Defines a contract for handling domain events.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain events.</typeparam>
public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    ///     Asynchronously handles the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> that represents the asynchronous handling operation.</returns>
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}