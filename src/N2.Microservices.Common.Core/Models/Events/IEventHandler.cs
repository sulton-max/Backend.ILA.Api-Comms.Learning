using MediatR;

namespace N2.Microservices.Common.Core.Models.Events;

public interface IEventHandler<in TEvent> : IEventHandler, INotificationHandler<TEvent> where TEvent : IEvent
{
}

public interface IEventHandler
{
}