using ProtoBuf;

namespace Pulsar.Common.Models.Network
{
    [ProtoContract]
    public class InterfaceEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string MAC { get; set; }
    }
}
