namespace Quasar.Server.Controls
{
    partial class FrmNetworkMovementRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetworkMovementRequest));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAsAdmin = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfter = new System.Windows.Forms.CheckBox();
            this.cmbShares = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkUseShares = new System.Windows.Forms.CheckBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(103, 12);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(208, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(320, 47);
            this.chkShowPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(125, 20);
            this.chkShowPass.TabIndex = 2;
            this.chkShowPass.Text = "Show Password";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(103, 44);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(208, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // chkAsAdmin
            // 
            this.chkAsAdmin.AutoSize = true;
            this.chkAsAdmin.Location = new System.Drawing.Point(21, 82);
            this.chkAsAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAsAdmin.Name = "chkAsAdmin";
            this.chkAsAdmin.Size = new System.Drawing.Size(137, 20);
            this.chkAsAdmin.TabIndex = 5;
            this.chkAsAdmin.Text = "Execute As Admin";
            this.chkAsAdmin.UseVisualStyleBackColor = true;
            // 
            // chkDeleteAfter
            // 
            this.chkDeleteAfter.AutoSize = true;
            this.chkDeleteAfter.Location = new System.Drawing.Point(179, 82);
            this.chkDeleteAfter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDeleteAfter.Name = "chkDeleteAfter";
            this.chkDeleteAfter.Size = new System.Drawing.Size(99, 20);
            this.chkDeleteAfter.TabIndex = 6;
            this.chkDeleteAfter.Text = "Delete After";
            this.chkDeleteAfter.UseVisualStyleBackColor = true;
            // 
            // cmbShares
            // 
            this.cmbShares.FormattingEnabled = true;
            this.cmbShares.Location = new System.Drawing.Point(76, 111);
            this.cmbShares.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbShares.Name = "cmbShares";
            this.cmbShares.Size = new System.Drawing.Size(235, 24);
            this.cmbShares.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Share:";
            // 
            // chkUseShares
            // 
            this.chkUseShares.AutoSize = true;
            this.chkUseShares.Location = new System.Drawing.Point(320, 113);
            this.chkUseShares.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkUseShares.Name = "chkUseShares";
            this.chkUseShares.Size = new System.Drawing.Size(133, 20);
            this.chkUseShares.TabIndex = 9;
            this.chkUseShares.Text = "Use Share (SMB)";
            this.chkUseShares.UseVisualStyleBackColor = true;
            this.chkUseShares.CheckedChanged += new System.EventHandler(this.chkUseShares_CheckedChanged);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(101, 144);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(353, 22);
            this.txtInput.TabIndex = 11;
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(17, 148);
            this.labelAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(72, 16);
            this.labelAction.TabIndex = 10;
            this.labelAction.Text = "Command:";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(21, 176);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(435, 28);
            this.btnOpenFile.TabIndex = 12;
            this.btnOpenFile.Text = "Select File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(243, 212);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(213, 28);
            this.btnDone.TabIndex = 13;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(21, 212);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(213, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmNetworkMovementRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 247);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.chkUseShares);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbShares);
            this.Controls.Add(this.chkDeleteAfter);
            this.Controls.Add(this.chkAsAdmin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkShowPass);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmNetworkMovementRequest";
            this.Text = "Network Movement Request []";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetworkMovementRequest_FormClosing);
            this.Load += new System.EventHandler(this.FrmNetworkMovementRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAsAdmin;
        private System.Windows.Forms.CheckBox chkDeleteAfter;
        private System.Windows.Forms.ComboBox cmbShares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkUseShares;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
    }
}