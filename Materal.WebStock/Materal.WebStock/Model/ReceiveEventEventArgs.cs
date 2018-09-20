using System;

namespace Materal.WebStock.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 消息传递事件参数
    /// </summary>
    public class ReceiveEventEventArgs : EventArgs
    {
        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] ByteArrayData { get; set; }
    }
}
