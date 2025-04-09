using ProtoBuf;
using Pulsar.Common.Models.Network;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoInterfaceScanResponse : IMessage
    {
        [ProtoMember(1)]
        public InterfaceEntity[] Interfaces { get; set; }

        [ProtoMember(2)]
        public int[] PotentialIps { get; set; }
    }
}
