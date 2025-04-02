﻿using ProtoBuf;
using Quasar.Common.Models.Network;
using Quasar.Common.Messages.other;

namespace Quasar.Common.Messages.Network
{
    [ProtoContract]
    public class DoNetworkScan : IMessage
    {
        [ProtoMember(1)]
        public InterfaceEntity nic { get; set; }
    }
}
