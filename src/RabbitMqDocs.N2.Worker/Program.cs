using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// create connection factory
var factory = new ConnectionFactory
{
    HostName = "localhost",
};

// create connection
using var connection = await factory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

// set prefetch count
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

// declare queue
channel.QueueDeclare(queue: "hello-messaging", durable: true, exclusive: false, autoDelete: false, arguments: null);

// create consumer
var consumer = new EventingBasicConsumer(channel);

// create event handler
consumer.Received += async (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    await Task.Delay(5_000);

    Console.WriteLine($"Received message: {message}");
    
    await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
};

// start consuming
channel.BasicConsume(queue: "hello-messaging", autoAck: false, consumer: consumer);

Console.ReadLine();