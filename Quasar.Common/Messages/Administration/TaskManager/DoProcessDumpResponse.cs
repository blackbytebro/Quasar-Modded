using ProtoBuf;
using Quasar.Common.Enums;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Administration.TaskManager
{
    [ProtoContract]
    public class DoProcessDumpResponse : IMessage
    {
        [ProtoMember(1)]
        public byte[] Memory { get; set; }

        [ProtoMember(2)]
        public bool Result { get; set; }
    }
}
