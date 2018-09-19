using System;
using System.Runtime.Serialization;

namespace Materal.WebStock.EventHandlers.Model
{
    public class EventHandlerException : ApplicationException
    {
        public EventHandlerException()
        {
        }

        public EventHandlerException(string message) : base(message)
        {
        }

        public EventHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EventHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
