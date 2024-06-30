namespace Samples.Patterns.Mediator.WithoutMediator
{
    public class UserWithoutMediator
    {
        public string Name { get; }
        private readonly List<UserWithoutMediator> _contacts;
        private readonly List<string> _receivedMessages;

        public IReadOnlyList<string> ReceivedMessages => _receivedMessages;


        public UserWithoutMediator(string name)
        {
            Name = name;
            _contacts = new List<UserWithoutMediator>();
            _receivedMessages = new List<string>();
        }

        public void SendMessage(string message, UserWithoutMediator recipient)
        {
            Console.WriteLine($"{Name} sends message to {recipient.Name}: {message}");

            // Insert logic here is not a good idea
            // Sanitize

            recipient.ReceiveMessage(message);
        }

        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{Name} receives message: {message}");
            _receivedMessages.Add(message);
        }

        public void AddContact(UserWithoutMediator user)
        {
            _contacts.Add(user);
        }
    }

}
