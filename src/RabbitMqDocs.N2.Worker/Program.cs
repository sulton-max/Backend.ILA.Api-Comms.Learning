using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// create connection factory
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
using var connection = await factory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

// set prefetch count
await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 5, global: false);

// declare queue
channel.QueueDeclare(queue: "hello-messaging", durable: true, exclusive: false, autoDelete: false, arguments: null);

// create consumer
var consumer = new EventingBasicConsumer(channel);

// create event handler
var random = new Random();

consumer.Received += async (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    await Task.Delay(random.Next(1_000, 3_000));

    var result = true;
    if (result)
    {
        Console.WriteLine($"Received message: {message} {result}");
        await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
    }
    else
    {
        Console.WriteLine($"Received message: {message} {result}");
        await channel.BasicNackAsync(deliveryTag: args.DeliveryTag, multiple: false, requeue: true);
    }
};

// start consuming
channel.BasicConsume(queue: "hello-messaging", autoAck: false, consumer: consumer);

Console.ReadLine();