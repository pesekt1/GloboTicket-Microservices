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
            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
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

            Console.WriteLine("Sales receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
