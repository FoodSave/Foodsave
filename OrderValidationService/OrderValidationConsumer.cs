using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessagingContracts.Orders;
using Microsoft.Extensions.Hosting;

namespace OrderValidationService
{
    public class OrderValidationConsumer : IConsumer<IOrderStartedEvent>
    {
        //Validate meteen
        public async Task Consume(ConsumeContext<IOrderStartedEvent> context)
        {
            Console.WriteLine("I Just validated an order!!!!");
            await context.Publish<IOrderValidatedEvent>(new
            {
                OrderId = context.Message.OrderId,
                PackagesIds = context.Message.PackagesIds
            });
        }
    }
}