using MateralTools.Base.Model;
using System;

namespace Materal.WebStock.CommandHandlers.Model
{
    public class CommandHandlerException : MException
    {
        public CommandHandlerException()
        {
        }

        public CommandHandlerException(string message) : base(message)
        {
        }

        public CommandHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
