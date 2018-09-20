using Materal.WebStock.Model;
using System;

namespace Materal.WebStock.Commands.Model
{
    public class CommandException : WebStockException
    {
        public CommandException()
        {
        }

        public CommandException(string message) : base(message)
        {
        }

        public CommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
