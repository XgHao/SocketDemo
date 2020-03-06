using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// 消息框帮助类
    /// </summary>
    public static class MessageBoxHelper
    {
        /// <summary>
        /// 提示框
        /// </summary>
        /// <param name="form"></param>
        /// <param name="hint"></param>
        public static void Hint(this Form form, Control control, string hint, MyEnum.ShowPosition position = MyEnum.ShowPosition.Bottom_Right)
        {
            Point point;

            switch (position)
            {
                case MyEnum.ShowPosition.Top_Left:
                    point = new Point
                    {
                        X = control.Location.X,
                        Y = control.Location.Y
                    };
                    break;
                case MyEnum.ShowPosition.Top_Mid:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width / 2,
                        Y = control.Location.Y 
                    };
                    break;
                case MyEnum.ShowPosition.Top_Right:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width,
                        Y = control.Location.Y
                    };
                    break;
                case MyEnum.ShowPosition.Mid_Left:
                    point = new Point
                    {
                        X = control.Location.X,
                        Y = control.Location.Y + control.Size.Height / 2
                    };
                    break;
                case MyEnum.ShowPosition.Mid_Mid:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width / 2,
                        Y = control.Location.Y + control.Size.Height / 2
                    };
                    break;
                case MyEnum.ShowPosition.Mid_Right:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width,
                        Y = control.Location.Y + control.Size.Height /2
                    };
                    break;
                case MyEnum.ShowPosition.Bottom_Left:
                    point = new Point
                    {
                        X = control.Location.X,
                        Y = control.Location.Y + control.Size.Height
                    };
                    break;
                case MyEnum.ShowPosition.Bottom_Mid:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width / 2,
                        Y = control.Location.Y + control.Size.Height
                    };
                    break;
                case MyEnum.ShowPosition.Bottom_Right:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width,
                        Y = control.Location.Y + control.Size.Height
                    };
                    break;
                default:
                    point = new Point
                    {
                        X = control.Location.X + control.Size.Width,
                        Y = control.Location.Y + control.Size.Height
                    };
                    break;
            }

            //提示框
            Help.ShowPopup(control, hint, form.PointToScreen(point));
        }
    }
}
