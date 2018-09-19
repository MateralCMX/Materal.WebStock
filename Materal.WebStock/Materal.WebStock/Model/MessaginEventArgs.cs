using System;

namespace Materal.WebStock.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 消息传递事件参数
    /// </summary>
    public class MessaginEventArgs : EventArgs
    {
        /// <summary>
        /// 消息对象
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MessageingTypeEnum Type { get; set; }
    }
}
