using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using ServiceB.Services;
using ServiceB.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5051, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Service B API", 
        Version = "v1",
        Description = "A simple example gRPC server API"
    });
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service B API V1");
});

// Use our extension method to automatically register all gRPC services
app.MapGrpcServices();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
