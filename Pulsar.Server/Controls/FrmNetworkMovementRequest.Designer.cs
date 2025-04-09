namespace Pulsar.Server.Controls
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.chkUseShares = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbShares = new System.Windows.Forms.ComboBox();
            this.chkDeleteAfter = new System.Windows.Forms.CheckBox();
            this.chkAsAdmin = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(11, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(177, 174);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(160, 23);
            this.btnDone.TabIndex = 28;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(11, 145);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(326, 23);
            this.btnOpenFile.TabIndex = 27;
            this.btnOpenFile.Text = "Select File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(71, 119);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(266, 20);
            this.txtInput.TabIndex = 26;
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(8, 122);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(57, 13);
            this.labelAction.TabIndex = 25;
            this.labelAction.Text = "Command:";
            // 
            // chkUseShares
            // 
            this.chkUseShares.AutoSize = true;
            this.chkUseShares.Location = new System.Drawing.Point(235, 94);
            this.chkUseShares.Name = "chkUseShares";
            this.chkUseShares.Size = new System.Drawing.Size(108, 17);
            this.chkUseShares.TabIndex = 24;
            this.chkUseShares.Text = "Use Share (SMB)";
            this.chkUseShares.UseVisualStyleBackColor = true;
            this.chkUseShares.CheckedChanged += new System.EventHandler(this.chkUseShares_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Share:";
            // 
            // cmbShares
            // 
            this.cmbShares.FormattingEnabled = true;
            this.cmbShares.Location = new System.Drawing.Point(52, 92);
            this.cmbShares.Name = "cmbShares";
            this.cmbShares.Size = new System.Drawing.Size(177, 21);
            this.cmbShares.TabIndex = 22;
            // 
            // chkDeleteAfter
            // 
            this.chkDeleteAfter.AutoSize = true;
            this.chkDeleteAfter.Location = new System.Drawing.Point(129, 69);
            this.chkDeleteAfter.Name = "chkDeleteAfter";
            this.chkDeleteAfter.Size = new System.Drawing.Size(82, 17);
            this.chkDeleteAfter.TabIndex = 21;
            this.chkDeleteAfter.Text = "Delete After";
            this.chkDeleteAfter.UseVisualStyleBackColor = true;
            // 
            // chkAsAdmin
            // 
            this.chkAsAdmin.AutoSize = true;
            this.chkAsAdmin.Location = new System.Drawing.Point(11, 69);
            this.chkAsAdmin.Name = "chkAsAdmin";
            this.chkAsAdmin.Size = new System.Drawing.Size(112, 17);
            this.chkAsAdmin.TabIndex = 20;
            this.chkAsAdmin.Text = "Execute As Admin";
            this.chkAsAdmin.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(72, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(157, 20);
            this.txtPassword.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password:";
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(235, 40);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(102, 17);
            this.chkShowPass.TabIndex = 17;
            this.chkShowPass.Text = "Show Password";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(72, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(157, 20);
            this.txtUsername.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Username:";
            // 
            // FrmNetworkMovementRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 201);
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
            this.Name = "FrmNetworkMovementRequest";
            this.Text = "Network Movement Request []";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetworkMovementRequest_FormClosing);
            this.Load += new System.EventHandler(this.FrmNetworkMovementRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.CheckBox chkUseShares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbShares;
        private System.Windows.Forms.CheckBox chkDeleteAfter;
        private System.Windows.Forms.CheckBox chkAsAdmin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
    }
}