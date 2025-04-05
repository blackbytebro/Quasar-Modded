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
using System.ComponentModel;
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

        private BindingList<InterfaceBoxItem> _interfaceItems = new BindingList<InterfaceBoxItem>();
        private BindingList<EntityListItem> _networkEntities = new BindingList<EntityListItem>();

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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public FrmNetworkMovement(Client client)
        {
            _connectClient = client;
            _networkHandler = new NetworkHandler(client);

            RegisterMessageHandler();
            InitializeComponent();

            cmbInterfaces.DataSource = _interfaceItems;
            _networkEntities.ListChanged += NetworkEntitiesChanged;

            DarkModeManager.ApplyDarkMode(this);
        }

        private void NetworkEntitiesChanged(object sender, ListChangedEventArgs e)
        {
            lstNetworkEntities.BeginUpdate();
            lstNetworkEntities.Items.Clear();
            foreach (EntityListItem obj in _networkEntities)
            {
                ListViewItem item = obj.toItem();
                lstNetworkEntities.Items.Add(item);
            }
            lstNetworkEntities.EndUpdate();
        }

        private void RegisterMessageHandler()
        {
            _connectClient.ClientState += ClientDisconnected;
            _networkHandler.NetworkScanResponseEvent += NetworkEntitiesChanged;
            _networkHandler.NetworkScanProgressEvent += ScanProgressChanged;
            _networkHandler.InterfaceScanResponseEvent += InterfaceEntitiesChanged;
            MessageHandler.Register(_networkHandler);
        }

        private void UnregisterMessageHandler()
        {
            MessageHandler.Unregister(_networkHandler);
            _networkHandler.InterfaceScanResponseEvent -= InterfaceEntitiesChanged;
            _networkHandler.NetworkScanProgressEvent -= ScanProgressChanged;
            _networkHandler.NetworkScanResponseEvent -= NetworkEntitiesChanged;
            _connectClient.ClientState -= ClientDisconnected;
        }

        private void NetworkEntitiesChanged(object sender, NetworkScanResponseEventArgs args)
        {
            try
            {
                _networkEntities.Remove(_networkEntities.Where(item => item.address.Address.ToString() == args.Packet.Address.ToString() && item.nic.Name == args.Packet.Interface.Name).FirstOrDefault());
            }
            finally
            {
                _networkEntities.Add(new EntityListItem { Result = args.Packet.Result, FailureReason = args.Packet.FailureReason, address = args.Packet.Address, nic = args.Packet.Interface });
            }
        }

        private void InterfaceEntitiesChanged(object sender, InterfaceScanResponseEventArgs args)
        {
            _interfaceItems.Clear();
            if (args.Packet.Interfaces.Length == args.Packet.PotentialIps.Length)
            {
                for (int i = 0; i < args.Packet.Interfaces.Length; i++)
                {
                    _interfaceItems.Add(new InterfaceBoxItem { entity = args.Packet.Interfaces[i], potentialIps = args.Packet.PotentialIps[i] });
                }
            }
            else
            {
                foreach (InterfaceEntity nic in args.Packet.Interfaces)
                {
                    _interfaceItems.Add(new InterfaceBoxItem { entity = nic, potentialIps = -1 });
                }
            }
        }

        private void ScanProgressChanged(object sender, NetworkScanProgressEventArgs args)
        {
            //This technically has a bug where users can start 2 scans, which then fucks the progress view
            //But you only have to fix that if we have some STUPID fucking users.
            if (args.Packet.Addresses <= 0)
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripProgress.Text = "Status: Idle...";
                    });
                }
                else
                {
                    toolStripProgress.Text = "Status: Idle...";
                }
            }
            else
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripProgress.Text = $"Status: Scanning {args.Packet.Addresses} IPs... ({Math.Round((double)(args.Packet.CurrentAddress / args.Packet.Addresses), 4)}% Complete)";
                    });
                }
                else
                {
                    toolStripProgress.Text = $"Status: Scanning {args.Packet.Addresses} IPs... ({Math.Round((double)(args.Packet.CurrentAddress / args.Packet.Addresses), 4)}% Complete)";
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
                InputBox.Show("Password", "Enter Password:", ref password) == DialogResult.OK)
            {

            }
        }

        private void toolStripExecuteItem_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string command = string.Empty;
            if (InputBox.Show("Username", "Enter Username:", ref username) == DialogResult.OK &&
                InputBox.Show("Password", "Enter Password:", ref password) == DialogResult.OK)
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

        private void btnInterfaceRefresh_Click(object sender, EventArgs e)
        {
            _networkHandler.RefreshInterfaces();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            _networkHandler.RefreshEntities((cmbInterfaces.SelectedItem as InterfaceBoxItem).entity);
        }
    }

    public class InterfaceBoxItem
    {
        public InterfaceEntity entity { get; set; }
        public int potentialIps { get; set; }
        public override string ToString()
        {
            return entity.Name + (potentialIps > -1 ? $" ({potentialIps} IPs)" : "");
        }
    }

    public class EntityListItem
    {
        public AddressEntity address { get; set; }

        public InterfaceEntity nic { get; set; }

        public bool Result { get; set; }

        public string FailureReason { get; set; }

        public ListViewItem toItem()
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = nic.Name.ToString();
            lvi.SubItems.Add(address.Address.ToString());
            lvi.SubItems.Add(string.Join(", ", address.Ports));
            lvi.SubItems.Add(string.Join(", ", address.Shares));
            return lvi;
        }

    }
}
