using ProtoBuf;
using Pulsar.Common.Messages.other;

namespace Pulsar.Common.Messages.Network
{
    [ProtoContract]
    public class DoUploadAndExecuteResponse : IMessage
    {
        [ProtoMember(1)]
        public bool Result { get; set; }

        [ProtoMember(2)]
        public bool FailureReason { get; set; }
    }
}
