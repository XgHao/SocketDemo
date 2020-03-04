using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTask.Common
{
    public static class MyEnum
    {
        public enum AddOrRemove
        {
            /// <summary>
            /// 添加
            /// </summary>
            Add,
            /// <summary>
            /// 移除
            /// </summary>
            Remove
        }

        /// <summary>
        /// 显示位置
        /// </summary>
        public enum ShowPosition
        {
            /// <summary>
            /// 上左
            /// </summary>
            Top_Left,
            /// <summary>
            /// 上中
            /// </summary>
            Top_Mid,
            /// <summary>
            /// 上右
            /// </summary>
            Top_Right,
            /// <summary>
            /// 中左
            /// </summary>
            Mid_Left,
            /// <summary>
            /// 中中
            /// </summary>
            Mid_Mid,
            /// <summary>
            /// 中右
            /// </summary>
            Mid_Right,
            /// <summary>
            /// 底左
            /// </summary>
            Bottom_Left,
            /// <summary>
            /// 底中
            /// </summary>
            Bottom_Mid,
            /// <summary>
            /// 底右
            /// </summary>
            Bottom_Right
        }
    }
}
