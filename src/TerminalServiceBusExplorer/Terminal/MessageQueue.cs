
namespace TerminalServiceBusExplorer.Terminal;

public class MessageQueue
{
    public MessageQueue(IEnumerable<Message> messages)
    {
        Messages = messages.ToList();
    }

    private List<Message> Messages = new List<Message>();
    public bool HasMessagesToShow => Messages.Any();

    private void Pop()
    {
        if (HasMessagesToShow)
        {
            Messages.RemoveAt(0);
        }
    }

    public void ShowFirst()
    {
        Messages.First().Print();

        // I'm assuming that we are 'done' with 
        // the message after it has been viewed.
        Pop();
    }
}
