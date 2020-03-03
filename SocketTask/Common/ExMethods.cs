using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTask.Common
{
    public static class ExMethods
    {
        /// <summary>
        /// 定位到文末
        /// </summary>
        /// <param name="textBox"></param>
        public static void HoldBottom(this TextBox textBox)
        {
            //设置焦点
            textBox.Focus();

            //选择文本位置
            textBox.Select(textBox.TextLength, 0);

            //滚动到该位置
            textBox.ScrollToCaret();
        }

        /// <summary>
        /// 获取Socket的终结点IP地址
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static string IPstr(this Socket socket)
        {
            return socket.RemoteEndPoint.ToString();
        }
    }
}
