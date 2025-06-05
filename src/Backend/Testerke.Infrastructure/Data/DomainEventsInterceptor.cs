using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Testerke.SharedKernel.Abstractions;

namespace Testerke.Infrastructure.Data;

internal sealed class DomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    private readonly IPublisher _publisher = publisher;

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
            await PublishDomainEventsAsync(eventData.Context, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        var domainEvents = context
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToList();
        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }
}