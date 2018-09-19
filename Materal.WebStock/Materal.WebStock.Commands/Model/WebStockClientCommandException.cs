using System;
using System.Runtime.Serialization;

namespace Materal.WebStock.Commands.Model
{
    public class WebStockClientCommandException : ApplicationException
    {
        public WebStockClientCommandException()
        {
        }

        public WebStockClientCommandException(string message) : base(message)
        {
        }

        public WebStockClientCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebStockClientCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
