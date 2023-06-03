using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TerminalServiceBusExplorer.Compression;
using TerminalServiceBusExplorer.Extensions;
using TerminalServiceBusExplorer.ServiceBus;
using TerminalServiceBusExplorer.Terminal;

namespace TerminalServiceBusExplorer
{
    public sealed class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly MessageBusService messageBusService;
        public Worker(IHostApplicationLifetime hostApplicationLifeTime, MessageBusService messageBusService)
        {
            this.hostApplicationLifetime = hostApplicationLifeTime;
            this.messageBusService = messageBusService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
            var topics = await serviceBusTestWithAdministratorRights.GetTopics(stoppingToken);
            topics.Print();
            var topicChoice = Terminal.Input.GetChoice("Enter topic:");


            var subscriptions = await serviceBusTestWithAdministratorRights.GetSubscriptions(topics[topicChoice], stoppingToken);
            var subscriptionChoice = Terminal.Input.GetChoice("Enter subscription:");

            // TODO: Get params from user input
            var messages = await serviceBusTest.Peek(topics[topicChoice], subscriptions[subscriptionChoice], startIndex: 20, batchSize: 100, stoppingToken);
            */

            var messageBus = await messageBusService.GetMessageBus();

            messageBus.PrintTopic(1);


            var messageQueue = new MessageQueue(messageBus.Topics[1].Subscriptions[22].Messages);

            while (messageQueue.HasMessagesToShow)
            {
                if (!Terminal.Input.Continue("Continue? y/n")) break;
                messageQueue.ShowFirst();
            }

            hostApplicationLifetime.StopApplication();
        }
    }
}
