using Common;
using Common.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            byte[] body = text.GetArrayByte(format); 
            byte[] stream = new byte[Const.BufferByteSize];
            //头部-文本形式
            stream[0] = (int)MsgStreamHead.Text;
            //拼接
            try
            {
                Array.Copy(body, 0, stream, 1, body.Length);
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
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static object StreamToText(byte[] stream, Format format = Format.Default)
        {
            //1.获取头部,主体
            int head = stream[0];
            byte[] body = stream.Skip(1).ToArray();

            //2.获取数据类型
            MsgStreamHead type;
            try
            {
                type = GetEnumByIDorName<MsgStreamHead>(head);
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
                    obj = body;
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
            byte[] arrFileSend = new byte[Const.FileSize];

            try
            {
                fileStream = new FileStream(file.FullName, FileMode.Open);
                byte[] filebytes = new byte[Const.FileSize - 1];
                //读取文件byte[]
                fileStream.Read(filebytes, 0, (int)fileStream.Length);
                //头部
                arrFileSend[0] = (int)MsgStreamHead.File;
                //拼接
                Array.Copy(filebytes, 0, arrFileSend, 1, filebytes.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream?.Dispose();
            }

            return arrFileSend;
        }
    }
}
