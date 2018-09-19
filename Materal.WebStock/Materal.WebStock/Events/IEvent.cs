namespace Materal.WebStock.Events
{
    public interface IEvent<T>
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        T Data { get; set; }
    }
}
