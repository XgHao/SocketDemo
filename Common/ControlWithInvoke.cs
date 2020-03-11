using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        /// 获取选中的集合
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetSelectedItemsWithInvoke(this ListBox listBox, InvokeType type = InvokeType.Invoke)
        {
            List<string> clients = new List<string>();
            
            if (listBox.InvokeRequired)
            {
                switch (type)
                {
                    case InvokeType.Invoke:
                        listBox.Invoke(new Action(() =>
                        {
                            foreach (var item in listBox.SelectedItems)
                            {
                                clients.Add(item.ToString());
                            }
                        }));
                        break;
                    case InvokeType.BeginInvoke:
                        listBox.BeginInvoke(new Action(() =>
                        {
                            foreach (var item in listBox.SelectedItems)
                            {
                                clients.Add(item.ToString());
                            }
                        }));
                        break;
                }
            }
            else
            {
                foreach (var item in listBox.SelectedItems)
                {
                    clients.Add(item.ToString());
                }
            }

            return clients;
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

        /// <summary>
        /// 获取选中项，返回string
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="invoke"></param>
        /// <returns></returns>
        public static string GetSelectedItemWithInvoke(this ComboBox comboBox, InvokeType invoke = InvokeType.Invoke)
        {
            string obj = string.Empty;
            if (comboBox.InvokeRequired)
            {
                switch (invoke)
                {
                    case InvokeType.Invoke:
                        comboBox.Invoke(new Action(() =>
                        {
                            obj = comboBox.SelectedItem.ToString();
                        }));
                        break;
                    case InvokeType.BeginInvoke:
                        comboBox.BeginInvoke(new Action(() =>
                        {
                            obj = comboBox.SelectedItem.ToString();
                        }));
                        break;
                }
            }
            else
            {
                obj = comboBox.SelectedItem.ToString();
            }

            return obj;
        }
    }
}
