using Common.Exception;
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
        /// 消息流头部
        /// 用于区分接受类型
        /// </summary>
        public enum MsgStreamHead
        {
            /// <summary>
            /// 文本
            /// </summary>
            Text,
            /// <summary>
            /// 图片
            /// </summary>
            Picture,
            /// <summary>
            /// 文件
            /// </summary>
            File,
        }

        /// <summary>
        /// 转换格式
        /// </summary>
        public enum Format
        {
            Default,
            UTF8,
            ASCII,
            Unicode
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

        /// <summary>
        /// ID获取对应枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="id"></param>
        /// <exception cref="ErrorMsgException"></exception>
        /// <returns></returns>
        public static T GetEnumByIDorName<T>(int id) where T : struct, Enum
        {
            if (Enum.TryParse(id.ToString(), out T type))
            {
                return type;
            }
            throw new ErrorMsgException("获取枚举格式失败");
        }

        /// <summary>
        /// 名称获取对应枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <exception cref="ErrorMsgException"></exception>
        /// <returns></returns>
        public static T GetEnumByIDorName<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse(value, out T type)) 
            {
                return type;
            }
            throw new ErrorMsgException("获取枚举格式失败");
        }
    }
}
