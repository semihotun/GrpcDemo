using ProtoBuf.Grpc;
using ServiceA.Models;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ServiceA.Services;

[ServiceContract(Name = "GrpcDemo.GreeterService")]
public interface IGreeterService
{
    [OperationContract]
    Task<HelloReply> SayHello(HelloRequest request, CallContext context = default);
}