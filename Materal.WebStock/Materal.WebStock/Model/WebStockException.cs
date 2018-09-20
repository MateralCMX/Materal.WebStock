using System;

namespace Materal.WebStock.Model
{
    public class WebStockException : Exception
    {
        public WebStockException()
        {
        }

        public WebStockException(string message) : base(message)
        {
        }

        public WebStockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
