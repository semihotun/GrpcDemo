using ProtoBuf.Grpc;
using ServiceB.Models;

namespace ServiceB.Services;

public class GreeterService : IGreeterService
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public Task<HelloReply> SayHello(HelloRequest request, CallContext context = default)
    {
        _logger.LogInformation("Received request from {Name} {Surname}", request.Name, request.Surname);
        
        var reply = new HelloReply 
        {
            Message = $"Merhaba {request.Name} {request.Surname}!"
        };
        
        return Task.FromResult(reply);
    }
}