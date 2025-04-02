using ProtoBuf;
using Quasar.Common.Models.Network;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoInterfaceScanResponse : IMessage
    {
        [ProtoMember(1)]
        public InterfaceEntity[] Interfaces { get; set; }

        [ProtoMember(2)]
        public int[] PotentialIps { get; set; }
    }
}
