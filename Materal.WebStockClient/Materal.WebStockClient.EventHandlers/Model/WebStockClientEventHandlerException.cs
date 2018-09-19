using System;
using System.Runtime.Serialization;

namespace Materal.WebStockClient.EventHandlers.Model
{
    public class WebStockClientEventHandlerException : ApplicationException
    {
        public WebStockClientEventHandlerException()
        {
        }

        public WebStockClientEventHandlerException(string message) : base(message)
        {
        }

        public WebStockClientEventHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebStockClientEventHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
