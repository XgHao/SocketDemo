using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using SocketTask.Helper;
using static Common.MyEnum;

namespace SocketTask
{
    public partial class FrmSocketClient : Form
    {
        #region 全局变量
        Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //监听任务取消信息
        CancellationTokenSource cancellationTS_Listening = null;
        #endregion

        public FrmSocketClient()
        {
            InitializeComponent();

            //初始化控件
            cmb_Encoding.DataSource = Enum.GetNames(typeof(Format));
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
            cancellationTS_Listening = new CancellationTokenSource();
            if (!IPAddress.TryParse(txt_ServerIP.Text.Trim(), out IPAddress iPAddress)) 
            {
                this.Hint(txt_ServerIP, "请输入正确的IP地址", ShowPosition.Mid_Right);
                return;
            }

            if (socketClient == null)
            {
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

                
                //监听消息
                Task.Factory.StartNew(() =>
                {
                    //接受数据
                    int len = -1;
                    while (!cancellationTS_Listening.IsCancellationRequested)
                    {
                        //数据缓冲区
                        byte[] arrMsg = new byte[Const.BufferByteSize];

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
                            Format format = GetEnumByIDorName<Format>(cmb_Encoding.GetSelectedItemWithInvoke());
                            var context = FormatHelper.StreamToText(arrMsg.RemoveNull(), format);
                            string msg = string.Empty;
                            if (context is string)
                            {
                                msg = context.ToString();
                            }
                            else if (context is byte[])
                            {
                                msg = $"接收到文件，未保存";
                                //保存文件
                                if (MessageBox.Show("接收到文件，是否保存？", "文件保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //保存文件
                                    msg = this.ShowSaveFileDialogWithInvoke(context as byte[]);
                                }
                                //反馈给服务端发送结果信息
                                Task.Factory.StartNew(m =>
                                {
                                    socketClient.Send(FormatHelper.TextToStream(m.ToString(), GetEnumByIDorName<Format>(cmb_Encoding.GetSelectedItemWithInvoke())));
                                }, $"发送成功，对方【{msg}】");
                            }
                            txt_RecInfo.AddTextWithInvoke(msg);
;                        }
                        else
                        {
                            //服务器关闭
                            txt_RecInfo.AddTextWithInvoke("连接断开");
                            //监听取消
                            cancellationTS_Listening.Cancel();

                            socketClient.Shutdown(SocketShutdown.Both);
                            socketClient.Disconnect(false);
                            socketClient.Dispose();
                        }
                    }

                }, cancellationTS_Listening.Token);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sender_Click(object sender, EventArgs e)
        {
            if (txt_Sender.Text.Length == 0)
                return;
            byte[] arrMsg = FormatHelper.TextToStream(txt_Sender.Text, GetEnumByIDorName<Format>(cmb_Encoding.GetSelectedItemWithInvoke()));
            socketClient.Send(arrMsg);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Broke_Click(object sender, EventArgs e)
        {
            //断开连接
            cancellationTS_Listening.Cancel();
            Task.Run(() =>
            {
                socketClient.Disconnect(false);
                socketClient.Dispose();
                socketClient = null;
            });
            btn_Connect.Enabled = true;
        }

        /// <summary>
        /// 客户端关闭时，发送断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSocketClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Btn_Broke_Click(sender, null);
        }
    }
}
