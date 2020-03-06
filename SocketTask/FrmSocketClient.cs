using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

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

            if (socketClient == null)
            {
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

            //连接
            try
            {
                //获取终结点
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, int.Parse(txt_ServerPort.Text));

                txt_RecInfo.AddTextWithInvoke("连接中.....");
                socketClient.Connect(iPEndPoint);
            }
            catch (Exception ex)
            {
                txt_RecInfo.AddTextWithInvoke($"连接失败，{ex.Message}");
                return;
            }

            //连接成功
            if (socketClient.Connected)
            {
                txt_RecInfo.AddTextWithInvoke("+++++++++++++++++连接成功+++++++++++++++++");
                btn_Connect.Enabled = false;

                //监听任务取消信息
                CancellationTokenSource cancellationTS_Listening = new CancellationTokenSource();

                //监听消息
                Task.Factory.StartNew(() =>
                {
                    //数据缓冲区
                    byte[] arrMsg = new byte[Const.BufferByteSize];

                    //接受数据
                    int len = -1;
                    while (!cancellationTS_Listening.IsCancellationRequested)
                    {
                        try
                        {
                            len = socketClient.Receive(arrMsg);
                        }
                        catch (SocketException)
                        {
                            throw;
                        }
                        catch (ObjectDisposedException)
                        {
                            MessageBox.Show("连接已断开");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        if (len > 0)
                        {
                            //显示接受信息
                            txt_RecInfo.AddTextWithInvoke($"来自{socketClient.IPstr()}的消息：{Encoding.Default.GetString(arrMsg)}");
                        }
                        else
                        {
                            //服务器关闭
                            txt_RecInfo.AddTextWithInvoke("连接断开");
                            //监听取消
                            cancellationTS_Listening.Cancel();

                            socketClient.Disconnect(true);
                            socketClient.Dispose();
                        }
                    }

                }, cancellationTS_Listening.Token);
            }
        }
    }
}
