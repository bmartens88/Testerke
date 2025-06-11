namespace Testerke.Common.Application.EventBus;

/// <summary>
///     Provides a base class for integration event handlers.
/// </summary>
/// <typeparam name="TIntegrationEvent">Type of integration events.</typeparam>
public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    /// <inheritdoc />
    public abstract Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task Handle(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TIntegrationEvent)integrationEvent, cancellationToken);
    }
}