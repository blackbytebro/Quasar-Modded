using ProtoBuf;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoClientMovementResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public string FailureReason { get; set; }
    }
}
