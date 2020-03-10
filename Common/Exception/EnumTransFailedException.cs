using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    [Serializable]
    public class EnumTransFailedException : ApplicationException
    {
        public EnumTransFailedException(string message) : base(message)
        {
        }
        protected EnumTransFailedException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
