using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketTask.Common;

namespace SocketTask
{
    public partial class FrmSocketClient : Form
    {
        #region 全局变量
        Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        #endregion

        public FrmSocketClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体显示时，查询IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSocketClient_Shown(object sender, EventArgs e)
        {
            var localIP = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily.ToString().Equals("InterNetwork")).FirstOrDefault();
            txt_ServerIP.RefreshTextWithInvoke(localIP.ToString());
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(txt_ServerIP.Text.Trim(), out IPAddress iPAddress)) 
            {
                this.Hint(txt_ServerIP, "请输入正确的IP地址", MyEnum.ShowPosition.Mid_Right);
                return;
            }

            if (socketClient.Connected)  
            {
                MessageBox.Show("连接已存在");
                return;
            }

            //连接
            try
            {
                //获取终结点
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, int.Parse(txt_ServerPort.Text));

                txt_RecInfo.RefreshTextWithInvoke("连接中.....");
                socketClient.Connect(iPEndPoint);
            }
            catch (Exception ex)
            {
                txt_RecInfo.RefreshTextWithInvoke($"连接失败，{ex.Message}");
                socketClient.Close();
                return;
            }

            //连接成功
            if (socketClient.Connected)
            {
                txt_RecInfo.RefreshTextWithInvoke("+++++++++++++++++连接成功+++++++++++++++++");
                btn_Connect.Enabled = false;

                //监听消息
                Task.Factory.StartNew(sc =>
                {
                    Socket socket = sc as Socket;
                }, socketClient);
            }
        }
    }
}
