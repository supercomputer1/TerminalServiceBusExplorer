﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Administration;

namespace TerminalServiceBusExplorer.ServiceBus
{
    public class ServiceBusTestWithAdministratorRights
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;
        public ServiceBusTestWithAdministratorRights(ServiceBusAdministrationClient serviceBusAdministrationClient)
        {
            this.serviceBusAdministrationClient = serviceBusAdministrationClient;
        }

        public async Task<Dictionary<int, string>> GetTopics(CancellationToken cancellationToken = default)
        {
            var topics = serviceBusAdministrationClient.GetTopicsAsync(cancellationToken);

            var topicNames = new List<string>();
            await foreach (var topic in topics)
            {
                topicNames.Add(topic.Name);
            }

            int index = 0;
            return topicNames.ToDictionary(k => index++, v => v);
        }

        public async Task<Dictionary<int, string>> GetSubscriptions(string topicName, CancellationToken cancellationToken = default)
        {
            var subscriptions = serviceBusAdministrationClient.GetSubscriptionsAsync(topicName, cancellationToken);

            var subscriptionNames = new List<string>();
            await foreach (var subscription in subscriptions)
            {
                subscriptionNames.Add(subscription.SubscriptionName);
            }

            int index = 0;
            return subscriptionNames.ToDictionary(k => index++, v => v);
        }
    }
}
