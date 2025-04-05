using ProtoBuf;

namespace Quasar.Common.Models
{
    [ProtoContract]
    public class ClientException
    {
        [ProtoMember(1)]
        public string Method { get; set; }

        [ProtoMember(2)]
        public string File { get; set; }

        [ProtoMember(3)]
        public int Line { get; set; }

        [ProtoMember(4)]
        public string Visibility { get; set; }

        [ProtoMember(5)]
        public bool Static { get; set; }

        [ProtoMember(6)]
        public ClientParameter[] Parameters { get; set; }
    }

    [ProtoContract]
    public class ClientParameter
    {

    }
}
