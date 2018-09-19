using System;

namespace Materal.WebStock.Common
{
    public class HandlerAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        public HandlerAttribute(string handlerName)
        {
            HandlerName = handlerName;
        }
        /// <summary>
        /// 处理器名称
        /// </summary>
        public string HandlerName { get; set; }
    }
}
