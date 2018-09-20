using Materal.WebStock.Model;
using System;

namespace Materal.WebStock.Events.Model
{
    public class EventException : WebStockException
    {
        public EventException()
        {
        }

        public EventException(string message) : base(message)
        {
        }

        public EventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
