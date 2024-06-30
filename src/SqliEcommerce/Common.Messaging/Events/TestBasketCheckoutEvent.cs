namespace Common.Messaging.Events
{
    public record  TestBasketCheckoutEvent : IntegrationEvent
    {
        public string? TestCheckout { get; set; }
    }
}
