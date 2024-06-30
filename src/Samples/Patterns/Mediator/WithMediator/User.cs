namespace Samples.Patterns.Mediator.WithMediator
{
    public class User
    {
        public string Name { get; }
        private readonly List<User> _contacts;
        private readonly IChatMediator _mediator;
        private readonly List<string> _receivedMessages;

        public IReadOnlyList<string> ReceivedMessages => _receivedMessages;

        public User(string name, IChatMediator mediator)
        {
            Name = name;
            _mediator = mediator;
            _contacts = new List<User>();
            _receivedMessages = new List<string>();
        }

        public void SendMessage(string message, User recipient)
        {
            _mediator.SendMessage(message, this, recipient);
        }

        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{Name} receives message: {message}");
            _receivedMessages.Add(message);
        }

        public void AddContact(User user)
        {
            _contacts.Add(user);
        }
    }
}
