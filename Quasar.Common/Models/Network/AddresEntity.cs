using ProtoBuf;
using System.Net;

namespace Quasar.Common.Models.Network
{
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
