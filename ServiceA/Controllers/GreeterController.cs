using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc.Client;
using ServiceA.Models;
using ServiceA.Services;
using System.Net.Http;

namespace ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class GreeterController : ControllerBase
{
    private static GrpcChannel? _channel;
    private readonly ILogger<GreeterController> _logger;

    public GreeterController(ILogger<GreeterController> logger)
    {
        _logger = logger;
    }

    [HttpGet("SayHello")]
    public async Task<IActionResult> SayHello([FromQuery] string name, [FromQuery] string surname)
    {
        try
        {
            if (_channel == null)
            {
                var handler = new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    EnableMultipleHttp2Connections = true
                };

                _channel = GrpcChannel.ForAddress("http://serviceb:5051", new GrpcChannelOptions
                {
                    HttpHandler = handler,
                    MaxReceiveMessageSize = 5 * 1024 * 1024, // 5 MB
                    MaxSendMessageSize = 5 * 1024 * 1024 // 5 MB
                });
            }

            var client = _channel.CreateGrpcService<IGreeterService>();

            _logger.LogInformation("gRPC isteği gönderiliyor...");
            
            var reply = await client.SayHello(new HelloRequest
            {
                Name = name,
                Surname = surname
            });

            return Ok(reply);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "gRPC isteği sırasında hata oluştu");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
