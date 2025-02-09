using System.Reflection;
using Microsoft.AspNetCore.Builder;
using System.ServiceModel;

namespace ServiceB.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void MapGrpcServices(this WebApplication app)
    {
        var grpcServices = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.CustomAttributes
                    .Any(attr => attr.AttributeType == typeof(ServiceContractAttribute))))
            .Where(t => !t.IsInterface && !t.IsAbstract);

        var method = typeof(GrpcEndpointRouteBuilderExtensions)
            .GetMethod("MapGrpcService", BindingFlags.Public | BindingFlags.Static);

        foreach (var serviceType in grpcServices)
        {
            var generic = method?.MakeGenericMethod(serviceType);
            generic?.Invoke(null, new object[] { app });
        }
    }
}
