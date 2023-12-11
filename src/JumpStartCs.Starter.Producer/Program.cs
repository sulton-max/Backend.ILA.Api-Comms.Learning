using System.Text;
using RabbitMQ.Client;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection 
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

// declare queue
await channel.QueueDeclareAsync(
    queue: "test-queue", 
    durable: false, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);

// produce message
var message = "Hello Async Communication!";
var messageBody = Encoding.UTF8.GetBytes(message);

// publish message
await channel.BasicPublishAsync("", "test-queue", messageBody);