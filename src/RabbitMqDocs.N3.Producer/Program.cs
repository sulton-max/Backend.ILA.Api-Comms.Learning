using System.Text;
using RabbitMQ.Client;

var notificationsExchangeName = "Notifications";
var renderProcessingQueueName = "RenderProcessing";
var renderProcessingRoutingKey = "OnRenderProcessing";

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
channel.ExchangeDeclare(notificationsExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);

// create queue
var queueName = await channel.QueueDeclareAsync(queue: renderProcessingQueueName, durable: true, exclusive: false, autoDelete: false);

// bind queue to exchange
await channel.QueueBindAsync(queue: queueName, exchange: notificationsExchangeName, routingKey: renderProcessingRoutingKey);

// produce message
var message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

// publish message
await channel.BasicPublishAsync(exchange: notificationsExchangeName, routingKey: renderProcessingRoutingKey, body: body);

Console.WriteLine("Message sent!");