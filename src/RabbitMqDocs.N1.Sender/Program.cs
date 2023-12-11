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
await channel.QueueDeclareAsync("hello-world", durable: false, exclusive: false, autoDelete: false, arguments: null);

// produce message
var message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello-world", body: body);