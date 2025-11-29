using GloboTicket.Promotion.Messages.Shows;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Emailer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "masstransit-rabbitmq";

            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host($"rabbitmq://{rabbitMqHost}");
                busConfig.ReceiveEndpoint("GloboTicket.Emailer", endpointConfig =>
                {
                    endpointConfig.Handler<ShowAdded>(async context =>
                        await new ShowAddedHandler().Handle(context.Message));
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Emailer: Receiving messages. Press Ctrl+C to stop.");
            await Task.Delay(-1);

            await bus.StopAsync();
        }
    }
}
