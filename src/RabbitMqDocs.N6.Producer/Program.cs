using RabbitMQ.Client;

// create connection factory
var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

var properties = new BasicProperties
{
    Persistent = true
}; 

// create exchange
await channel.ExchangeDeclareAsync("logs", type: ExchangeType.Topic, durable: true);

// create queues
await channel.QueueDeclareAsync(queue: "logs-monitor", durable: true, exclusive: false, autoDelete: false);
await channel.QueueDeclareAsync(queue: "logs-persistence", durable: true, exclusive: false, autoDelete: false);

// bind queues to exchanges
await channel.QueueBindAsync(queue: "logs-monitor", exchange: "logs", routingKey: "critical.*");
await channel.QueueBindAsync(queue: "logs-monitor", exchange: "logs", routingKey: "error.*");
await channel.QueueBindAsync(queue: "logs-monitor", exchange: "logs", routingKey: "warning.*");
await channel.QueueBindAsync(queue: "logs-monitor", exchange: "logs", routingKey: "info.*");

await channel.QueueBindAsync(queue: "logs-persistence", exchange: "logs", routingKey: "critical.*");
await channel.QueueBindAsync(queue: "logs-persistence", exchange: "logs", routingKey: "*.store");

// produce messages
await channel.BasicPublishAsync(
    exchange: "logs",
    routingKey: "debug.ignore",
    basicProperties: properties,
    body: "Received request at route /api/products"u8.ToArray()
);

await channel.BasicPublishAsync(
    exchange: "logs",
    routingKey: "warning.store",
    basicProperties: properties,
    body: "Elapsed more time from rendering service than expected"u8.ToArray()
);

await channel.BasicPublishAsync(
    exchange: "logs",
    routingKey: "error.ignore",
    basicProperties: properties,
    body: "Service timeout occurred"u8.ToArray()
);

await channel.BasicPublishAsync(
    exchange: "logs",
    routingKey: "critical.store",
    basicProperties: properties,
    body: "Service unavailable"u8.ToArray()
);

await channel.BasicPublishAsync(
    exchange: "logs",
    routingKey: "critical.trigger",
    basicProperties: properties,
    body: "All payment services are down"u8.ToArray()
);