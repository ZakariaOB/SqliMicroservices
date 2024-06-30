using Common.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class TestCheckoutHandler
        (ISender sender, ILogger<TestCheckoutHandler> logger) 
        : IConsumer<TestBasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<TestBasketCheckoutEvent> context)
        {
            logger.LogInformation(
                "Test Basket Checkout Event handled: {IntegrationEvent}",
                context.Message.GetType().Name);

            await sender.Send("test command");
        }
    }
}
