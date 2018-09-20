using Materal.WebStock.Model;
using System;

namespace Materal.WebStock.EventHandlers.Model
{
    public class EventHandlerException : WebStockException
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
    }
}
