using System;
using System.Runtime.Serialization;

namespace Materal.WebStock.CommandHandlers.Model
{
    public class CommandHandlerException : ApplicationException
    {
        public CommandHandlerException()
        {
        }

        public CommandHandlerException(string message) : base(message)
        {
        }

        public CommandHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
