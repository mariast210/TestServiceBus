using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Options;

namespace TestAzureServiceBus;

public class QueueConsumerService : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly QueueSettings _queueSettings;
    private readonly IMediator _mediator;

    public QueueConsumerService(IOptions<QueueSettings> queueSettings, IMediator mediator)
    {
        _queueSettings = queueSettings.Value;

        var serviceBusClient = new ServiceBusClient(_queueSettings.ConnectionString);
        _processor = serviceBusClient.CreateProcessor(_queueSettings.QueueName, new ServiceBusProcessorOptions());
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;
        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var type = Type.GetType($"TestAzureServiceBus.Messages.{args.Message.ApplicationProperties["Type"]}");

        if (type is null)
        {
            throw new NotImplementedException("Message type not implemented");
        }

        var message = JsonSerializer.Deserialize(body, type);
        await _mediator.Send(message);

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}