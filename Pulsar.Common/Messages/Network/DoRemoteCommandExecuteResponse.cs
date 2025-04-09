using ProtoBuf;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoRemoteCommandExecuteResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public string FailureReason { get; set; }

        [ProtoMember(3)]
        public string Output { get; set; }
    }
}
