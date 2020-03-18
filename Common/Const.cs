using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// 字节缓冲区大小
        /// </summary>
        public static long BufferByteSize
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["BufferByteSize"], out int cnt))
                    return cnt * 1024 * 1024;
                else
                    return 2 * 1024 * 1024;
            }
        }

        /// <summary>
        /// 请求外网限制时间
        /// </summary>
        public static TimeSpan RequestTime
        {
            get
            {
                if (double.TryParse(ConfigurationManager.AppSettings["RequestTime"], out double time))
                    return TimeSpan.FromSeconds(time);
                else
                    return TimeSpan.FromSeconds(6);
            }
        }

        /// <summary>
        /// 发送文件大小限制
        /// </summary>
        public static long MaxFileSize
        {
            get
            {
                int cnt = StreamHeadSize - 2;
                for (int i = 0; i < cnt; i++)
                {

                }
                if (long.TryParse(ConfigurationManager.AppSettings["FileSize"], out long size))
                    return size * 1024 * 1024;
                else
                    return 3 * 1024 * 1024;
            }
        }

        /// <summary>
        /// 字节流头部大小
        /// </summary>
        public static int StreamHeadSize
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["StreamHeadSize"], out int head))
                    return head;
                else
                    return 10;
            }
        }
    }
}
