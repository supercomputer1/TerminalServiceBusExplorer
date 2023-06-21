using TerminalServiceBusExplorer.Terminal;

namespace TerminalServiceBusExplorer.ServiceBus;

public class MessageBus
{
    public List<Topic> Topics { get; set; }

    public void ShowTopics()
    {
        foreach (var topic in Topics)
        {
            Console.WriteLine($"Topic: {topic.Name}, Index: {topic.Identifier}.");
        }
    }

    public void PrintTopic(int index)
    {
        var topic = Topics[index];

        Console.WriteLine($"Topic: {topic.Name}.");
        Console.WriteLine($"Subscriptions:");
        foreach (var x in topic.Subscriptions)
        {
            Console.WriteLine($"Index: {x.Identifier}, Name: {x.Name}, Messages: {x.MessageCount}");
        }
    }

    public Topic GetTopic(int index)
    {
        return Topics[index];
    }
}
