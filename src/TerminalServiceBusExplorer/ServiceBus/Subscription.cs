using TerminalServiceBusExplorer.Terminal;

namespace TerminalServiceBusExplorer.ServiceBus;

public class Subscription
{
    public Subscription(int identifier, string name)
    {
        Identifier = identifier;
        Name = name;
        Messages = new List<Message>();
    }

    public int Identifier { get; set; }

    public string Name { get; set; }
    public int MessageCount => Messages.Count();
    public List<Message> Messages;

    public void AddMessages(IEnumerable<Message> messages)
    {
        foreach (var message in messages)
        {
            Messages.Add(message);
        }
    }
}
