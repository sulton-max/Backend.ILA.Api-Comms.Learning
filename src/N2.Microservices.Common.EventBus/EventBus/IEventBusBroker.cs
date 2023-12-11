using N2.Microservices.Common.Core.Models.Events;

namespace N2.Microservices.EventBus.EventBus;

public interface IEventBusBroker
{
    ValueTask PublishLocallyAsync<TEvent>(TEvent command) where TEvent : IEvent;

    ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

    ValueTask SubscribeAsync<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
}