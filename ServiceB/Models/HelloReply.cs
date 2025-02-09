using ProtoBuf;
using System.Runtime.Serialization;

namespace ServiceB.Models;

[ProtoContract]
public class HelloReply
{
    [ProtoMember(1)]
    public string Message { get; set; } = string.Empty;
} 