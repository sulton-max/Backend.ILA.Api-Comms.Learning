using System.Text;
using RabbitMQ.Client;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// connection - RabbitMqConnectionProvider - CreateChannel - 

// ResumeGeneratorService - ResumeProcessingSubscriber
// NotificationAggregatorService - NotificationProcessingSubscriber

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

// create queue
await channel.QueueDeclareAsync(
    queue: "hello-messaging", 
    durable: true, 
    exclusive: false, 
    autoDelete: false);

// produce message
var message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

// publish message
await channel.BasicPublishAsync(
    exchange: string.Empty, 
    routingKey: "hello-messaging", 
    body: body);