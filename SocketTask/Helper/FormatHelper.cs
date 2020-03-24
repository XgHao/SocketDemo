using Common;
using Common.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.MyEnum;

namespace SocketTask.Helper
{
    /// <summary>
    /// 格式转换
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// 字符转换为消息流
        /// </summary>
        /// <param name="text"></param>
        /// <returns>byte数组</returns>
        public static byte[] TextToStream(string text, Format format = Format.Default)
        {
            //总容器：正文+头部
            byte[] stream = new byte[Const.BufferByteSize + Const.StreamHeadSize];
            //正文
            byte[] body = text.GetArrayByte();
            //头部
            byte[] head = new byte[Const.StreamHeadSize];

            try
            {
                //头部-文本形式
                head[0] = (int)MsgStreamHead.Text;

                //拼接-头部
                Array.Copy(head, 0, stream, 0, head.Length);
                //拼接-正文
                Array.Copy(body, 0, stream, head.Length, body.Length);
            }
            //内容内容超限，抛出异常
            catch (Exception ex)
            {
                throw ex;
            }

            return stream.RemoveNull();
        }

        /// <summary>
        /// 消息流转换为字符串
        /// 返回byte[]，则表示是文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static object StreamToText(byte[] stream, Format format = Format.Default)
        {
            //1.获取主体
            byte[] body = stream.Skip(Const.StreamHeadSize).ToArray();

            //2.获取数据类型
            MsgStreamHead type;
            try
            {
                //根据头部信息获取消息类型
                type = GetEnumByIDorName<MsgStreamHead>(stream[0]);
            }
            catch (ErrorMsgException)
            {
                return "出错，接收失败";
            }

            //3.根据头部类型判断操作
            object obj = string.Empty;
            switch (type)
            {
                case MsgStreamHead.Text:
                    switch (format)
                    {
                        case Format.UTF8:
                            obj = Encoding.UTF8.GetString(body);
                            break;
                        case Format.ASCII:
                            obj = Encoding.ASCII.GetString(body);
                            break;
                        case Format.Default:
                            obj = Encoding.Default.GetString(body);
                            break;
                        case Format.Unicode:
                            obj = Encoding.Unicode.GetString(body);
                            break;
                    }
                    break;
                case MsgStreamHead.Picture:
                    obj = "【收到一张表情，该版本暂不支持】";
                    break;
                case MsgStreamHead.File:
                    //返回所有信息
                    obj = stream;
                    break;
                default:
                    break;
            }

            return obj;
        }

        /// <summary>
        /// 文件转为文件发送格式
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] ToFileStream(FileInfo file)
        {
            //文件流
            FileStream fileStream = null;
            //容器
            byte[] stream = new byte[Const.BufferByteSize];
            //头部
            byte[] head = new byte[Const.StreamHeadSize];
            //文件流大小限制
            byte[] filebytes = new byte[Const.MaxFileSize];

            try
            {
                //根据文件，生成对应流
                fileStream = new FileStream(file.FullName, FileMode.Open);
                
                //读取文件byte[]
                fileStream.Read(filebytes, 0, (int)fileStream.Length);
                //头部
                head[0] = (byte)MsgStreamHead.File; //类型
                head[1] = (byte)GetEnumByIDorName<Extensions>(file.Extension.TrimStart('.'));   //格式
                //拼接
                Array.Copy(head, 0, stream, 0, head.Length);
                Array.Copy(filebytes, 0, stream, Const.StreamHeadSize, fileStream.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream?.Dispose();
            }

            return stream;
        }
    }
}
