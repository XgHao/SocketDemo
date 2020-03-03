﻿using SocketTask.Common;
using System;
using System.Collections.Generic;
using System.Data;
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
        private static readonly List<Socket> socketClientList = new List<Socket>();      //监听到的Socket集合
        private static CancellationTokenSource tokenSource = null;     //服务任务取消信号
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
                MessageBox.Show("请输入正确的IP地址", "提示");
                return;
            }
            if (!int.TryParse(txt_LocalPort.Text.Trim(), out int port))
            {
                MessageBox.Show("请输入正确的端口号", "提示");
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
            tokenSource = new CancellationTokenSource();
            tokenSource.Token.Register(() =>
            {
                if (socket == null)
                {
                    MessageBox.Show("服务已关闭");
                }
            });

            //开启线程-监听对象端口
            Task.Run(() =>
            {
                while (!tokenSource.IsCancellationRequested)
                {
                    //如果监听到一个客户端，则创建一个对应的新Socket
                    Socket socketClient = socket.Accept();

                    //将该对象存入字典集合
                    socketClientList.Add(socketClient);

                    //显示连接信息
                    lb_OnlineList.RefreshListWithInvoke(socketClient.IPstr(), MyEnum.AddOrRemove.Add);
                    txt_RecInfo.AddTextWithInvoke($"{socketClient.IPstr()}上线");
                    //开启新的线程，监听已连接的通讯
                    Task.Factory.StartNew(sc =>
                    {
                        Socket sclientobj = sc as Socket;

                        while (!tokenSource.IsCancellationRequested)
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
                    }, socketClient);
                }
            }, tokenSource.Token);

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

            //取消
            tokenSource.Cancel();
            //释放资源
            socket.Dispose();
            socket = null;
            foreach (var item in socketClientList)
            {
                item?.Dispose();
            }

            btn_Start.Enabled = true;
            txt_RecInfo.AddTextWithInvoke("+++++++++++++++++服务已关闭+++++++++++++++++");
        }
    }
}