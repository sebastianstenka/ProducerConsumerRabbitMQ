using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare("BaseTest", false, false, false, null);
        channel.CallbackException += (sender, args) =>
        {
            Console.WriteLine($"Exception {args.Exception.Message}");
        };

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received message: {message}");
        };

        channel.BasicConsume("BasicTest", true, consumer);

        Console.WriteLine("Press [enter] to exit the Consumer...");
        Console.ReadLine();
    }
}