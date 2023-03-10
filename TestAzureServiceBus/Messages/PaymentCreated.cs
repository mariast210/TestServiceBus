using MediatR;
using TestAzureServiceBus.Handlers;

namespace TestAzureServiceBus.Messages;

public class PaymentCreated : QueueMessage
{
    public int Id { get; set; }
    public string User { get; set; }
}