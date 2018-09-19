using System.Threading.Tasks;

namespace Materal.WebStockClient.Commands
{
    /// <summary>
    /// 命令总线
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IWebStockClientCommandBus<in T>
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <param name="data">数据</param>
        void Send(string handlerName, T data);
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        Task SendAsync(string handlerName, T data);

    }
}
