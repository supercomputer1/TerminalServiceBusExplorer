using TerminalServiceBusExplorer.Terminal;

namespace TerminalServiceBusExplorer.ServiceBus;

public class MessageBusService
{
    private readonly MessageBusAdministrationClient messageBusAdministrationClient;
    private readonly MessageBusClient messageBusClient;
    public MessageBusService(MessageBusAdministrationClient messageBusAdministrationClient, MessageBusClient messageBusClient)
    {
        this.messageBusAdministrationClient = messageBusAdministrationClient;
        this.messageBusClient = messageBusClient;
    }

    public async Task<MessageBus> GetMessageBus(CancellationToken cancellationToken = default)
    {
        var topics = await GetTopics(cancellationToken);

        foreach (var topic in topics)
        {
            topic.AddSubscriptions(await GetSubscriptions(topic.Name, cancellationToken));
        }

        return new MessageBus(topics);
    }

    private async Task<List<Topic>> GetTopics(CancellationToken cancellationToken)
    {
        return await messageBusAdministrationClient.GetTopics(cancellationToken);
    }

    private async Task<List<Subscription>> GetSubscriptions(string topic, CancellationToken cancellationToken = default)
    {
        var subscriptions = await messageBusAdministrationClient.GetSubscriptions(topic);
        foreach (var sub in subscriptions)
        {
            sub.AddMessages(await messageBusClient.Peek(topic, sub.Name, 0, 20, cancellationToken));
        }

        return subscriptions;
    }
}
