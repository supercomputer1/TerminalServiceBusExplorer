namespace TerminalServiceBusExplorer.ServiceBus;

public class Topic
{
    public Topic(int identifier, string name)
    {
        Identifier = identifier;
        Name = name;
    }

    public int Identifier { get; set; }
    public string Name { get; set; }
    public List<Subscription> Subscriptions = new List<Subscription>();

    public void AddSubscriptions(IEnumerable<Subscription> subscriptions)
    {
        foreach (var subscription in subscriptions)
        {
            Subscriptions.Add(subscription);
        }
    }



}
