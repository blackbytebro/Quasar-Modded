using ProtoBuf;
using Quasar.Common.Messages.other;

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
    }
}
