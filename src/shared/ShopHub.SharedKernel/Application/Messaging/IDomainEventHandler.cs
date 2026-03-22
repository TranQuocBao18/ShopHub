using MediatR;
using ShopHub.SharedKernel.Domain.Events;

namespace ShopHub.SharedKernel.Application.Messaging;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}