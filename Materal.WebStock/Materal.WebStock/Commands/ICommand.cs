namespace Materal.WebStock.Commands
{
    public interface ICommand<T>
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandelerName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        T Data { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; set; }
    }
}
