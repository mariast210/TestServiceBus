using MediatR;
using TestAzureServiceBus.Messages;

namespace TestAzureServiceBus.Handlers;

public class PaymentProcessedHandler : IRequestHandler<PaymentProcessed>
{
    public Task<Unit> Handle(PaymentProcessed request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Payment processed for user {request.User} with id {request.Id}");
        return Unit.Task;
    }
}