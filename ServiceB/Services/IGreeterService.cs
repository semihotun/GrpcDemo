using ProtoBuf.Grpc;
using ServiceB.Models;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ServiceB.Services;

[ServiceContract(Name = "GrpcDemo.GreeterService")]
public interface IGreeterService
{
    [OperationContract]
    Task<HelloReply> SayHello(HelloRequest request, CallContext context = default);
}