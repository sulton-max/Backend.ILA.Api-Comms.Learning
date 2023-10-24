using AsyncComms.RabbitMq.CourseRabbitInNet.N1.Consumer;

var receiver = new Receiver();
receiver.Consume();

Console.ReadLine();