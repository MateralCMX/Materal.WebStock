using System;
using System.Runtime.Serialization;
using Materal.WebStock.Events.Model;

namespace TestClient.Events
{
    public class EventsException : WebStockClientEventException
    {
        public EventsException()
        {
        }

        public EventsException(string message) : base(message)
        {
        }

        public EventsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EventsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
