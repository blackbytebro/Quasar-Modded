using ProtoBuf;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    public class DoClientMovementResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public string FailureReason { get; set; }
    }
}
