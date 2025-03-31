namespace Quasar.Server.Forms
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
            Quasar.Server.Utilities.ListViewColumnSorter listViewColumnSorter1 = new Quasar.Server.Utilities.ListViewColumnSorter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetworkMovement));
            this.lstNetworkEntities = new Quasar.Server.Controls.AeroListView();
            this.columnInterface = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAddresses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnShares = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExecuteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCommandItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRefreshItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstNetworkEntities
            // 
            this.lstNetworkEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnInterface,
            this.columnAddresses,
            this.columnPorts,
            this.columnShares});
            this.lstNetworkEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNetworkEntities.FullRowSelect = true;
            this.lstNetworkEntities.HideSelection = false;
            this.lstNetworkEntities.Location = new System.Drawing.Point(0, 0);
            listViewColumnSorter1.NeedNumberCompare = false;
            listViewColumnSorter1.Order = System.Windows.Forms.SortOrder.None;
            listViewColumnSorter1.SortColumn = 0;
            this.lstNetworkEntities.LvwColumnSorter = listViewColumnSorter1;
            this.lstNetworkEntities.Name = "lstNetworkEntities";
            this.lstNetworkEntities.Size = new System.Drawing.Size(766, 496);
            this.lstNetworkEntities.TabIndex = 0;
            this.lstNetworkEntities.UseCompatibleStateImageBehavior = false;
            this.lstNetworkEntities.View = System.Windows.Forms.View.Details;
            // 
            // columnInterface
            // 
            this.columnInterface.Text = "Interface";
            this.columnInterface.Width = 181;
            // 
            // columnAddresses
            // 
            this.columnAddresses.Text = "IP";
            this.columnAddresses.Width = 80;
            // 
            // columnShares
            // 
            this.columnShares.Text = "Shares";
            this.columnShares.Width = 131;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMoveItem,
            this.toolStripExecuteItem,
            this.toolStripCommandItem,
            this.toolStripSeparator1,
            this.toolStripRefreshItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(176, 98);
            // 
            // toolStripMoveItem
            // 
            this.toolStripMoveItem.Image = global::Quasar.Server.Properties.Resources.computer_go;
            this.toolStripMoveItem.Name = "toolStripMoveItem";
            this.toolStripMoveItem.Size = new System.Drawing.Size(175, 22);
            this.toolStripMoveItem.Text = "Move To";
            this.toolStripMoveItem.Click += new System.EventHandler(this.toolStripMoveItem_Click);
            // 
            // toolStripExecuteItem
            // 
            this.toolStripExecuteItem.Image = global::Quasar.Server.Properties.Resources.application_go;
            this.toolStripExecuteItem.Name = "toolStripExecuteItem";
            this.toolStripExecuteItem.Size = new System.Drawing.Size(175, 22);
            this.toolStripExecuteItem.Text = "Remote Execute";
            this.toolStripExecuteItem.Click += new System.EventHandler(this.toolStripExecuteItem_Click);
            // 
            // toolStripCommandItem
            // 
            this.toolStripCommandItem.Image = global::Quasar.Server.Properties.Resources.application_view_xp_terminal;
            this.toolStripCommandItem.Name = "toolStripCommandItem";
            this.toolStripCommandItem.Size = new System.Drawing.Size(175, 22);
            this.toolStripCommandItem.Text = "Remote Command";
            this.toolStripCommandItem.Click += new System.EventHandler(this.toolStripCommandItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // toolStripRefreshItem
            // 
            this.toolStripRefreshItem.Image = global::Quasar.Server.Properties.Resources.refresh;
            this.toolStripRefreshItem.Name = "toolStripRefreshItem";
            this.toolStripRefreshItem.Size = new System.Drawing.Size(175, 22);
            this.toolStripRefreshItem.Text = "Refresh";
            this.toolStripRefreshItem.Click += new System.EventHandler(this.toolStripRefreshItem_Click);
            // 
            // columnPorts
            // 
            this.columnPorts.Text = "Ports";
            this.columnPorts.Width = 370;
            // 
            // FrmNetworkMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 496);
            this.Controls.Add(this.lstNetworkEntities);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNetworkMovement";
            this.Text = "Network Movement []";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetworkMovement_FormClosing);
            this.Load += new System.EventHandler(this.FrmNetworkMovement_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AeroListView lstNetworkEntities;
        private System.Windows.Forms.ColumnHeader columnAddresses;
        private System.Windows.Forms.ColumnHeader columnShares;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMoveItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripExecuteItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripCommandItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripRefreshItem;
        private System.Windows.Forms.ColumnHeader columnInterface;
        private System.Windows.Forms.ColumnHeader columnPorts;
    }
}