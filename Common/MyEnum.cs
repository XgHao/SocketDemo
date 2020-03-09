using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class MyEnum
    {
        /// <summary>
        /// 添加或者移除
        /// </summary>
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
        /// 指定Invoke是异步还是同步
        /// </summary>
        public enum InvokeType
        {
            /// <summary>
            /// 同步
            /// </summary>
            Invoke,
            /// <summary>
            /// 异步
            /// </summary>
            BeginInvoke
        }

        /// <summary>
        /// 主题色
        /// </summary>
        public enum ThemeColor
        {
            /// <summary>
            /// 主要
            /// </summary>
            Primary,
            /// <summary>
            /// 次要
            /// </summary>
            Secondary,
            /// <summary>
            /// 成功
            /// </summary>
            Success,
            /// <summary>
            /// 危险
            /// </summary>
            Danger,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 亮色
            /// </summary>
            Light,
            /// <summary>
            /// 暗色
            /// </summary>
            Dark,
            /// <summary>
            /// 默认
            /// </summary>
            Default
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
