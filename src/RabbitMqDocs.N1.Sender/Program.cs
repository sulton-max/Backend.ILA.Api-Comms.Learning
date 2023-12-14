using System.Text;
using RabbitMQ.Client;

// create connection factory
var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

// create connection
using var connection = await factory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

// declare queue
await channel.QueueDeclareAsync(
    queue: "hello-world", // queue-name
    durable: true, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);

// produce message
var message = "Hello messaging!";
var body = Encoding.UTF8.GetBytes(message);

// exchange types

// direct exchange - routing key orqali ishlaydi
// default exchange - rabbit mq tomonidan e'lon qilingan

// publish message
await channel
    .BasicPublishAsync(
        exchange: string.Empty, 
        routingKey: "hello-world", // routing key - bu qaysi queue borishini belgilaydi
        body: body);