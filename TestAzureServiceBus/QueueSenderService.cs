using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace TestAzureServiceBus;

public class QueueSenderService
{
    private readonly ServiceBusSender _sender;
    private readonly QueueSettings _queueSettings;
    
    public QueueSenderService(IOptions<QueueSettings> queueSettings)
    {
        _queueSettings = queueSettings.Value;
        var serviceBusClient = new ServiceBusClient(_queueSettings.ConnectionString);
        _sender = serviceBusClient.CreateSender(_queueSettings.QueueName);
    }
    
    public async Task SendMessageAsync<T>(T message)
    {
        var body = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(body);
        serviceBusMessage.SessionId = new Guid().ToString();//unique for session, feature only for non-basic pricing tear
        serviceBusMessage.ApplicationProperties.Add("Type", message.GetType().Name);
        await _sender.SendMessageAsync(serviceBusMessage);
    }
}