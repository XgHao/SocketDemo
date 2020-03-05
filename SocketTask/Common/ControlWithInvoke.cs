using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SocketTask.Common.MyEnum;

namespace SocketTask.Common
{
    /// <summary>
    /// 刷新控件信息【跨线程】
    /// </summary>
    public static class ControlWithInvoke
    {
        /// <summary>
        /// 刷新文本
        /// </summary>
        /// <param name="control"></param>
        /// <param name="addtext"></param>
        public static void RefreshTextWithInvoke(this Control control,string addtext)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action<string>(txt =>
                {
                    control.Text = txt;
                }), addtext);
                return;
            }

            control.Text = addtext;
        }

        /// <summary>
        /// 刷新ListBox中的Item 
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="item"></param>
        /// <param name="AddOrRmv"></param>
        public static void RefreshListWithInvoke(this ListBox listBox,string item,AddOrRemove addOrRemove)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action<string, AddOrRemove>((s, f) =>
                {
                    if (addOrRemove == AddOrRemove.Add && !listBox.Items.Contains(s))
                        listBox.Items.Add(s);
                    else if (addOrRemove == AddOrRemove.Remove && listBox.Items.Contains(s))
                        listBox.Items.Remove(s);
                }), item, addOrRemove);
                return;
            }

            if (addOrRemove == AddOrRemove.Add && !listBox.Items.Contains(item))
                listBox.Items.Add(item);
            else if (addOrRemove == AddOrRemove.Remove && listBox.Items.Contains(item))
                listBox.Items.Remove(item);
        }


        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="txt"></param>
        public static void AddTextWithInvoke(this TextBox textBox,string txt)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<string>(t =>
                {
                    textBox.AppendText($"[{DateTime.Now}]{Environment.NewLine}{t}{Environment.NewLine}{Environment.NewLine}");
                }), txt);
                return;
            }
            
            textBox.AppendText($"[{DateTime.Now}]{Environment.NewLine}{txt}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
