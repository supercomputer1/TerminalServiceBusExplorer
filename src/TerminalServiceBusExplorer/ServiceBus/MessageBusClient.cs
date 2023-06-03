using Azure.Messaging.ServiceBus;
using TerminalServiceBusExplorer.Terminal;
using TerminalServiceBusExplorer.Encoding;

namespace TerminalServiceBusExplorer.ServiceBus
{
    public class MessageBusClient
    {
        private readonly ServiceBusClient serviceBusClient;
        public MessageBusClient(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient;
        }

        public async Task<IEnumerable<Message>> Peek(string topicName, string subscriptionName, int startIndex, int batchSize, CancellationToken cancellationToken = default)
        {
            var reciever = serviceBusClient.CreateReceiver(topicName, subscriptionName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

            var serviceBusReceivedMessages = await reciever.PeekMessagesAsync(batchSize, startIndex, cancellationToken);
            return serviceBusReceivedMessages.Select(s => new Message(s.MessageId, Encoder.Decode(s.Body.ToArray(), "gzip"), s.ContentType, s.EnqueuedTime));
        }
    }
}
