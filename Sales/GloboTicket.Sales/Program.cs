using GloboTicket.Sales.Messages.Purchases;
using GloboTicket.Sales.Purchasing;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";

            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host($"rabbitmq://{rabbitMqHost}");
                busConfig.ReceiveEndpoint("GloboTicket.Sales", endpointConfig =>
                {
                    endpointConfig.Handler<PurchaseTicket>(async context =>
                    {
                        var handler = new PurchaseTicketHandler();
                        await handler.Handle(context);
                    });
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Sales receiving messages. Press Ctrl+C to stop.");
            await Task.Delay(-1);

            await bus.StopAsync();
        }
    }
}
