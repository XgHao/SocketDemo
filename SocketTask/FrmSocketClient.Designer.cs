namespace SocketTask
{
    partial class FrmSocketClient
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
            this.txt_Sender = new System.Windows.Forms.TextBox();
            this.txt_RecInfo = new System.Windows.Forms.TextBox();
            this.btn_Sender = new System.Windows.Forms.Button();
            this.btn_Broke = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txt_ServerPort = new System.Windows.Forms.TextBox();
            this.txt_ServerIP = new System.Windows.Forms.TextBox();
            this.Lbl_ServerPort = new System.Windows.Forms.Label();
            this.Lbl_ServerIP = new System.Windows.Forms.Label();
            this.cmb_Encoding = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_Sender
            // 
            this.txt_Sender.Location = new System.Drawing.Point(48, 238);
            this.txt_Sender.Multiline = true;
            this.txt_Sender.Name = "txt_Sender";
            this.txt_Sender.Size = new System.Drawing.Size(445, 166);
            this.txt_Sender.TabIndex = 33;
            // 
            // txt_RecInfo
            // 
            this.txt_RecInfo.Location = new System.Drawing.Point(48, 51);
            this.txt_RecInfo.Multiline = true;
            this.txt_RecInfo.Name = "txt_RecInfo";
            this.txt_RecInfo.ReadOnly = true;
            this.txt_RecInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_RecInfo.Size = new System.Drawing.Size(445, 166);
            this.txt_RecInfo.TabIndex = 34;
            // 
            // btn_Sender
            // 
            this.btn_Sender.Location = new System.Drawing.Point(696, 381);
            this.btn_Sender.Name = "btn_Sender";
            this.btn_Sender.Size = new System.Drawing.Size(75, 23);
            this.btn_Sender.TabIndex = 30;
            this.btn_Sender.Text = "发送消息";
            this.btn_Sender.UseVisualStyleBackColor = true;
            this.btn_Sender.Click += new System.EventHandler(this.Btn_Sender_Click);
            // 
            // btn_Broke
            // 
            this.btn_Broke.Location = new System.Drawing.Point(696, 283);
            this.btn_Broke.Name = "btn_Broke";
            this.btn_Broke.Size = new System.Drawing.Size(75, 23);
            this.btn_Broke.TabIndex = 31;
            this.btn_Broke.Text = "断开服务器";
            this.btn_Broke.UseVisualStyleBackColor = true;
            this.btn_Broke.Click += new System.EventHandler(this.Btn_Broke_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(696, 236);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 32;
            this.btn_Connect.Text = "连接服务器";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // txt_ServerPort
            // 
            this.txt_ServerPort.Location = new System.Drawing.Point(714, 152);
            this.txt_ServerPort.Name = "txt_ServerPort";
            this.txt_ServerPort.Size = new System.Drawing.Size(144, 21);
            this.txt_ServerPort.TabIndex = 28;
            this.txt_ServerPort.Text = "1232";
            // 
            // txt_ServerIP
            // 
            this.txt_ServerIP.Location = new System.Drawing.Point(714, 63);
            this.txt_ServerIP.Name = "txt_ServerIP";
            this.txt_ServerIP.Size = new System.Drawing.Size(144, 21);
            this.txt_ServerIP.TabIndex = 29;
            // 
            // Lbl_ServerPort
            // 
            this.Lbl_ServerPort.AutoSize = true;
            this.Lbl_ServerPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lbl_ServerPort.Location = new System.Drawing.Point(621, 152);
            this.Lbl_ServerPort.Name = "Lbl_ServerPort";
            this.Lbl_ServerPort.Size = new System.Drawing.Size(106, 22);
            this.Lbl_ServerPort.TabIndex = 26;
            this.Lbl_ServerPort.Text = "服务器端口：";
            // 
            // Lbl_ServerIP
            // 
            this.Lbl_ServerIP.AutoSize = true;
            this.Lbl_ServerIP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lbl_ServerIP.Location = new System.Drawing.Point(637, 62);
            this.Lbl_ServerIP.Name = "Lbl_ServerIP";
            this.Lbl_ServerIP.Size = new System.Drawing.Size(90, 22);
            this.Lbl_ServerIP.TabIndex = 27;
            this.Lbl_ServerIP.Text = "服务器IP：";
            // 
            // cmb_Encoding
            // 
            this.cmb_Encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Encoding.FormattingEnabled = true;
            this.cmb_Encoding.Location = new System.Drawing.Point(48, 421);
            this.cmb_Encoding.Name = "cmb_Encoding";
            this.cmb_Encoding.Size = new System.Drawing.Size(121, 20);
            this.cmb_Encoding.TabIndex = 35;
            // 
            // FrmSocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 494);
            this.Controls.Add(this.cmb_Encoding);
            this.Controls.Add(this.txt_Sender);
            this.Controls.Add(this.txt_RecInfo);
            this.Controls.Add(this.btn_Sender);
            this.Controls.Add(this.btn_Broke);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txt_ServerPort);
            this.Controls.Add(this.txt_ServerIP);
            this.Controls.Add(this.Lbl_ServerPort);
            this.Controls.Add(this.Lbl_ServerIP);
            this.Name = "FrmSocketClient";
            this.Text = "FrmSocketClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSocketClient_FormClosed);
            this.Shown += new System.EventHandler(this.FrmSocketClient_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Sender;
        private System.Windows.Forms.TextBox txt_RecInfo;
        private System.Windows.Forms.Button btn_Sender;
        private System.Windows.Forms.Button btn_Broke;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txt_ServerPort;
        private System.Windows.Forms.TextBox txt_ServerIP;
        private System.Windows.Forms.Label Lbl_ServerPort;
        private System.Windows.Forms.Label Lbl_ServerIP;
        private System.Windows.Forms.ComboBox cmb_Encoding;
    }
}