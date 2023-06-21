
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
        var messageBus = new MessageBus();
        await GetTopics(messageBus, cancellationToken);
        await GetSubscriptions(messageBus, cancellationToken);
        await GetMessages(messageBus, cancellationToken);

        return messageBus;
    }

    private async Task GetTopics(MessageBus messageBus, CancellationToken cancellationToken = default)
    {
        messageBus.Topics = await messageBusAdministrationClient.GetTopics(cancellationToken);
    }

    private async Task GetSubscriptions(MessageBus messageBus, CancellationToken cancellationToken = default)
    {
        foreach (var topic in messageBus.Topics)
        {
            topic.Subscriptions = await messageBusAdministrationClient.GetSubscriptions(topic.Name, cancellationToken);
        }
    }

    private async Task GetMessages(MessageBus messageBus, CancellationToken cancellationToken = default)
    {
        foreach (var topic in messageBus.Topics)
        {
            foreach (var subscription in topic.Subscriptions)
            {
                subscription.AddMessages(await messageBusClient.Peek(topic.Name, subscription.Name, 0, 100, cancellationToken));
            }
        }
    }
}
