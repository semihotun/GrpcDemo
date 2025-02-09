using ProtoBuf;
using System.Runtime.Serialization;

namespace ServiceA.Models;

[ProtoContract]
public class HelloRequest
{
    [ProtoMember(1)]
    public string Name { get; set; } = string.Empty;
    
    [ProtoMember(2)]
    public string Surname { get; set; } = string.Empty;
} 