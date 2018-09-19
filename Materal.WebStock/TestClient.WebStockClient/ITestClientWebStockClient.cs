using Materal.WebStock;
using Materal.WebStock.Model;
using System.Threading.Tasks;
using TestClient.Commands;

namespace TestClient.WebStockClient
{
    public interface ITestClientWebStockClient : IWebStockClient
    {
        /// <summary>
        /// 输出消息
        /// </summary>
        event MessageEvent OnOutputTestClientMessage;

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="commandM">命令对象</param>
        Task SendMessageByCommandAsync(Command commandM);

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task HandleMessageAsync(string message);
    }
    #region 委托定义
    /// <summary>
    /// 处理消息委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void HandleMessageEvent<T>(HandleMessageEventArgs<T> args);
    #endregion
}
