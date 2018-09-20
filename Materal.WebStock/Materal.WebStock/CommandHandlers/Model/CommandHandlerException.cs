using Materal.WebStock.Model;
using System;

namespace Materal.WebStock.CommandHandlers.Model
{
    public class CommandHandlerException : WebStockException
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
