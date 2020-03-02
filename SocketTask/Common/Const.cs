using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTask.Common
{
    public static class Const
    {
        public static readonly long BufferByteSize = GetSize();

        private static long GetSize()
        {
            if (int.TryParse(ConfigurationManager.AppSettings["BufferByteSize"], out int cnt))
                return cnt * 1024 * 1024;
            else
                return 2 * 1024 * 1024;
        }
    }
}
