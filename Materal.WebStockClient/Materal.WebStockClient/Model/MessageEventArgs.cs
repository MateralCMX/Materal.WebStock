using System;

namespace Materal.WebStockClient.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 消息事件参数
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 消息对象
        /// </summary>
        public string Message { get; set; }
    }
}
