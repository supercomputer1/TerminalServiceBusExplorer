using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TerminalServiceBusExplorer.ServiceBus;

namespace TerminalServiceBusExplorer
{
    public sealed class Worker : BackgroundService
    {
        private readonly ServiceBusTestWithAdministratorRights serviceBusTestWithAdministratorRights;
        private readonly ServiceBusTest serviceBusTest;
        public Worker(ServiceBusTestWithAdministratorRights serviceBusTestWithAdministratorRights, ServiceBusTest serviceBusTest)
        {
            this.serviceBusTestWithAdministratorRights = serviceBusTestWithAdministratorRights;
            this.serviceBusTest = serviceBusTest;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var topics = await serviceBusTestWithAdministratorRights.GetTopics(stoppingToken);
            var subscriptions = await serviceBusTestWithAdministratorRights.GetSubscriptions(topics.First(), stoppingToken);

            // TODO: Get params from user input
            var messages = await serviceBusTest.Peek(topics.First(), subscriptions.First(), 0, 20, stoppingToken);
        }
    }
}
