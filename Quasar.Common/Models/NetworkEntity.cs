using ProtoBuf;
using Quasar.Common.Enums;

namespace Quasar.Common.Models
{
    [ProtoContract]
    public class NetworkEntity
    {
        [ProtoMember(1)]
        public string Interface { get; set; }

        [ProtoMember(2)]
        public string MAC { get; set; }

        [ProtoMember(3)]
        public string Address { get; set; }

        [ProtoMember(4)]
        public int[] Ports { get; set; }

        [ProtoMember(5)]
        public string[] Shares { get; set; }
    }
}
