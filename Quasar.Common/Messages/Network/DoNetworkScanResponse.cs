using ProtoBuf;
using System.Net;
using Quasar.Common.Messages.other;
using Quasar.Common.Models.Network;

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
    public class NetworkScanProgress : IMessage
    {
        [ProtoMember(1)]
        public int Interfaces { get; set; }

        [ProtoMember(2)]
        public int InterfaceIndex { get; set; }

        [ProtoMember(3)]
        public int Addresses { get; set; }

        [ProtoMember(4)]
        public int CurrentAddress { get; set; }
    }
}
