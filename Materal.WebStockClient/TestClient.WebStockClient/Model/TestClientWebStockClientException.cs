using System;
using System.Runtime.Serialization;
using Materal.WebStockClient.Model;

namespace TestClient.WebStockClient.Model
{
    public class TestClientWebStockClientException : WebStockClientException
    {
        public TestClientWebStockClientException()
        {
        }

        public TestClientWebStockClientException(string message) : base(message)
        {
        }

        public TestClientWebStockClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestClientWebStockClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
