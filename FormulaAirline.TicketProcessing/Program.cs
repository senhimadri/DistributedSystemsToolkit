using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Welcome to the ticketing services");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "mypass",
    VirtualHost = "/"
};

var conn = factory.CreateConnection();

using var channel = conn.CreateModel();

channel.QueueDeclare(queue: "bookings", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received +=(model,eventArgs)=>
{
    // Getting byte[]
    var body = eventArgs.Body.ToArray();

    var messege = Encoding.UTF8.GetString(body);

    Console.WriteLine($"A message received - t - {messege}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();