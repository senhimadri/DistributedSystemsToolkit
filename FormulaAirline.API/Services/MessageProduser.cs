using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FormulaAirline.API.Services;

public class MessageProduser : IMessageProduser
{
    public void SendingMessages<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password ="mypass",
            VirtualHost ="/"
        };

        var conn = factory.CreateConnection();

        using var channel = conn.CreateModel();

        channel.QueueDeclare(queue:"bookings", durable: true, exclusive: false);

         var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("","bookings",body: body);
    }
}
