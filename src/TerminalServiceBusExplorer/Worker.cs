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
using TerminalServiceBusExplorer.Extensions.cs;
using TerminalServiceBusExplorer.ServiceBus;
using TerminalServiceBusExplorer.Terminal;

namespace TerminalServiceBusExplorer
{
    public sealed class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly ServiceBusTestWithAdministratorRights serviceBusTestWithAdministratorRights;
        private readonly ServiceBusTest serviceBusTest;
        public Worker(IHostApplicationLifetime hostApplicationLifeTime, ServiceBusTestWithAdministratorRights serviceBusTestWithAdministratorRights, ServiceBusTest serviceBusTest)
        {
            this.hostApplicationLifetime = hostApplicationLifeTime;
            this.serviceBusTestWithAdministratorRights = serviceBusTestWithAdministratorRights;
            this.serviceBusTest = serviceBusTest;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var topics = await serviceBusTestWithAdministratorRights.GetTopics(stoppingToken);
            topics.Print();
            var topicChoice = Terminal.Input.GetChoice("Enter topic:");


            var subscriptions = await serviceBusTestWithAdministratorRights.GetSubscriptions(topics[topicChoice], stoppingToken);
            subscriptions.Print();
            var subscriptionChoice = Terminal.Input.GetChoice("Enter subscription:");



            // TODO: Get params from user input
            var messages = await serviceBusTest.Peek(topics[topicChoice], subscriptions[subscriptionChoice], 0, 20, stoppingToken);

            Console.WriteLine($"Messages in subscription: {messages.Count()}.");


            foreach (var msg in messages)
            {
                if (!Terminal.Input.Continue("Continue? y/n")) break;

                var encoding = msg.ApplicationProperties.GetValue<string>("X-Content-Encoding");

                var body = Encoding.Encoder.Decode(msg.Body.ToArray(), encoding);

                new Message(msg.MessageId, body, msg.ContentType, encoding, msg.EnqueuedTime);
                Console.WriteLine(body);
            }

            hostApplicationLifetime.StopApplication();
        }
    }
}
