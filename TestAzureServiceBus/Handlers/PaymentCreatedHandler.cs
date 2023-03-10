using MediatR;
using TestAzureServiceBus.Messages;

namespace TestAzureServiceBus.Handlers;

public class PaymentCreatedHandler : IRequestHandler<PaymentCreated>
{
    public Task<Unit> Handle(PaymentCreated request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Payment created for user {request.User} with id {request.Id}");
        return Unit.Task;
    }
}