using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channelA = await connection.CreateChannelAsync();
var channelB = await connection.CreateChannelAsync();

await channelA.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
await channelB.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

// create consumer
var consumerA = new EventingBasicConsumer(channelA);
var consumerB = new EventingBasicConsumer(channelB);

// handle received message
consumerA.Received += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // process message
    Console.WriteLine($"Message received by consumer A: {message}");

    // acknowledge message
    await channelA.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
};

consumerB.Received += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // process message
    Console.WriteLine($"Message received  by consumer B: {message}");

    // acknowledge message
    await channelA.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
};

// start consuming
channelA.BasicConsume(queue: "hello-messaging", autoAck: false, consumer: consumerA);
channelB.BasicConsume(queue: "hello-messaging", autoAck: false, consumer: consumerB);

Console.ReadLine();