﻿using ProtoBuf;
using Quasar.Common.Enums;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Administration.FileManager
{
    [ProtoContract]
    public class DoPathRename : IMessage
    {
        [ProtoMember(1)]
        public string Path { get; set; }

        [ProtoMember(2)]
        public string NewPath { get; set; }

        [ProtoMember(3)]
        public FileType PathType { get; set; }
    }
}
