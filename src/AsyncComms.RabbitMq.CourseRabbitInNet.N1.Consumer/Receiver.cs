using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AsyncComms.RabbitMq.CourseRabbitInNet.N1.Consumer;

public class Receiver
{
    public void Consume()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("BasicTest", false, false, false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine("Received message {0} ... ", message);
        };

        channel.BasicConsume("BasicTest", true, consumer);
    }
}