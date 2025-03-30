using ProtoBuf;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoNetworkScanResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public string FailureReason { get; set; }

        [ProtoMember(3)]
        public InterfaceEntity[] Interfaces { get; set; }

        [ProtoMember(4)]
        public AddressEntity[] Addresses { get; set; }
    }

    [ProtoContract]
    public struct InterfaceEntity
    {
        [ProtoMember(1)]
        public string name { get; set; }

        [ProtoMember(2)]
        public string MAC { get; set; }
    }

    [ProtoContract]
    public struct AddressEntity
    {
        [ProtoMember(1)]
        public int interfaceIndex { get; set; }

        [ProtoMember(2)]
        public string Address { get; set; }

        [ProtoMember(3)]
        public int[] Ports { get; set; }

        [ProtoMember(4)]
        public string[] Shares { get; set; }
    }
}
