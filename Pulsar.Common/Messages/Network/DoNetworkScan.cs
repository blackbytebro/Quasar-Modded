using ProtoBuf;
using Pulsar.Common.Models.Network;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoNetworkScan : IMessage
    {
        [ProtoMember(1)]
        public InterfaceEntity nic { get; set; }
    }
}
