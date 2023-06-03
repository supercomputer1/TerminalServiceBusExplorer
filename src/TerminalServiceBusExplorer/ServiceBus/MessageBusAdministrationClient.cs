using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Administration;

namespace TerminalServiceBusExplorer.ServiceBus
{
    public class MessageBusAdministrationClient
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;
        public MessageBusAdministrationClient(ServiceBusAdministrationClient serviceBusAdministrationClient)
        {
            this.serviceBusAdministrationClient = serviceBusAdministrationClient;
        }

        public async Task<List<Topic>> GetTopics(CancellationToken cancellationToken = default)
        {
            var topics = serviceBusAdministrationClient.GetTopicsAsync(cancellationToken);

            var topicNames = new List<string>();
            await foreach (var topic in topics)
            {
                topicNames.Add(topic.Name);
            }

            return topicNames.Select((s, index) => new Topic(index, s)).ToList();
        }

        public async Task<List<Subscription>> GetSubscriptions(string topicName, CancellationToken cancellationToken = default)
        {
            var subscriptions = serviceBusAdministrationClient.GetSubscriptionsAsync(topicName, cancellationToken);

            var subscriptionNames = new List<string>();
            await foreach (var subscription in subscriptions)
            {
                subscriptionNames.Add(subscription.SubscriptionName);
            }

            return subscriptionNames.Select((s, index) => new Subscription(index, s)).ToList();
        }
    }
}