using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
};

// create connection
using var connection = await connectionFactory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

// declare queue
await channel.QueueDeclareAsync(
    queue: "hello-world", 
    durable: false, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);

// create consumer
var consumer = new EventingBasicConsumer(channel);
    
// create event handler
consumer.Received += (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // Process message

    Console.WriteLine(message);
};

// start consuming
await channel.BasicConsumeAsync(
    queue: "hello-world", 
    autoAck: true, 
    consumer: consumer);

Console.ReadLine();