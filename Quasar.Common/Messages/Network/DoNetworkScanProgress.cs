using ProtoBuf;
using Quasar.Common.Messages.other;
using Quasar.Common.Models.Network;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoNetworkScanProgress : IMessage
    {
        [ProtoMember(1)]
        public int NetworkInterfaces { get; set; }

        [ProtoMember(2)]
        public int InterfaceIndex { get; set; }

        [ProtoMember(3)]
        public int Addresses { get; set; }

        [ProtoMember(4)]
        public int CurrentAddress { get; set; }

        [ProtoMember(5)]
        public bool ScanningPorts { get; set; }

        [ProtoMember(6)]
        public bool ScanningShares { get; set; }

        [ProtoMember(7)]
        public int CurrentPort { get; set; }

        [ProtoMember(8)]
        public AddressEntity Address { get; set; }

        [ProtoMember(9)]
        public InterfaceEntity NIC { get; set; }
    }
}
