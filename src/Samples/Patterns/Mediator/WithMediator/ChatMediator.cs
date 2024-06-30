using System.Text.RegularExpressions;

namespace Samples.Patterns.Mediator.WithMediator
{
    public class ChatMediator : IChatMediator
    {
        public void SendMessage(string message, User sender, User recipient)
        {
            Console.WriteLine($"{sender.Name} sends message to {recipient.Name}: {message}");

            // Logic
            if (string.IsNullOrWhiteSpace(message) )
            {
                Console.WriteLine("Message should not be empty");
                return;
            }

            message = SanitizeMessage(message); // Sanitize message before sending

            // Other logic could be added here
            recipient.ReceiveMessage(message);
        }

        private static string SanitizeMessage(string message)
        {
            // Remove special characters using regex
            return Regex.Replace(message, @"[^\w\s]", string.Empty);
        }
    }
}
