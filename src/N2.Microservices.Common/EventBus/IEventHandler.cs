namespace N2.Microservices.Common.Bus;

public interface IEventHandler<in TEvent> : IEventHandler
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{
}