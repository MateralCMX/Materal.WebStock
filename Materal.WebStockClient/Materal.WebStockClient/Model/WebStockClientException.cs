using System;
using System.Runtime.Serialization;

namespace Materal.WebStockClient.Model
{
    public class WebStockClientException : ApplicationException
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebStockClientException()
        {
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public WebStockClientException(string message) : base(message)
        {
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception. If the innerException
        ///     parameter is not a null reference, the current exception is raised in a catch
        ///     block that handles the inner exception.
        /// </param>
        public WebStockClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected WebStockClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
