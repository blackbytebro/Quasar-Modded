using ProtoBuf;
using System.Net;
using Pulsar.Common.Enums;

namespace Pulsar.Common.Models.Network
{
    [ProtoContract]
    public class NetworkEntity
    {
        [ProtoMember(1)]
        public InterfaceEntity NIC { get; set; }

        [ProtoMember(3)]
        public AddressEntity Address { get; set; }
    }
}
