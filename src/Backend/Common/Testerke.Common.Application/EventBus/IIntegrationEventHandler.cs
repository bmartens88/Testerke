namespace Testerke.Common.Application.EventBus;

/// <summary>
///     Defines the contract for handling integration events.
/// </summary>
/// <typeparam name="TIntegrationEvent">The type of the integration event.</typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    /// <summary>
    ///     Asynchronously handles an integration event of type <typeparamref name="TIntegrationEvent" />.
    /// </summary>
    /// <param name="integrationEvent"><typeparamref name="TIntegrationEvent" /> instance to handle.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The operation will be canceled when the
    ///     token is canceled.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}

/// <summary>
///     Defines a contract for handling integration events.
/// </summary>
public interface IIntegrationEventHandler
{
    /// <summary>
    ///     Asynchronously handles an integration event.
    /// </summary>
    /// <param name="integrationEvent"><see cref="IIntegrationEvent" /> instance to handle.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The operation will be canceled when the
    ///     token is canceled.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Handle(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}