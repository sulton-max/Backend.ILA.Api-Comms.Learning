using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
};

// create connection
var connection = connectionFactory.CreateConnection();

// create channel
var channel = await connection.CreateChannelAsync();
await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 3, global: false);

// create monitoring consumer
var monitoringConsumer = new EventingBasicConsumer(channel);
monitoringConsumer.Received += async (sender, args) =>
{
    var body = Encoding.UTF8.GetString(args.Body.ToArray());
    Console.WriteLine($"Displaying log message : {body}");
    
    await channel.BasicAckAsync(args.DeliveryTag, false);
};

// create persisting consumer
var persistingConsumer = new EventingBasicConsumer(channel);
persistingConsumer.Received += async (sender, args) =>
{
    var body = Encoding.UTF8.GetString(args.Body.ToArray());
    Console.WriteLine($"Persisting log message : {body}");
    
    await channel.BasicAckAsync(args.DeliveryTag, false);
};

// start consuming
await channel.BasicConsumeAsync(queue: "logs-monitor", autoAck: false, consumer: monitoringConsumer);
await channel.BasicConsumeAsync(queue: "logs-persistence", autoAck: false, consumer: persistingConsumer);

Console.ReadLine();