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
    public partial class FrmSocketServer : Form
    {
        #region 全局变量
        Socket socket = null;   //服务端用于监听的Socket
        private List<Socket> sockets = new List<Socket>();      //监听到的Socket集合
        #endregion

        public FrmSocketServer()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 开启服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (socket != null) 
            {
                MessageBox.Show("服务器已开启", "提示");
                return;
            }

            //创建负责监听的套接字
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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

            //根据IP地址和端口号生成IPEndPoint对象
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

            try
            {
                socket.Bind(iPEndPoint);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "连接失败");
                socket = null;
                return;
            }

            //开启监听
            socket.Listen(1);

            //开启线程-监听对象端口
            Task.Run(() =>
            {
                while (true)
                {
                    //如果监听到一个客户端，则创建一个对应的新Socket
                    Socket socketClient = socket.Accept();

                    //将该对象存入字典集合
                    sockets.Add(socketClient);

                    //显示连接信息
                    lb_OnlineList.RefreshListWithInvoke(socketClient.RemoteEndPoint.ToString(), MyEnum.AddOrRemove.Add);
                    txt_RecInfo.AddTextWithInvoke($"{socketClient.RemoteEndPoint.ToString()}上线了！");

                    //开启新的线程，监听已连接的通讯
                    Task.Factory.StartNew(sc =>
                    {
                        Socket sclient = sc as Socket;
                        while (true)
                        {
                            //定一个缓冲区-用于接收数据
                            byte[] arrMsgRec = new byte[Const.BufferByteSize];
                        }
                    }, socketClient, TaskCreationOptions.None);
                }
            });
        } 
    }
}
