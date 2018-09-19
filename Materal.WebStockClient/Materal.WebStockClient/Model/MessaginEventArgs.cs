using System;
using System.Text;

namespace Materal.WebStockClient.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 消息传递事件参数
    /// </summary>
    public class MessaginEventArgs:EventArgs
    {
        /// <summary>
        /// 消息对象
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// 消息byte数组
        /// </summary>
        public byte[] ByteArray { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MessageingTypeEnum Type { get; set; }
    }
}
