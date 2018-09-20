using MateralTools.Base.Model;
using System;

namespace Materal.WebStock.Events.Model
{
    public class EventException : MException
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
