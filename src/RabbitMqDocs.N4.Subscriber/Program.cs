using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqDocs.N4.Subscriber.Services;

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

// create consumer
var verificationProcessingConsumer = new EventingBasicConsumer(channel);
var homeFeedProcessingConsumer = new EventingBasicConsumer(channel);

// create handler
var accountOrchestrationService = new AccountOrchestrationService();
var homeFeedOrchestrationService = new HomeFeedOrchestrationService();

verificationProcessingConsumer.Received += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    var accountCreatedEvent = JsonSerializer.Deserialize<AccountCreatedEvent>(message)!;

    var result = await accountOrchestrationService.VerifyIdentityAsync(accountCreatedEvent.UserId);

    if (result)
        await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    else
        await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
};

homeFeedProcessingConsumer.Received += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    var accountCreatedEvent = JsonSerializer.Deserialize<AccountCreatedEvent>(message)!;

    var result = await homeFeedOrchestrationService.CreateHomeFeedAsync(accountCreatedEvent.UserId);

    if (result)
        await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    else
        await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
};

// start consuming
await channel.BasicConsumeAsync(queue: identityVerificationQueueName, autoAck: false, consumer: verificationProcessingConsumer);
await channel.BasicConsumeAsync(queue: homeFeedQueueName, autoAck: false, consumer: homeFeedProcessingConsumer);

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