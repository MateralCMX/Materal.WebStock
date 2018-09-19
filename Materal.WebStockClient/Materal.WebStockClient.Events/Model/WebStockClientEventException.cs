using System;
using System.Runtime.Serialization;

namespace Materal.WebStockClient.Events.Model
{
    public class WebStockClientEventException : ApplicationException
    {
        public WebStockClientEventException()
        {
        }

        public WebStockClientEventException(string message) : base(message)
        {
        }

        public WebStockClientEventException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebStockClientEventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
