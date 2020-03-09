using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.MyEnum;

namespace Common
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
        /// <param name="invoke"></param>
        public static void RefreshTextWithInvoke(this Control control, string addtext, InvokeType invoke = InvokeType.Invoke)
        {
            //是否需要调用Invoke方法
            if (control.InvokeRequired)
            {
                switch (invoke)
                {
                    //同步委托
                    case InvokeType.Invoke:
                        control.Invoke(new Action<string>(txt =>
                        {
                            control.Text = txt;
                        }), addtext);
                        break;
                    //异步委托
                    case InvokeType.BeginInvoke:
                        control.BeginInvoke(new Action<string>(txt =>
                        {
                            control.Text = txt;
                        }), addtext);
                        break;
                }
                return;
            }

            control.Text = addtext;
        }

        /// <summary>
        /// 刷新ListBox中的Items
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="item"></param>
        /// <param name="addOrRemove"></param>
        /// <param name="invoke"></param>
        public static void RefreshListWithInvoke(this ListBox listBox, string item, AddOrRemove addOrRemove, InvokeType invoke = InvokeType.Invoke)
        {
            if (listBox.InvokeRequired)
            {
                switch (invoke)
                {
                    case InvokeType.Invoke:
                        listBox.Invoke(new Action<string, AddOrRemove>((s, f) =>
                        {
                            if (addOrRemove == AddOrRemove.Add && !listBox.Items.Contains(s))
                                listBox.Items.Add(s);
                            else if (addOrRemove == AddOrRemove.Remove && listBox.Items.Contains(s))
                                listBox.Items.Remove(s);
                        }), item, addOrRemove);
                        break;
                    case InvokeType.BeginInvoke:
                        listBox.BeginInvoke(new Action<string, AddOrRemove>((s, f) =>
                        {
                            if (addOrRemove == AddOrRemove.Add && !listBox.Items.Contains(s))
                                listBox.Items.Add(s);
                            else if (addOrRemove == AddOrRemove.Remove && listBox.Items.Contains(s))
                                listBox.Items.Remove(s);
                        }), item, addOrRemove);
                        break;
                }
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
        /// <param name="invoke"></param>
        public static void AddTextWithInvoke(this TextBox textBox, string txt, InvokeType invoke = InvokeType.Invoke)
        {
            if (textBox.InvokeRequired)
            {
                switch (invoke)
                {
                    case InvokeType.Invoke:
                        textBox.Invoke(new Action<string>(t =>
                        {
                            textBox.AppendText($"[{DateTime.Now}]{Environment.NewLine}{t}{Environment.NewLine}{Environment.NewLine}");
                        }), txt);
                        break;
                    case InvokeType.BeginInvoke:
                        textBox.BeginInvoke(new Action<string>(t =>
                        {
                            textBox.AppendText($"[{DateTime.Now}]{Environment.NewLine}{t}{Environment.NewLine}{Environment.NewLine}");
                        }), txt);
                        break;
                }
                return;
            }
            
            textBox.AppendText($"[{DateTime.Now}]{Environment.NewLine}{txt}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
