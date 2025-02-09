using Microsoft.AspNetCore.Mvc;
using ServiceB.Models;
using ServiceB.Services;

namespace ServiceB.Controllers;

[ApiController]
[Route("[controller]")]
public class GreeterController : ControllerBase
{
    private readonly ILogger<GreeterController> _logger;
    private readonly GreeterService _greeterService;

    public GreeterController(ILogger<GreeterController> logger, GreeterService greeterService)
    {
        _logger = logger;
        _greeterService = greeterService;
    }

    [HttpGet("SayHello")]
    public async Task<IActionResult> SayHello([FromQuery] string name, [FromQuery] string surname)
    {
        try
        {
            var reply = await _greeterService.SayHello(new HelloRequest
            {
                Name = name,
                Surname = surname
            }, default);

            return Ok(reply);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "İstek işlenirken hata oluştu");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
