using ProtoBuf;
using System.Net;
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
        public InterfaceEntity Interface { get; set; }

        [ProtoMember(4)]
        public AddressEntity Address { get; set; }
    }

    [ProtoContract]
    public struct InterfaceEntity
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string MAC { get; set; }
    }

    [ProtoContract]
    public struct AddressEntity
    {
        [ProtoMember(1)]
        public int InterfaceIndex { get; set; }

        [ProtoMember(2)]
        public IPAddress Address { get; set; }

        [ProtoMember(3)]
        public int[] Ports { get; set; }

        [ProtoMember(4)]
        public string[] Shares { get; set; }
    }
}
