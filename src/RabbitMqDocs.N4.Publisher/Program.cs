using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

var identityExchangeName = "Identity";
var identityVerificationQueueName = "IdentityVerificationProcessing";
var homeFeedQueueName = "HomeFeedProcessing";
var accountCreatedEventRoutingKey = "OnAccountCreated";

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

// create exchange
channel.ExchangeDeclare(identityExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);

// create queue
var verificationQueue = await channel.QueueDeclareAsync(queue: identityVerificationQueueName, durable: true, exclusive: false, autoDelete: false);
var homeFeedQueue = await channel.QueueDeclareAsync(queue: homeFeedQueueName, durable: true, exclusive: false, autoDelete: false);

// bind queue to exchange
await channel.QueueBindAsync(queue: identityVerificationQueueName, exchange: identityExchangeName, routingKey: accountCreatedEventRoutingKey);
await channel.QueueBindAsync(queue: homeFeedQueueName, exchange: identityExchangeName, routingKey: accountCreatedEventRoutingKey);

// produce message
var message = new AccountCreatedEvent
{
    Id = Guid.NewGuid(),
    UserId = Guid.NewGuid(),
    CreatedAt = DateTimeOffset.UtcNow
};

var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

// publish message
await channel.BasicPublishAsync(exchange: identityExchangeName, routingKey: accountCreatedEventRoutingKey, body: body);

public abstract class Event
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public bool IsCancelled { get; set; }
}

public abstract class IdentityEvent : Event
{
    public Guid UserId { get; set; }
}

public class AccountCreatedEvent : IdentityEvent
{
}