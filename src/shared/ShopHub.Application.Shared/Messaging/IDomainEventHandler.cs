using MediatR;
using ShopHub.Domain.Shared.Events;

namespace ShopHub.Application.Shared.Messaging;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}