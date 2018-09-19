using Materal.WebStock.Model;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.WebStock
{
    public class WebStockClientImpl : IWebStockClient
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebStockClientImpl()
        {
            _cancellationToken = new CancellationToken();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        public WebStockClientImpl(WebStockClientConfigModel config)
        {
            _cancellationToken = new CancellationToken();
            SetConfig(config);
        }
        /// <summary>
        /// 配置对象
        /// </summary>
        private WebStockClientConfigModel _config;
        /// <summary>
        /// 配置对象
        /// </summary>
        protected WebStockClientConfigModel Config
        {
            get => _config;
            set
            {
                _config = value;
                var args = new ConfigEventArgs
                {
                    Config = _config,
                    Message = _config == null ? "未进行配置" : "" + _config.Url
                };
                OnConfigChange?.Invoke(args);
            }
        }
        /// <summary>
        /// WebSocket客户端
        /// </summary>
        protected ClientWebSocket ClientWebSocket;
        /// <summary>
        /// 取消标记
        /// </summary>
        private CancellationToken _cancellationToken;
        /// <summary>
        /// 客户端状态
        /// </summary>
        private WebStockClientStateEnum _state = WebStockClientStateEnum.NotConfigured;
        public WebStockClientStateEnum State
        {
            get
            {
                return _state;
            }
            private set
            {
                _state = value;
                OnStateChange?.Invoke(new ConnectServerEventArgs
                {
                    State = State
                });
            }
        }
        public event ConfigEvent OnConfigChange;
        public event ConnectServerEvent OnStateChange;
        public event MessagingEvent OnMessaging;
        public event MessageEvent OnOutputMessage;
        public void SetConfig(WebStockClientConfigModel config)
        {
            if (config.Verification(out List<string> messages))
            {
                Config = config;
                State = WebStockClientStateEnum.Ready;
            }
            else
            {
                OnOutputMessage?.Invoke(new MessageEventArgs
                {
                    Message = string.Join(",", messages)
                });
            }
        }
        public void Dispose()
        {
            if (State == WebStockClientStateEnum.Runing)
            {
                _cancellationToken = new CancellationToken();
                StopAsync().Wait(_cancellationToken);
            }
            _cancellationToken = new CancellationToken();
            State = WebStockClientStateEnum.Stop;
        }
        public async Task ReloadAsync()
        {
            switch (State)
            {
                case WebStockClientStateEnum.NotConfigured:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务尚未配置"
                    });
                    break;
                case WebStockClientStateEnum.Ready:
                case WebStockClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case WebStockClientStateEnum.Runing:
                    await StopAsync();
                    await OpenWebStockClientAsync();
                    break;
                case WebStockClientStateEnum.Stop:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已经停止,请重新配置"
                    });
                    break;
            }
        }
        public virtual async Task SendMessageByBytesAsync(byte[] data, string message)
        {
            if (State == WebStockClientStateEnum.Runing)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(data);
                await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, _cancellationToken);
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = message,
                    ByteArray = data,
                    Type = MessageingTypeEnum.Send
                });
            }
            else
            {
                OnOutputMessage?.Invoke(new MessageEventArgs
                {
                    Message = "服务尚未启动"
                });
            }
        }
        public virtual async Task SendMessageByStringAsync(string message)
        {
            if (State == WebStockClientStateEnum.Runing)
            {
                byte[] byteArray = _config.EncodingType.GetBytes(message);
                ArraySegment<byte> buffer = new ArraySegment<byte>(byteArray);
                await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = message,
                    Type = MessageingTypeEnum.Send
                });
            }
            else
            {
                OnOutputMessage?.Invoke(new MessageEventArgs
                {
                    Message = "服务尚未启动"
                });
            }
        }
        public async Task StartAsync()
        {
            switch (State)
            {
                case WebStockClientStateEnum.NotConfigured:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务尚未配置"
                    });
                    break;
                case WebStockClientStateEnum.Ready:
                case WebStockClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case WebStockClientStateEnum.Runing:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已在运行"
                    });
                    break;
                case WebStockClientStateEnum.Stop:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已经停止,请重新配置"
                    });
                    break;
            }
        }
        public virtual async Task StartListeningMessageAsync()
        {
            while (State == WebStockClientStateEnum.Runing && ClientWebSocket != null && ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    byte[] serverByteArray = new byte[_config.ServerMessageMaxLength];
                    ArraySegment<byte> buffer = new ArraySegment<byte>(serverByteArray);
                    var wsdata = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    byte[] bRec = new byte[wsdata.Count];
                    Array.Copy(serverByteArray, bRec, wsdata.Count);
                    OnMessaging?.Invoke(new MessaginEventArgs
                    {
                        Encoding = _config.EncodingType,
                        ByteArray = bRec,
                        Type = MessageingTypeEnum.Receive
                    });
                }
                catch (Exception ex)
                {
                    MessageEventArgs args = new MessageEventArgs
                    {
                        Message = ex.Message
                    };
                    switch (ClientWebSocket.State)
                    {
                        case WebSocketState.Aborted:
                        case WebSocketState.CloseSent:
                        case WebSocketState.CloseReceived:
                        case WebSocketState.Closed:
                            State = WebStockClientStateEnum.ConnectionFailed;
                            args.Message = "连接已关闭";
                            break;
                        case WebSocketState.Connecting:
                        case WebSocketState.None:
                        case WebSocketState.Open:
                            break;
                    }
                    OnOutputMessage?.Invoke(args);
                    break;
                }
            }
        }
        public virtual async void StartListeningMessage()
        {
            await StartListeningMessageAsync();
        }
        public virtual async Task StopAsync()
        {
            if (State == WebStockClientStateEnum.Runing)
            {
                if (ClientWebSocket.State == WebSocketState.Connecting || ClientWebSocket.State == WebSocketState.Open)
                {
                    try
                    {
                        await ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "NomalClosure",
                            _cancellationToken);
                        OnMessaging?.Invoke(new MessaginEventArgs
                        {
                            Message = "已发送关闭请求",
                            Type = MessageingTypeEnum.Send
                        });
                    }
                    catch
                    {
                        OnMessaging?.Invoke(new MessaginEventArgs
                        {
                            Message = "已发送关闭请求，但服务器没有回应",
                            Type = MessageingTypeEnum.Send
                        });
                    }
                }
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = "连接已关闭",
                    Type = MessageingTypeEnum.Send
                });
            }
        }
        /// <summary>
        /// 打开WebStock客户端
        /// </summary>
        /// <returns></returns>
        private async Task OpenWebStockClientAsync()
        {
            ClientWebSocket = new ClientWebSocket();
            var uri = new Uri(_config.Url);
            try
            {
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                State = WebStockClientStateEnum.Runing;
            }
            catch(Exception)
            {
                State = WebStockClientStateEnum.ConnectionFailed;
            }
        }
    }
}
