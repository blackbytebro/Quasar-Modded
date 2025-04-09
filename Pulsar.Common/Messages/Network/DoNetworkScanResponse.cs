using ProtoBuf;
using Pulsar.Common.Models.Network;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoNetworkScanResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public string FailureReason { get; set; }

        [ProtoMember(3)]
        public InterfaceEntity Interface { get; set; }

        [ProtoMember(4)]
        public AddressEntity Address { get; set; }
    }
}
