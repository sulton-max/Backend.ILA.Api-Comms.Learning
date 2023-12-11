using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var renderProcessingQueueName = "RenderProcessing";

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

// create consumer
var consumer = new EventingBasicConsumer(channel);

// handle received message
consumer.Received += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // process message
    Console.WriteLine($"Message received: {message}");

    // acknowledge message
    await channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
};

// start consuming
channel.BasicConsume(queue: renderProcessingQueueName, autoAck: false, consumer: consumer);

Console.ReadLine();