namespace Pulsar.Server.Forms
{
    partial class FrmNetworkMovement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Pulsar.Server.Utilities.ListViewColumnSorter listViewColumnSorter1 = new Pulsar.Server.Utilities.ListViewColumnSorter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetworkMovement));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAddresses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnInterface = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.columnShares = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnScan = new System.Windows.Forms.Button();
            this.cmbInterfaces = new System.Windows.Forms.ComboBox();
            this.toolStripRefreshItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCommandItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExecuteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInterfaceRefresh = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lstNetworkEntities = new Pulsar.Server.Controls.AeroListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 85;
            // 
            // columnPorts
            // 
            this.columnPorts.Text = "Ports";
            this.columnPorts.Width = 342;
            // 
            // columnAddresses
            // 
            this.columnAddresses.Text = "IP";
            this.columnAddresses.Width = 97;
            // 
            // columnInterface
            // 
            this.columnInterface.Text = "Interface";
            this.columnInterface.Width = 98;
            // 
            // toolStripProgress
            // 
            this.toolStripProgress.Name = "toolStripProgress";
            this.toolStripProgress.Size = new System.Drawing.Size(73, 17);
            this.toolStripProgress.Text = "Status: Idle...";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip";
            // 
            // columnShares
            // 
            this.columnShares.Text = "Shares";
            this.columnShares.Width = 181;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(400, 21);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(404, 24);
            this.btnScan.TabIndex = 7;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // cmbInterfaces
            // 
            this.cmbInterfaces.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbInterfaces.FormattingEnabled = true;
            this.cmbInterfaces.Location = new System.Drawing.Point(0, 0);
            this.cmbInterfaces.Name = "cmbInterfaces";
            this.cmbInterfaces.Size = new System.Drawing.Size(800, 21);
            this.cmbInterfaces.TabIndex = 5;
            // 
            // toolStripRefreshItem
            // 
            this.toolStripRefreshItem.Image = global::Pulsar.Server.Properties.Resources.refresh;
            this.toolStripRefreshItem.Name = "toolStripRefreshItem";
            this.toolStripRefreshItem.Size = new System.Drawing.Size(179, 26);
            this.toolStripRefreshItem.Text = "Refresh";
            this.toolStripRefreshItem.Click += new System.EventHandler(this.toolStripRefreshItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // toolStripCommandItem
            // 
            this.toolStripCommandItem.Image = global::Pulsar.Server.Properties.Resources.application_view_xp_terminal;
            this.toolStripCommandItem.Name = "toolStripCommandItem";
            this.toolStripCommandItem.Size = new System.Drawing.Size(179, 26);
            this.toolStripCommandItem.Text = "Remote Command";
            this.toolStripCommandItem.Click += new System.EventHandler(this.toolStripCommandItem_Click);
            // 
            // toolStripExecuteItem
            // 
            this.toolStripExecuteItem.Image = global::Pulsar.Server.Properties.Resources.application_go;
            this.toolStripExecuteItem.Name = "toolStripExecuteItem";
            this.toolStripExecuteItem.Size = new System.Drawing.Size(179, 26);
            this.toolStripExecuteItem.Text = "Remote Execute";
            this.toolStripExecuteItem.Click += new System.EventHandler(this.toolStripExecuteItem_Click);
            // 
            // toolStripMoveItem
            // 
            this.toolStripMoveItem.Image = global::Pulsar.Server.Properties.Resources.computer_go;
            this.toolStripMoveItem.Name = "toolStripMoveItem";
            this.toolStripMoveItem.Size = new System.Drawing.Size(179, 26);
            this.toolStripMoveItem.Text = "Move To";
            this.toolStripMoveItem.Click += new System.EventHandler(this.toolStripMoveItem_Click);
            // 
            // btnInterfaceRefresh
            // 
            this.btnInterfaceRefresh.Location = new System.Drawing.Point(-3, 21);
            this.btnInterfaceRefresh.Name = "btnInterfaceRefresh";
            this.btnInterfaceRefresh.Size = new System.Drawing.Size(404, 24);
            this.btnInterfaceRefresh.TabIndex = 6;
            this.btnInterfaceRefresh.Text = "Refresh";
            this.btnInterfaceRefresh.UseVisualStyleBackColor = true;
            this.btnInterfaceRefresh.Click += new System.EventHandler(this.btnInterfaceRefresh_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMoveItem,
            this.toolStripExecuteItem,
            this.toolStripCommandItem,
            this.toolStripSeparator1,
            this.toolStripRefreshItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(180, 114);
            // 
            // lstNetworkEntities
            // 
            this.lstNetworkEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstNetworkEntities.FullRowSelect = true;
            this.lstNetworkEntities.HideSelection = false;
            this.lstNetworkEntities.Location = new System.Drawing.Point(0, 52);
            listViewColumnSorter1.NeedNumberCompare = false;
            listViewColumnSorter1.Order = System.Windows.Forms.SortOrder.None;
            listViewColumnSorter1.SortColumn = 0;
            this.lstNetworkEntities.LvwColumnSorter = listViewColumnSorter1;
            this.lstNetworkEntities.Name = "lstNetworkEntities";
            this.lstNetworkEntities.Size = new System.Drawing.Size(800, 373);
            this.lstNetworkEntities.TabIndex = 9;
            this.lstNetworkEntities.UseCompatibleStateImageBehavior = false;
            this.lstNetworkEntities.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Interface";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.Width = 66;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ports";
            this.columnHeader3.Width = 359;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Shares";
            this.columnHeader4.Width = 136;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            this.columnHeader5.Width = 159;
            // 
            // FrmNetworkMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstNetworkEntities);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.cmbInterfaces);
            this.Controls.Add(this.btnInterfaceRefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNetworkMovement";
            this.Text = "Network Movement []";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetworkMovement_FormClosing);
            this.Load += new System.EventHandler(this.FrmNetworkMovement_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnPorts;
        private System.Windows.Forms.ColumnHeader columnAddresses;
        private System.Windows.Forms.ColumnHeader columnInterface;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgress;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ColumnHeader columnShares;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ComboBox cmbInterfaces;
        private System.Windows.Forms.ToolStripMenuItem toolStripRefreshItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripCommandItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripExecuteItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMoveItem;
        private System.Windows.Forms.Button btnInterfaceRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private Controls.AeroListView lstNetworkEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}