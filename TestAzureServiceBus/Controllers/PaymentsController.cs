using Microsoft.AspNetCore.Mvc;
using TestAzureServiceBus.Messages;

namespace TestAzureServiceBus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly QueueSenderService _queueSenderService;

    public PaymentsController(QueueSenderService queueSenderService)
    {
        _queueSenderService = queueSenderService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentCreated payment)
    {
        await _queueSenderService.SendMessageAsync(payment);
        return Ok();
    }
}