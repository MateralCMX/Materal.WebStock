using System;

namespace Materal.WebStockClient.Model
{
    /// <summary>
    /// 处理消息事件参数
    /// </summary>
    public class HandleMessageEventArgs<T>: EventArgs
    {
        /// <summary>
        /// 解析后的数据
        /// </summary>
        public T Data { get; set; }
    }
}
