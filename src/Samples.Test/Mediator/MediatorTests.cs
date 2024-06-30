using Samples.Patterns.Mediator.WithMediator;
using Samples.Patterns.Mediator.WithoutMediator;

namespace Samples.Test.Mediator
{
    public class MediatorTests
    {
        [Fact]
        public void SendMessage_DirectCommunication()
        {
            // Arrange
            var alice = new UserWithoutMediator("Alice"); // Mediator is not used directly
            var bob = new UserWithoutMediator("Bob"); // Mediator is not used directly

            // Act
            alice.SendMessage("Hello Bob", bob);

            // Assert
            // Verify that Bob received the message
            Assert.Contains("Hello Bob", bob.ReceivedMessages);
        }

        [Fact]
        public void SendMessage_MediatedCommunication()
        {
            // Arrange
            var mediator = new ChatMediator();
            var alice = new User("Alice", mediator);
            var bob = new User("Bob", mediator);

            // Act
            alice.SendMessage("Hello Bob", bob);

            // Assert
            // Verify that Bob received the message
            Assert.Contains("Hello Bob", bob.ReceivedMessages);
        }

        [Fact]
        public void SendMessage_MediatedCommunication_SpecialCharacters()
        {
            // Arrange
            var mediator = new ChatMediator();
            var alice = new User("Alice", mediator);
            var bob = new User("Bob", mediator);

            // Act
            alice.SendMessage("Hey @Bob! How are you?", bob);

            // Assert
            // Verify that special characters are removed from the message
            Assert.Contains("Hey Bob How are you", bob.ReceivedMessages);
        }
    }
}
