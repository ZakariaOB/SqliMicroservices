namespace Samples.Patterns.Mediator.WithMediator
{
    public interface IChatMediator
    {
        void SendMessage(string message, User sender, User recipient);
    }
}
