using MateralTools.Base.Model;
using System;

namespace Materal.WebStock.Commands.Model
{
    public class CommandException : MException
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
