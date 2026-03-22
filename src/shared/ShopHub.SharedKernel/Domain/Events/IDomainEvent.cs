using MediatR;

namespace ShopHub.SharedKernel.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}