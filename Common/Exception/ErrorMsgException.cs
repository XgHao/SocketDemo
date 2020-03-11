using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common.Exception
{
    /// <summary>
    /// 消息流错误异常
    /// </summary>
    [Serializable]
    public class ErrorMsgException : ApplicationException
    {
        public ErrorMsgException() : base()
        {
        }

        public ErrorMsgException(string message) : base(message)
        {
        }

        public ErrorMsgException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ErrorMsgException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
