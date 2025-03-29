using Quasar.Common.Enums;
using Quasar.Common.Messages;
// Custom
using Quasar.Common.Models;
using Quasar.Server.Controls;
using Quasar.Server.Forms.DarkMode;
using Quasar.Server.Helper;
using Quasar.Server.Messages;
using Quasar.Server.Networking;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Quasar.Server.Forms
{
    public partial class FrmNetworkMovement : Form
    {
        /// <summary>
        /// The client which can be used for the network movement.
        /// </summary>
        private readonly Client _connectClient;



        /// <summary>
        /// Holds the opened network movement form for each client.
        /// </summary>
        private static readonly Dictionary<Client, FrmNetworkMovement> OpenedForms = new Dictionary<Client, FrmNetworkMovement>();

        /// <summary>
        /// Creates a new network movement form for the client or gets the current open form, if there exists one already.
        /// </summary>
        /// <param name="client">The client used for the network movement form.</param>
        /// <returns>
        /// Returns a new network movement form for the client if there is none currently open, otherwise creates a new one.
        /// </returns>
        public static FrmNetworkMovement CreateNewOrGetExisting(Client client)
        {
            if (OpenedForms.ContainsKey(client))
            {
                return OpenedForms[client];
            }
            FrmNetworkMovement f = new FrmNetworkMovement(client);
            f.Disposed += (sender, args) => OpenedForms.Remove(client);
            OpenedForms.Add(client, f);
            return f;
        }

        public FrmNetworkMovement(Client client)
        {
            _connectClient = client;

            RegisterMessageHandler();
            InitializeComponent();

            DarkModeManager.ApplyDarkMode(this);
        }

        private void RegisterMessageHandler()
        {
            _connectClient.ClientState += ClientDisconnected;
        }

        private void UnregisterMessageHandler()
        {
            _connectClient.ClientState -= ClientDisconnected;
        }

        private void ClientDisconnected(Client client, bool connected)
        {
            if (!connected)
            {
                this.Invoke((MethodInvoker)this.Close);
            }
        }

        private void FrmNetworkMovement_Load(object sender, EventArgs e)
        {
            this.Text = WindowHelper.GetWindowTitle("Network Movement", _connectClient);
        }

        private void toolStripMoveItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripExecuteItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripCommandItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripRefreshItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmNetworkMovement_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterMessageHandler();
        }
    }
}
