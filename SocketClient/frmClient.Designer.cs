namespace SocketClient
{
    partial class FrmClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClient));
            this.btnBreak = new System.Windows.Forms.Button();
            this.txtMsgToServer = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbGuiyue = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnXML = new System.Windows.Forms.Button();
            this.chbHexI = new System.Windows.Forms.CheckBox();
            this.chbHexO = new System.Windows.Forms.CheckBox();
            this.转发系统 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ckbDebugMode = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBreak
            // 
            this.btnBreak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBreak.Enabled = false;
            this.btnBreak.Location = new System.Drawing.Point(750, 18);
            this.btnBreak.Name = "btnBreak";
            this.btnBreak.Size = new System.Drawing.Size(45, 23);
            this.btnBreak.TabIndex = 27;
            this.btnBreak.Text = "断开";
            this.btnBreak.UseVisualStyleBackColor = true;
            this.btnBreak.Click += new System.EventHandler(this.btnBreak_Click);
            // 
            // txtMsgToServer
            // 
            this.txtMsgToServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsgToServer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMsgToServer.Location = new System.Drawing.Point(179, 441);
            this.txtMsgToServer.MaxLength = 99999999;
            this.txtMsgToServer.Multiline = true;
            this.txtMsgToServer.Name = "txtMsgToServer";
            this.txtMsgToServer.Size = new System.Drawing.Size(531, 50);
            this.txtMsgToServer.TabIndex = 22;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(720, 441);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 50);
            this.btnSend.TabIndex = 21;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.BackColor = System.Drawing.Color.GhostWhite;
            this.txtContent.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContent.Location = new System.Drawing.Point(179, 53);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(616, 382);
            this.txtContent.TabIndex = 20;
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // btnConn
            // 
            this.btnConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConn.Location = new System.Drawing.Point(687, 18);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(57, 23);
            this.btnConn.TabIndex = 19;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(358, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "聊天记录";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbGuiyue);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 506);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器";
            // 
            // cmbGuiyue
            // 
            this.cmbGuiyue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGuiyue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbGuiyue.FormattingEnabled = true;
            this.cmbGuiyue.Location = new System.Drawing.Point(6, 20);
            this.cmbGuiyue.Name = "cmbGuiyue";
            this.cmbGuiyue.Size = new System.Drawing.Size(161, 24);
            this.cmbGuiyue.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(161, 436);
            this.listBox1.TabIndex = 0;
            // 
            // btnXML
            // 
            this.btnXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXML.Location = new System.Drawing.Point(624, 18);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(57, 23);
            this.btnXML.TabIndex = 19;
            this.btnXML.Text = "IP";
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // chbHexI
            // 
            this.chbHexI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbHexI.AutoSize = true;
            this.chbHexI.Checked = true;
            this.chbHexI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbHexI.Location = new System.Drawing.Point(447, 30);
            this.chbHexI.Name = "chbHexI";
            this.chbHexI.Size = new System.Drawing.Size(96, 16);
            this.chbHexI.TabIndex = 35;
            this.chbHexI.Text = "十六进制发送";
            this.chbHexI.UseVisualStyleBackColor = true;
            this.chbHexI.CheckedChanged += new System.EventHandler(this.chbHexI_CheckedChanged);
            // 
            // chbHexO
            // 
            this.chbHexO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbHexO.AutoSize = true;
            this.chbHexO.Checked = true;
            this.chbHexO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbHexO.Location = new System.Drawing.Point(447, 12);
            this.chbHexO.Name = "chbHexO";
            this.chbHexO.Size = new System.Drawing.Size(96, 16);
            this.chbHexO.TabIndex = 34;
            this.chbHexO.Text = "十六进制接受";
            this.chbHexO.UseVisualStyleBackColor = true;
            // 
            // 转发系统
            // 
            this.转发系统.Icon = ((System.Drawing.Icon)(resources.GetObject("转发系统.Icon")));
            this.转发系统.Text = "notifyIcon1";
            this.转发系统.Visible = true;
            this.转发系统.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // ckbDebugMode
            // 
            this.ckbDebugMode.AutoSize = true;
            this.ckbDebugMode.Checked = true;
            this.ckbDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDebugMode.Location = new System.Drawing.Point(179, 22);
            this.ckbDebugMode.Name = "ckbDebugMode";
            this.ckbDebugMode.Size = new System.Drawing.Size(72, 16);
            this.ckbDebugMode.TabIndex = 36;
            this.ckbDebugMode.Text = "调试模式";
            this.ckbDebugMode.UseVisualStyleBackColor = true;
            // 
            // FrmClient
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 506);
            this.Controls.Add(this.ckbDebugMode);
            this.Controls.Add(this.chbHexI);
            this.Controls.Add(this.chbHexO);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBreak);
            this.Controls.Add(this.txtMsgToServer);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnXML);
            this.Controls.Add(this.btnConn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "转发系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBreak;
        private System.Windows.Forms.TextBox txtMsgToServer;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnXML;
        private System.Windows.Forms.CheckBox chbHexI;
        private System.Windows.Forms.CheckBox chbHexO;
        private System.Windows.Forms.NotifyIcon 转发系统;
        private System.Windows.Forms.ComboBox cmbGuiyue;
        private System.Windows.Forms.CheckBox ckbDebugMode;
    }
}