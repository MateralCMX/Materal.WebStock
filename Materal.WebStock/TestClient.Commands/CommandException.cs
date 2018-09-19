using Materal.WebStock.Commands.Model;
using System;
using System.Runtime.Serialization;

namespace TestClient.Commands
{
    public class CommandException : WebStockClientCommandException
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

        protected CommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
