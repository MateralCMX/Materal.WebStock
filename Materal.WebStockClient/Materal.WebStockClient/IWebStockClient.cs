using Materal.WebStockClient.Model;
using System;
using System.Threading.Tasks;

namespace Materal.WebStockClient
{
    /// <inheritdoc />
    /// <summary>
    /// WebStock客户端
    /// </summary>
    public interface IWebStockClient : IDisposable
    {
        #region 事件
        /// <summary>
        /// 配置更改时
        /// </summary>
        event ConfigEvent OnConfigChange;
        /// <summary>
        /// 当服务器状态发生改变时
        /// </summary>
        event ConnectServerEvent OnStateChange;
        /// <summary>
        /// 当有消息传递时
        /// </summary>
        event MessagingEvent OnMessaging;
        /// <summary>
        /// 输出消息
        /// </summary>
        event MessageEvent OnOutputMessage;
        #endregion
        WebStockClientStateEnum State { get;}
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="config">配置对象</param>
        void SetConfig(WebStockClientConfigModel config);
        /// <summary>
        /// 启动客户端
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 重启客户端
        /// </summary>
        /// <returns></returns>
        Task ReloadAsync();
        /// <summary>
        /// 停止客户端
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task SendMessageByStringAsync(string message);
        /// <summary>
        /// 发送ByteArray
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task SendMessageByBytesAsync(byte[] data,string message);
        /// <summary>
        /// 开始监听消息
        /// </summary>
        Task StartListeningMessageAsync();
        /// <summary>
        /// 开始监听消息
        /// </summary>
        void StartListeningMessage();
    }
    #region 委托定义
    /// <summary>
    /// 配置事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void ConfigEvent(ConfigEventArgs args);
    /// <summary>
    /// 连接服务器事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void ConnectServerEvent(ConnectServerEventArgs args);
    /// <summary>
    /// 消息传递事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void MessagingEvent(MessaginEventArgs args);
    /// <summary>
    /// 消息事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void MessageEvent(MessageEventArgs args);
    #endregion
}
