using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

using(var connection = factory.CreateConnection())
{
    using(var channel = connection.CreateModel())
    {
        channel.QueueDeclare("BasicTest", false, false, false, null);

        var message = "Message send to RabbitMq queue";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(string.Empty, "BasicTest", null, body);
        Console.WriteLine($"SentMessage message {message}");

    }
}

Console.WriteLine("Press [enter] to exit the Consumer...");
Console.ReadLine();
