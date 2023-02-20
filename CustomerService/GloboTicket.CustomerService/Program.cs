using GloboTicket.Sales.Messages.Purchases;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.CustomerService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
                busConfig.ReceiveEndpoint("GloboTicket.CustomerService", endpointConfig =>
                {
                    endpointConfig.Handler<PurchaseTicketFailed>(context =>
                    {
                        Console.WriteLine($"Purchase ticket failed: {context.Message.failureReason}");
                        return Task.CompletedTask;
                    });
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Customer Service receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
