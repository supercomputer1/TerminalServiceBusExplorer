using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TerminalServiceBusExplorer;
using TerminalServiceBusExplorer.ServiceBus;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{


    services.AddSingleton((s) =>
    {
        return new ServiceBusClient(hostContext.Configuration["AppSettings:ServiceBus:ConnectionString"]);
    });

    // This is a ServiceBusClient with administrator privileges. 
    // Unfortunately it can not read messages from a queue/topic/subscription,
    // but it can list all available topics/queues/subscriptions.
    services.AddSingleton((s) =>
    {
        return new ServiceBusAdministrationClient(hostContext.Configuration["AppSettings:ServiceBus:ConnectionString"]);
    });

    services.AddSingleton<MessageBusClient>();
    services.AddSingleton<MessageBusAdministrationClient>();
    services.AddSingleton<MessageBusService>();
    services.AddHostedService<Worker>();
});


using var host = builder.Build();

host.Run();
