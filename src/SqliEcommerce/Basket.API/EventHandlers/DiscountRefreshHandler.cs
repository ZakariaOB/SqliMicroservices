using Common.Messaging.Events;
using MassTransit;
using MediatR;

namespace Basket.API.EventHandlers
{
    public class DiscountRefreshHandler
        (ISender sender, ILogger<DiscountUpdatedEvent> logger)
        : IConsumer<DiscountUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<DiscountUpdatedEvent> context)
        {
            logger.LogInformation(
                "Test Discount Event Update: {IntegrationEvent}",
                context.Message.GetType().Name);

            // await sender.Send("test command");
        }
    }
}