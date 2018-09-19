using Materal.WebStock;
using Materal.WebStock.Model;
using System.Threading.Tasks;
using TestClient.Commands;
using TestClient.Events;

namespace TestClient.WebStockClient
{
    public interface ITestClientClient : IClient<string>
    {
        /// <summary>
        /// 输出消息
        /// </summary>
        event MessageEvent OnOutputTestClientMessage;

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="commandM">命令对象</param>
        Task SendCommandAsync(Command commandM);

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventM">消息</param>
        /// <returns></returns>
        Task HandleEventAsync(Event eventM);
    }
    #region 委托定义
    /// <summary>
    /// 处理消息委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void HandleMessageEvent<T>(HandleMessageEventArgs<T> args);
    #endregion
}
