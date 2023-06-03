namespace TerminalServiceBusExplorer.ServiceBus;

public class MessageBus
{
    public MessageBus(List<Topic> topics)
    {
        Topics = topics;
    }

    public List<Topic> Topics { get; set; }

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
}
