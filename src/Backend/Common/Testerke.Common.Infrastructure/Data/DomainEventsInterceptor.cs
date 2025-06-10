using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Testerke.Common.Domain.Abstractions;

namespace Testerke.Common.Infrastructure.Data;

/// <summary>
///     EF Core interceptor that publishes domain events after saving changes to the database.
/// </summary>
/// <param name="publisher"><see cref="IPublisher" /> instance for publishing domain events.</param>
public sealed class DomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    private readonly IPublisher _publisher = publisher;

    /// <inheritdoc />
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
            await PublishDomainEventsAsync(eventData.Context, cancellationToken);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    ///     Gets the domain events from the <see cref="DbContext" /> and publishes them using the provided
    ///     <see cref="IPublisher" />.
    /// </summary>
    /// <param name="dbContext"><see cref="DbContext" /> which is performing a save operation.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    private async Task PublishDomainEventsAsync(DbContext dbContext, CancellationToken cancellationToken = default)
    {
        var domainEvents = dbContext
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToArray();

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }
}