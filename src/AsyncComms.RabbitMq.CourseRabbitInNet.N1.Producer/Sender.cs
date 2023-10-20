using System.Text;
using RabbitMQ.Client;

namespace AsyncComms.RabbitMq.CourseRabbitInNet.N1.Producer;

public class Sender
{
    public void Publish()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("BasicTest", false, false, false);

        var message = "Getting started with .NET Core Rabbit MQ";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", "BasicTest", null, body);
    }
}