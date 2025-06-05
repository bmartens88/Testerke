namespace Testerke.SharedKernel.Abstractions;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> PopDomainEvents();
}