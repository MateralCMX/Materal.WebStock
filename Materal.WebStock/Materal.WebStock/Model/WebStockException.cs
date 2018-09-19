using System;
using MateralTools.Base.Model;

namespace Materal.WebStock.Model
{
    public class WebStockException : MException
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
