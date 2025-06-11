namespace Testerke.Common.Application.EventBus;

/// <summary>
///     Defines the contract for an event bus.
/// </summary>
public interface IEventBus
{
    /// <summary>
    ///     Asynchronously publishes an integration event to the event bus.
    /// </summary>
    /// <param name="integrationEvent">The integration event to publish.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The operation will be canceled when the
    ///     token is canceled.
    /// </param>
    /// <typeparam name="T">The type of integration event.</typeparam>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}