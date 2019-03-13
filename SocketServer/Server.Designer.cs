namespace SocketClient
{
    partial class Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMsgToClient = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnCloseServer = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chbHexO = new System.Windows.Forms.CheckBox();
            this.chbHexI = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(12, 48);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(536, 265);
            this.txtContent.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 21);
            this.label4.TabIndex = 28;
            this.label4.Text = "聊天记录";
            // 
            // txtMsgToClient
            // 
            this.txtMsgToClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMsgToClient.Location = new System.Drawing.Point(12, 321);
            this.txtMsgToClient.Multiline = true;
            this.txtMsgToClient.Name = "txtMsgToClient";
            this.txtMsgToClient.Size = new System.Drawing.Size(424, 32);
            this.txtMsgToClient.TabIndex = 27;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(460, 321);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(88, 32);
            this.btnSend.TabIndex = 26;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(191, 12);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(58, 21);
            this.txtPoint.TabIndex = 30;
            this.txtPoint.Text = "10010";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(112, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 31;
            this.label1.Text = "端口号";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(395, 11);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 22);
            this.btnStartServer.TabIndex = 32;
            this.btnStartServer.Text = "开启服务";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnCloseServer
            // 
            this.btnCloseServer.Enabled = false;
            this.btnCloseServer.Location = new System.Drawing.Point(473, 11);
            this.btnCloseServer.Name = "btnCloseServer";
            this.btnCloseServer.Size = new System.Drawing.Size(75, 23);
            this.btnCloseServer.TabIndex = 32;
            this.btnCloseServer.Text = "关闭服务";
            this.btnCloseServer.UseVisualStyleBackColor = true;
            this.btnCloseServer.Click += new System.EventHandler(this.btnCloseServer_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // chbHexO
            // 
            this.chbHexO.AutoSize = true;
            this.chbHexO.Location = new System.Drawing.Point(294, 6);
            this.chbHexO.Name = "chbHexO";
            this.chbHexO.Size = new System.Drawing.Size(96, 16);
            this.chbHexO.TabIndex = 33;
            this.chbHexO.Text = "十六进制接受";
            this.chbHexO.UseVisualStyleBackColor = true;
            // 
            // chbHexI
            // 
            this.chbHexI.AutoSize = true;
            this.chbHexI.Location = new System.Drawing.Point(294, 24);
            this.chbHexI.Name = "chbHexI";
            this.chbHexI.Size = new System.Drawing.Size(96, 16);
            this.chbHexI.TabIndex = 33;
            this.chbHexI.Text = "十六进制发送";
            this.chbHexI.UseVisualStyleBackColor = true;
            // 
            // Server
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 365);
            this.Controls.Add(this.chbHexI);
            this.Controls.Add(this.chbHexO);
            this.Controls.Add(this.btnCloseServer);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPoint);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMsgToClient);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMsgToClient;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnCloseServer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chbHexO;
        private System.Windows.Forms.CheckBox chbHexI;

    }
}

