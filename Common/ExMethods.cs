using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.MyEnum;

namespace Common
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

        /// <summary>
        /// 设置文本及颜色主题
        /// </summary>
        /// <param name="control"></param>
        /// <param name="text"></param>
        /// <param name=""></param>
        public static void SetTextWithTheme(this Control control, string text, ThemeColor theme = ThemeColor.Default)
        {
            control.ForeColor = GetThemeColor(theme);
            control.Text = text;
        }

        /// <summary>
        /// 主题色
        /// </summary>
        /// <param name="color"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static Color GetThemeColor(ThemeColor theme)
        {
            Color color;
            switch (theme)
            {
                case ThemeColor.Primary:
                    color = Color.FromArgb(0, 123, 255);
                    break;
                case ThemeColor.Secondary:
                    color = Color.FromArgb(108, 117, 125);
                    break;
                case ThemeColor.Success:
                    color = Color.FromArgb(40, 167, 69);
                    break;
                case ThemeColor.Danger:
                    color = Color.FromArgb(220, 53, 69);
                    break;
                case ThemeColor.Warning:
                    color = Color.FromArgb(245, 127, 23);
                    break;
                case ThemeColor.Info:
                    color = Color.FromArgb(23, 162, 184);
                    break;
                case ThemeColor.Light:
                    color = Color.FromArgb(248, 249, 250);
                    break;
                case ThemeColor.Dark:
                    color = Color.FromArgb(52, 58, 64);
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            return color;
        }

        /// <summary>
        /// 获取字节序列
        /// </summary>
        /// <param name="text"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] GetArrayByte(this string text,Format format = Format.Default)
        {
            byte[] arr = null;
            switch (format)
            {
                case Format.UTF8:
                    arr = Encoding.UTF8.GetBytes(text);
                    break;
                case Format.ASCII:
                    arr = Encoding.ASCII.GetBytes(text);
                    break;
                case Format.Default:
                    arr = Encoding.Default.GetBytes(text);
                    break;
                case Format.Unicode:
                    arr = Encoding.Unicode.GetBytes(text);
                    break;
                default:
                    arr = Encoding.Default.GetBytes(text);
                    break;
            }
            return arr;
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetArrayText(this byte[] bytes, Format format = Format.Default)
        {
            string arr = null;
            switch (format)
            {
                case Format.UTF8:
                    arr = Encoding.UTF8.GetString(bytes);
                    break;
                case Format.ASCII:
                    arr = Encoding.ASCII.GetString(bytes);
                    break;
                case Format.Default:
                    arr = Encoding.Default.GetString(bytes);
                    break;
                case Format.Unicode:
                    arr = Encoding.Unicode.GetString(bytes);
                    break;
                default:
                    arr = Encoding.Default.GetString(bytes);
                    break;
            }
            return arr;
        }

        /// <summary>
        /// 去除末尾空值
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static byte[] RemoveNull(this byte[] arr)
        {
            //从末尾开始去除连续的空值
            //len>1 忽略头部信息
            int len;
            for (len = arr.Length - 1; len > 1; len--)
            {
                if (arr[len] == 0 && arr[len - 1] != 0)
                    break;
            }

            return arr.Take(len).ToArray();
        }

        /// <summary>
        /// 设置筛选
        /// </summary>
        /// <param name="fileDialog"></param>
        /// <param name="filters">筛选集合</param>
        public static void SetFilter(this FileDialog fileDialog, string[] filters = null)
        {
            if (filters == null)
            {
                filters = Enum.GetNames(typeof(Extensions));
            }

            StringBuilder DialogFilter = new StringBuilder();
            foreach (var item in filters)
            {
                DialogFilter.Append($"{item} files (*.{item})|*.{item}|");
            }
            fileDialog.Filter = DialogFilter.ToString().TrimEnd('|');
        }
    }
}
