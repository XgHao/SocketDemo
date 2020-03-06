using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
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
    }
}
