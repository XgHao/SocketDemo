using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketTask.Helper
{
    /// <summary>
    /// 网络请求帮助类
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// 公网IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetPublicIP()
        {
            IPAddress publicIP = null;

            Stream stream = null;
            StreamReader streamReader = null;
            try
            {
                stream = WebRequest.Create("https://www.ipip5.com/").GetResponse().GetResponseStream();
                streamReader = new StreamReader(stream, Encoding.UTF8);
                var str = streamReader.ReadToEnd();
                int first = str.IndexOf("<span class=\"c-ip\">") + 19;
                int last = str.IndexOf("</span>", first);
                var ip = str.Substring(first, last - first);
                IPAddress.TryParse(ip, out publicIP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                streamReader?.Dispose();
                stream?.Dispose();
            }

            return publicIP;
        }

        /// <summary>
        /// 内网IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIP()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily.ToString().Equals("InterNetwork")).FirstOrDefault();
        }
    }
}
