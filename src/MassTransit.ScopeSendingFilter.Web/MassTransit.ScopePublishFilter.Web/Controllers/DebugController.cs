using MassTransit.ScopeSendingFilter.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.ScopeSendingFilter.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DebugController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IMyContextService _myContextService;

    public DebugController(IBus bus, IMyContextService myContextService)
    {
        _bus = bus;
        _myContextService = myContextService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Console.WriteLine("Controller: Scoped service has a value of: " + _myContextService.Id);
        
        await _bus.Publish(new DebugMessage(Guid.NewGuid()));

        return Ok();
    }
}

public record DebugMessage(Guid Id);