using System;
using System.Runtime.Serialization;

namespace Materal.WebStock.CommandHandlers.Model
{
    public class WebStockClientCommandHandlerException : ApplicationException
    {
        public WebStockClientCommandHandlerException()
        {
        }

        public WebStockClientCommandHandlerException(string message) : base(message)
        {
        }

        public WebStockClientCommandHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebStockClientCommandHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
