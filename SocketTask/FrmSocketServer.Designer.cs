namespace SocketTask
{
    partial class FrmSocketServer
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
            this.Lbl_FilePath = new System.Windows.Forms.Label();
            this.btn_SendFile = new System.Windows.Forms.Button();
            this.btn_ChooseFile = new System.Windows.Forms.Button();
            this.txt_Sender = new System.Windows.Forms.TextBox();
            this.txt_RecInfo = new System.Windows.Forms.TextBox();
            this.lb_OnlineList = new System.Windows.Forms.ListBox();
            this.btn_OpenClient = new System.Windows.Forms.Button();
            this.btn_Sender = new System.Windows.Forms.Button();
            this.btn_Broke = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_LocalPort = new System.Windows.Forms.TextBox();
            this.txt_PublicIP = new System.Windows.Forms.TextBox();
            this.txt_LocalIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_PublicIP = new System.Windows.Forms.Label();
            this.lbl_LocalPort = new System.Windows.Forms.Label();
            this.lbl_LocalIP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbl_FilePath
            // 
            this.Lbl_FilePath.AutoSize = true;
            this.Lbl_FilePath.Location = new System.Drawing.Point(123, 439);
            this.Lbl_FilePath.Name = "Lbl_FilePath";
            this.Lbl_FilePath.Size = new System.Drawing.Size(0, 12);
            this.Lbl_FilePath.TabIndex = 27;
            // 
            // btn_SendFile
            // 
            this.btn_SendFile.Location = new System.Drawing.Point(477, 434);
            this.btn_SendFile.Name = "btn_SendFile";
            this.btn_SendFile.Size = new System.Drawing.Size(75, 23);
            this.btn_SendFile.TabIndex = 26;
            this.btn_SendFile.Text = "发送文件";
            this.btn_SendFile.UseVisualStyleBackColor = true;
            // 
            // btn_ChooseFile
            // 
            this.btn_ChooseFile.Location = new System.Drawing.Point(42, 434);
            this.btn_ChooseFile.Name = "btn_ChooseFile";
            this.btn_ChooseFile.Size = new System.Drawing.Size(75, 23);
            this.btn_ChooseFile.TabIndex = 25;
            this.btn_ChooseFile.Text = "选择文件";
            this.btn_ChooseFile.UseVisualStyleBackColor = true;
            // 
            // txt_Sender
            // 
            this.txt_Sender.Location = new System.Drawing.Point(28, 212);
            this.txt_Sender.Multiline = true;
            this.txt_Sender.Name = "txt_Sender";
            this.txt_Sender.Size = new System.Drawing.Size(445, 166);
            this.txt_Sender.TabIndex = 23;
            // 
            // txt_RecInfo
            // 
            this.txt_RecInfo.Location = new System.Drawing.Point(28, 25);
            this.txt_RecInfo.Multiline = true;
            this.txt_RecInfo.Name = "txt_RecInfo";
            this.txt_RecInfo.ReadOnly = true;
            this.txt_RecInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_RecInfo.Size = new System.Drawing.Size(445, 166);
            this.txt_RecInfo.TabIndex = 24;
            // 
            // lb_OnlineList
            // 
            this.lb_OnlineList.FormattingEnabled = true;
            this.lb_OnlineList.ItemHeight = 12;
            this.lb_OnlineList.Location = new System.Drawing.Point(589, 202);
            this.lb_OnlineList.Name = "lb_OnlineList";
            this.lb_OnlineList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lb_OnlineList.Size = new System.Drawing.Size(249, 100);
            this.lb_OnlineList.TabIndex = 22;
            // 
            // btn_OpenClient
            // 
            this.btn_OpenClient.Location = new System.Drawing.Point(666, 451);
            this.btn_OpenClient.Name = "btn_OpenClient";
            this.btn_OpenClient.Size = new System.Drawing.Size(75, 23);
            this.btn_OpenClient.TabIndex = 20;
            this.btn_OpenClient.Text = "打开客户端";
            this.btn_OpenClient.UseVisualStyleBackColor = false;
            // 
            // btn_Sender
            // 
            this.btn_Sender.Enabled = false;
            this.btn_Sender.Location = new System.Drawing.Point(666, 395);
            this.btn_Sender.Name = "btn_Sender";
            this.btn_Sender.Size = new System.Drawing.Size(75, 23);
            this.btn_Sender.TabIndex = 21;
            this.btn_Sender.Text = "发送消息";
            this.btn_Sender.UseVisualStyleBackColor = true;
            // 
            // btn_Broke
            // 
            this.btn_Broke.Location = new System.Drawing.Point(666, 366);
            this.btn_Broke.Name = "btn_Broke";
            this.btn_Broke.Size = new System.Drawing.Size(75, 23);
            this.btn_Broke.TabIndex = 19;
            this.btn_Broke.Text = "关闭服务";
            this.btn_Broke.UseVisualStyleBackColor = true;
            this.btn_Broke.Click += new System.EventHandler(this.Btn_Broke_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(666, 337);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 18;
            this.btn_Start.Text = "启动服务";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // txt_LocalPort
            // 
            this.txt_LocalPort.Location = new System.Drawing.Point(694, 127);
            this.txt_LocalPort.Name = "txt_LocalPort";
            this.txt_LocalPort.Size = new System.Drawing.Size(144, 21);
            this.txt_LocalPort.TabIndex = 17;
            this.txt_LocalPort.Text = "1232";
            // 
            // txt_PublicIP
            // 
            this.txt_PublicIP.Location = new System.Drawing.Point(694, 81);
            this.txt_PublicIP.Name = "txt_PublicIP";
            this.txt_PublicIP.ReadOnly = true;
            this.txt_PublicIP.Size = new System.Drawing.Size(144, 21);
            this.txt_PublicIP.TabIndex = 16;
            this.txt_PublicIP.Text = "正在努力获取中...";
            // 
            // txt_LocalIP
            // 
            this.txt_LocalIP.Location = new System.Drawing.Point(694, 37);
            this.txt_LocalIP.Name = "txt_LocalIP";
            this.txt_LocalIP.ReadOnly = true;
            this.txt_LocalIP.Size = new System.Drawing.Size(144, 21);
            this.txt_LocalIP.TabIndex = 15;
            this.txt_LocalIP.Text = "正在努力获取中...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(598, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "在线列表：";
            // 
            // lbl_PublicIP
            // 
            this.lbl_PublicIP.AutoSize = true;
            this.lbl_PublicIP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PublicIP.Location = new System.Drawing.Point(585, 82);
            this.lbl_PublicIP.Name = "lbl_PublicIP";
            this.lbl_PublicIP.Size = new System.Drawing.Size(106, 22);
            this.lbl_PublicIP.TabIndex = 12;
            this.lbl_PublicIP.Text = "本机公网IP：";
            // 
            // lbl_LocalPort
            // 
            this.lbl_LocalPort.AutoSize = true;
            this.lbl_LocalPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LocalPort.Location = new System.Drawing.Point(601, 128);
            this.lbl_LocalPort.Name = "lbl_LocalPort";
            this.lbl_LocalPort.Size = new System.Drawing.Size(90, 22);
            this.lbl_LocalPort.TabIndex = 14;
            this.lbl_LocalPort.Text = "本机端口：";
            // 
            // lbl_LocalIP
            // 
            this.lbl_LocalIP.AutoSize = true;
            this.lbl_LocalIP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LocalIP.Location = new System.Drawing.Point(585, 38);
            this.lbl_LocalIP.Name = "lbl_LocalIP";
            this.lbl_LocalIP.Size = new System.Drawing.Size(106, 22);
            this.lbl_LocalIP.TabIndex = 11;
            this.lbl_LocalIP.Text = "本机内网IP：";
            // 
            // FrmSocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 521);
            this.Controls.Add(this.Lbl_FilePath);
            this.Controls.Add(this.btn_SendFile);
            this.Controls.Add(this.btn_ChooseFile);
            this.Controls.Add(this.txt_Sender);
            this.Controls.Add(this.txt_RecInfo);
            this.Controls.Add(this.lb_OnlineList);
            this.Controls.Add(this.btn_OpenClient);
            this.Controls.Add(this.btn_Sender);
            this.Controls.Add(this.btn_Broke);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txt_LocalPort);
            this.Controls.Add(this.txt_PublicIP);
            this.Controls.Add(this.txt_LocalIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_PublicIP);
            this.Controls.Add(this.lbl_LocalPort);
            this.Controls.Add(this.lbl_LocalIP);
            this.Name = "FrmSocketServer";
            this.Text = "FrmSocketServer";
            this.Shown += new System.EventHandler(this.FrmSocketServer_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_FilePath;
        private System.Windows.Forms.Button btn_SendFile;
        private System.Windows.Forms.Button btn_ChooseFile;
        private System.Windows.Forms.TextBox txt_Sender;
        private System.Windows.Forms.TextBox txt_RecInfo;
        private System.Windows.Forms.ListBox lb_OnlineList;
        private System.Windows.Forms.Button btn_OpenClient;
        private System.Windows.Forms.Button btn_Sender;
        private System.Windows.Forms.Button btn_Broke;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txt_LocalPort;
        private System.Windows.Forms.TextBox txt_PublicIP;
        private System.Windows.Forms.TextBox txt_LocalIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_PublicIP;
        private System.Windows.Forms.Label lbl_LocalPort;
        private System.Windows.Forms.Label lbl_LocalIP;
    }
}