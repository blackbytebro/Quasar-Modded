using ProtoBuf;

namespace Pulsar.Common.Models.Network
{
    [ProtoContract]
    public class ShareEntity
    {
        [ProtoMember(1)]
        public string ShareName { get; set; }

        [ProtoMember(2)]
        public uint ShareType { get; set; }

        [ProtoMember(3)]
        public string Remark { get; set; }

        [ProtoMember(4)]
        public bool RequiresCredentials { get; set; }
    }
}
