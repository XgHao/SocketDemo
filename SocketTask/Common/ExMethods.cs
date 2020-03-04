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

        /// <summary>
        /// 在任意A前插入n个B
        /// </summary>
        /// <param name="str">要操作字符串对象</param>
        /// <param name="A">目标</param>
        /// <param name="B">要插入的值</param>
        /// <param name="n">每次插入的次数</param>
        /// <returns></returns>
        public static string AdvancedInsert(this string str, string A, string B, int n = 1)
        {
            int head = 0, end = str.IndexOf(A);

            StringBuilder res = new StringBuilder();

            while (end >= 0) 
            {
                //找到“a”位置
                res.Append(str.Substring(head, end - head));
                //插入b
                for (int i = 0; i < n; i++)
                {
                    res.Append(B);
                }
                head = end;
                end = str.IndexOf(A, end + 1);
            }
            res.Append(str.Substring(head));

            return res.ToString();
        }
    }
}
