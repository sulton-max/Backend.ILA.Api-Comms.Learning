// create connection factory 

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

// create event consumer
var consumer = new EventingBasicConsumer(channel);

// add event handler and processor
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // invoke processor

    Console.WriteLine($"Message received: {message}");
};

// subscribe to queue
await channel.BasicConsumeAsync(queue: "test-queue", autoAck: true, consumer: consumer);

Console.ReadLine();