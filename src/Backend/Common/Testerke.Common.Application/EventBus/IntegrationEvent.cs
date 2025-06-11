namespace Testerke.Common.Application.EventBus;

/// <summary>
///     Provides a base class for integration events.
/// </summary>
/// <param name="Id">The unique identifier of the integration event.</param>
/// <param name="OccurredOnUtc">The moment in time the event occurred, in UTC.</param>
public abstract record IntegrationEvent(Guid Id, DateTime OccurredOnUtc) : IIntegrationEvent;