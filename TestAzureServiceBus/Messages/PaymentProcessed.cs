using MediatR;
using TestAzureServiceBus.Handlers;

namespace TestAzureServiceBus.Messages;

public class PaymentProcessed : QueueMessage
{
    public int Id { get; set; }
    public string User { get; set; }
    public int Amount { get; set; }
}