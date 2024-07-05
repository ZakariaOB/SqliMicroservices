namespace Common.Messaging.Events
{
    public class DiscountUpdatedEvent
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Amount { get; set; }
    }
}
