using ProtoBuf;

namespace Quasar.Common.Models.Network
{
    [ProtoContract]
    public class AddressEntity
    {
        [ProtoMember(1)]
        public int InterfaceIndex { get; set; }

        [ProtoMember(2)]
        public string Address { get; set; }

        [ProtoMember(3)]
        public int[] Ports { get; set; }

        [ProtoMember(4)]
        public ShareEntity[] Shares { get; set; }
    }
}
