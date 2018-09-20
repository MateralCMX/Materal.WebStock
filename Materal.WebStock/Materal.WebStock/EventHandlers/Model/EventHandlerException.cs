using MateralTools.Base.Model;
using System;

namespace Materal.WebStock.EventHandlers.Model
{
    public class EventHandlerException : MException
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
