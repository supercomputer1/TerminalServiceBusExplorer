using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace TerminalServiceBusExplorer.ServiceBus
{
    public class ServiceBusTest
    {
        private readonly ServiceBusClient serviceBusClient; 
        public ServiceBusTest(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient; 
        }

        public async Task<IEnumerable<ServiceBusReceivedMessage>> Peek(string topicName, string subscriptionName, int startIndex, int batchSize, CancellationToken cancellationToken = default)
        {
            var reciever = serviceBusClient.CreateReceiver(topicName, subscriptionName, new ServiceBusReceiverOptions(){ ReceiveMode = ServiceBusReceiveMode.PeekLock});

            var messages = await reciever.PeekMessagesAsync(batchSize, startIndex, cancellationToken);
            return messages; 
        }
    }
}
