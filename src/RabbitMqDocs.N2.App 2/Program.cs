using System.Text;
using RabbitMQ.Client;

// create connection factory
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
using var connection = await factory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

// declare queue
channel.QueueDeclare(
    queue: "hello-messaging", 
    durable: true, 
    exclusive: false, 
    autoDelete: false);

// create properties
var props = new BasicProperties
{
    Persistent = true
};

// create message
var count = 0;
var body = () => Encoding.UTF8.GetBytes($"Hello World! - {count++}");

// publish message
await Task.WhenAll(
    Enumerable.Range(1, 20)
        .Select(async _ => { await channel.BasicPublishAsync(
            exchange: string.Empty, 
            routingKey: "hello-messaging",  
            basicProperties: props, 
            body: body()); })
);