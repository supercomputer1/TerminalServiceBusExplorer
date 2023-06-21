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
        public Worker(IHostApplicationLifetime hostApplicationLifetime, MessageBusService messageBusService)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.messageBusService = messageBusService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageBus = await messageBusService.GetMessageBus(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                messageBus.ShowTopics();

                var whichTopic = Input.GetChoice("Select a topic:");
                var topic = messageBus.GetTopic(whichTopic);

                messageBus.PrintTopic(whichTopic);

                var whichSubscription = Input.GetChoice("Select a subscription:");

                var messageQueue = new MessageQueue(topic.Subscriptions[whichSubscription].Messages);

                while (messageQueue.HasMessagesToShow)
                {
                    if (!Input.Continue("Continue? y/n")) break;
                    messageQueue.ShowFirst();
                }

                // TODO: Read user input to determine if the loop should
                // be exited.
                Console.WriteLine("Would like to view another subscription?");
            }

            hostApplicationLifetime.StopApplication();
        }
    }
}
