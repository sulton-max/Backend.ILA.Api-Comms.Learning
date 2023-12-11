using System.Text;
using MediatR;
using N2.Microservices.Common.Core.Models.Commands;
using N2.Microservices.Common.Core.Models.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace N2.Microservices.EventBus.EventBus;

public class RabbitMqEventBusBroker : IEventBusBroker
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _eventHandlers;
    private readonly List<Type> _eventTypes;

    public RabbitMqEventBusBroker(IMediator mediator)
    {
        _mediator = mediator;
        _eventHandlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
    }

    public ValueTask PublishLocallyAsync<TEvent>(TEvent command) where TEvent : IEvent
    {
        return new ValueTask(_mediator.Publish(command));
    }

    public ValueTask PublishAsync<T>(T @event) where T : IEvent
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var eventName = @event.GetType().Name;

        channel.QueueDeclare(eventName, false, false, false, null);
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", eventName, null, body);

        return ValueTask.CompletedTask;
    }

    public ValueTask SubscribeAsync<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>
    {
        var eventName = typeof(TEvent).Name;
        var handlerType = typeof(TEventHandler);

        if (!_eventTypes.Contains(typeof(TEvent)))
            _eventTypes.Add(typeof(TEvent));

        if (!_eventHandlers.ContainsKey(eventName))
            _eventHandlers.Add(eventName, new List<Type>());

        if (_eventHandlers[eventName].Exists(handler => handler == handlerType))
            throw new ArgumentException($"{handlerType.Name} already registered for {eventName}");

        _eventHandlers[eventName].Add(typeof(TEventHandler));
        StartBasicConsume<TEvent>();

        return ValueTask.CompletedTask;
    }

    public void StartBasicConsume<T>() where T : IEvent
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var eventName = typeof(T).Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += HandleReceivedEvent;

        channel.BasicConsume(eventName, true, consumer);
    }

    private async Task HandleReceivedEvent(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_eventHandlers.ContainsKey(eventName))
        {
            var subscriptions = _eventHandlers[eventName];
            foreach (var subscription in _eventHandlers[eventName])
            {
                var handler = Activator.CreateInstance(subscription);
                if (handler == null) continue;
                var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                var @event = JsonConvert.DeserializeObject(message, eventType);
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
            }
        }
    }
}