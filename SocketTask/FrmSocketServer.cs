using SocketTask.Common;
using System;
using System.Collections.Generic;
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

namespace SocketTask
{
    public partial class FrmSocketServer : Form
    {
        public FrmSocketServer()
        {
            InitializeComponent();
        }


        #region 全局变量
        private static Socket socket = null;   //服务端用于监听的Socket
        private static CancellationTokenSource CancellationTS_Server = new CancellationTokenSource();     //服务总任务取消信号
        private static readonly Dictionary<Socket, CancellationTokenSource> socketClientList = new Dictionary<Socket, CancellationTokenSource>();   //客户端Socket与对应取消信号字典集合
        #endregion

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (socket != null)
            {
                MessageBox.Show("服务正在运行", "提示");
                return;
            }


            //验证IP地址和端口号
            if (!IPAddress.TryParse(txt_LocalIP.Text.Trim(), out IPAddress iPAddress))
            {
                this.Hint(txt_LocalIP, "请输入正确的IP地址", MyEnum.ShowPosition.Mid_Right);
                return;
            }
            if (!int.TryParse(txt_LocalPort.Text.Trim(), out int port))
            {   
                this.Hint(txt_LocalPort, "请输入正确的端口号", MyEnum.ShowPosition.Mid_Right);
                return;
            }

            //创建实例
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //根据IP地址和端口号生成IPEndPoint对象
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

                //绑定
                socket.Bind(iPEndPoint);

                //开启监听
                socket.Listen(10);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "连接失败");
                socket = null;
                return;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"端口参数超界，{ex.Message}");
                socket = null;
                return;
            }


            //开启任务-监听
            //任务类型[LongRunning]
            CancellationTS_Server.Token.Register(() =>
            {
                if (socket == null)
                {
                    MessageBox.Show("服务已关闭");
                }
            });

            //开启线程-监听对象端口
            Task.Run(() =>
            {
                while (!CancellationTS_Server.IsCancellationRequested)
                {
                    //如果监听到一个客户端，则创建一个对应的新Socket以及对应取消信号
                    Socket socketClient = socket.Accept();
                    CancellationTokenSource CancellationTS_Client = new CancellationTokenSource();

                    //将该对象存入字典集合
                    socketClientList.Add(socketClient, CancellationTS_Client);

                    //显示连接信息
                    lb_OnlineList.RefreshListWithInvoke(socketClient.IPstr(), MyEnum.AddOrRemove.Add);
                    txt_RecInfo.AddTextWithInvoke($"{socketClient.IPstr()}上线");
                    //开启新的线程，监听已连接的通讯
                    Task.Factory.StartNew(sc =>
                    {
                        Socket sclientobj = sc as Socket;

                        while (!socketClientList[sclientobj].IsCancellationRequested)
                        {
                            //定一个缓冲区-用于接收数据
                            byte[] arrMsgRec = new byte[Const.BufferByteSize];

                            //根据收到返回的字节数判断是否断开连接
                            int len = -1;
                            try
                            {
                                //接收
                                len = sclientobj.Receive(arrMsgRec);
                            }
                            catch (SocketException)
                            {
                                lb_OnlineList.RefreshListWithInvoke(sclientobj.IPstr(), MyEnum.AddOrRemove.Remove);
                                txt_RecInfo.AddTextWithInvoke($"{sclientobj.IPstr()}离线");
                                break;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                break;
                            }

                            if (len > 0)
                            {
                                //显示接收数据
                                txt_RecInfo.AddTextWithInvoke($"来自{sclientobj.IPstr()}的消息：{Encoding.Default.GetString(arrMsgRec, 0, len)}");
                            }
                            else
                            {
                                //客户端离线
                                lb_OnlineList.RefreshListWithInvoke(sclientobj.RemoteEndPoint.ToString(), MyEnum.AddOrRemove.Remove);
                                txt_RecInfo.AddTextWithInvoke($"{sclientobj.IPstr()}离线");
                                break;
                            }
                        }
                    }, socketClient, CancellationTS_Client.Token);
                }
            }, CancellationTS_Server.Token);

            //按钮禁用
            btn_Start.Enabled = false;
            txt_RecInfo.AddTextWithInvoke($"+++++++++++++++++服务开启成功+++++++++++++++++");
        }

        /// <summary>
        /// 窗体加载完毕后，获取内网及公网IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSocketServer_Shown(object sender, EventArgs e)
        {
            IPAddress publicIP = null, localIP = null;
            
            //获取公网IP
            Task.Run(() =>
            {
                Stream stream = null;
                StreamReader streamReader = null;
                try
                {
                    stream = WebRequest.Create("https://www.ipip5.com/").GetResponse().GetResponseStream();
                    streamReader = new StreamReader(stream, Encoding.UTF8);
                    var str = streamReader.ReadToEnd();
                    int first = str.IndexOf("<span class=\"c-ip\">") + 19;
                    int last = str.IndexOf("</span>", first);
                    var ip = str.Substring(first, last - first);
                    publicIP = IPAddress.Parse(ip);
                    txt_PublicIP.RefreshTextWithInvoke(publicIP.ToString());
                }
                catch (Exception ex)
                {
                    txt_PublicIP.RefreshTextWithInvoke($"获取失败,{ex.Message}");
                }
                finally
                {
                    streamReader?.Dispose();
                    stream?.Dispose();
                }
            });

            //获取内网IP
            Task.Run(() =>
            {
                localIP = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily.ToString().Equals("InterNetwork")).FirstOrDefault();
                txt_LocalIP.RefreshTextWithInvoke(localIP.ToString());
            });
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Broke_Click(object sender, EventArgs e)
        {
            if (socket == null) 
            {
                MessageBox.Show("服务未开启");
                return;
            }

            //服务取消信号
            CancellationTS_Server.Cancel();
            
            //释放资源
            socket.Dispose();
            socket = null;
            //释放已连接的socket资源
            foreach (var item in socketClientList)
            {
                //已连接的连接取消信号，Socket释放
                item.Value?.Cancel();
                item.Key?.Dispose();
            }
            //清除ListBox
            lb_OnlineList.Items.Clear();

            btn_Start.Enabled = true;
            txt_RecInfo.AddTextWithInvoke("+++++++++++++++++服务已关闭+++++++++++++++++");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sender_Click(object sender, EventArgs e)
        {
            if (txt_Sender.Text.Length <= 0) 
            {
                this.Hint(txt_Sender, "消息不能为空", MyEnum.ShowPosition.Mid_Mid);
                return;
            }

            if (lb_OnlineList.SelectedItems.Count == 0) 
            {
                this.Hint(lb_OnlineList, "至少选择一个发送对象", MyEnum.ShowPosition.Bottom_Mid);
                return;
            }

            //转换信息格式
            byte[] arrMsg = Encoding.Default.GetBytes(txt_Sender.Text);

            //遍历所有选中客户端并发送
            foreach (var item in lb_OnlineList.SelectedItems)
            {
                //获取对应的Socket对象
                Socket socket = socketClientList.Keys.Where(s => s.RemoteEndPoint.ToString().Equals(item.ToString())).FirstOrDefault();
                socket?.Send(arrMsg);
            }

            txt_Sender.Clear();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_All_CheckedChanged(object sender, EventArgs e)
        {
            //全选
            if (cb_All.Checked)
            {
                for (int i = 0; i < lb_OnlineList.Items.Count; i++)
                {
                    lb_OnlineList.SetSelected(i, true);
                }
                return;
            }
            lb_OnlineList.ClearSelected();
        }

        /// <summary>
        /// 监听消息框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Sender_TextChanged(object sender, EventArgs e) => btn_Sender.Enabled = txt_Sender.Text?.Length > 0;

        /// <summary>
        /// 监听换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Sender_KeyUp(object sender, KeyEventArgs e)
        {
            //Shift+Enter换行
            //发送
            if (!((e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter) || e.KeyCode != Keys.Enter))
            {
                //取消回车符
                var str = txt_Sender.Text;
                if (str.EndsWith(Environment.NewLine)) 
                {
                    txt_Sender.Text = str.Substring(0, str.Length - Environment.NewLine.Length);
                    Btn_Sender_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Broken_Click(object sender, EventArgs e)
        {
            if (lb_OnlineList.SelectedItems.Count < 1)  
            {
                this.Hint(lb_OnlineList, "请选择你要断开的连接", MyEnum.ShowPosition.Mid_Mid);
                return;
            }

            //获取要断开Socket的集合
            List<Socket> selectedSockets = socketClientList.Keys.Where(s => s.RemoteEndPoint != null && lb_OnlineList.SelectedItems.Contains(s.RemoteEndPoint.ToString())).ToList();

            //断开连接
            foreach (var item in selectedSockets)
            {
                //取消信号，移除字典与ListBox，断开连接，释放资源
                socketClientList[item].Cancel();
                socketClientList.Remove(item);
                lb_OnlineList.Items.Remove(item.RemoteEndPoint.ToString());
                txt_RecInfo.AddTextWithInvoke($"主动断开与{item.RemoteEndPoint}的连接");

                item.Disconnect(true);
                item.Dispose();

            }
        }
    }
}
