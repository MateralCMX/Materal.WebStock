using Materal.WebStock.Model;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.WebStock
{
    public abstract class ClientImpl<T> : IClient<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected ClientImpl()
        {
            _cancellationToken = new CancellationToken();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        protected ClientImpl(ClientConfigModel config)
        {
            _cancellationToken = new CancellationToken();
            SetConfig(config);
        }
        /// <summary>
        /// 配置对象
        /// </summary>
        private ClientConfigModel _config;
        /// <summary>
        /// 配置对象
        /// </summary>
        protected ClientConfigModel Config
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
        private ClientStateEnum _state = ClientStateEnum.NotConfigured;
        public ClientStateEnum State
        {
            get => _state;
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
        public void SetConfig(ClientConfigModel config)
        {
            if (config.Verification(out List<string> messages))
            {
                Config = config;
                State = ClientStateEnum.Ready;
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
            if (State == ClientStateEnum.Runing)
            {
                _cancellationToken = new CancellationToken();
                StopAsync().Wait(_cancellationToken);
            }
            _cancellationToken = new CancellationToken();
            State = ClientStateEnum.Stop;
        }
        public async Task ReloadAsync()
        {
            switch (State)
            {
                case ClientStateEnum.NotConfigured:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务尚未配置"
                    });
                    break;
                case ClientStateEnum.Ready:
                case ClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Runing:
                    await StopAsync();
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Stop:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已经停止,请重新配置"
                    });
                    break;
            }
        }
        public virtual async Task SendCommandByBytesAsync(byte[] data, string message)
        {
            if (State == ClientStateEnum.Runing)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(data);
                await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, _cancellationToken);
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = message,
                    Data = data,
                    Type = MessageingTypeEnum.Command
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
        public virtual async Task SendCommandByStringAsync(string data, string message)
        {
            if (State == ClientStateEnum.Runing)
            {
                byte[] byteArray = _config.EncodingType.GetBytes(data);
                ArraySegment<byte> buffer = new ArraySegment<byte>(byteArray);
                await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = message,
                    Data = data,
                    Type = MessageingTypeEnum.Command
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
        public abstract Task SendCommandAsync(T data, string message);
        public async Task StartAsync()
        {
            switch (State)
            {
                case ClientStateEnum.NotConfigured:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务尚未配置"
                    });
                    break;
                case ClientStateEnum.Ready:
                case ClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Runing:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已在运行"
                    });
                    break;
                case ClientStateEnum.Stop:
                    OnOutputMessage?.Invoke(new MessageEventArgs
                    {
                        Message = "服务已经停止,请重新配置"
                    });
                    break;
            }
        }
        public virtual async Task StartListeningMessageAsync()
        {
            while (State == ClientStateEnum.Runing && ClientWebSocket != null && ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    var serverByteArray = new byte[_config.ServerMessageMaxLength];
                    var buffer = new ArraySegment<byte>(serverByteArray);
                    WebSocketReceiveResult wsdata = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    var bRec = new byte[wsdata.Count];
                    Array.Copy(serverByteArray, bRec, wsdata.Count);
                    OnMessaging?.Invoke(new MessaginEventArgs
                    {
                        Data = bRec,
                        Type = MessageingTypeEnum.Event
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
                            State = ClientStateEnum.ConnectionFailed;
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
            if (State == ClientStateEnum.Runing)
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
                            Type = MessageingTypeEnum.Command
                        });
                    }
                    catch
                    {
                        OnMessaging?.Invoke(new MessaginEventArgs
                        {
                            Message = "已发送关闭请求，但服务器没有回应",
                            Type = MessageingTypeEnum.Command
                        });
                    }
                }
                OnMessaging?.Invoke(new MessaginEventArgs
                {
                    Message = "连接已关闭",
                    Type = MessageingTypeEnum.Command
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
                State = ClientStateEnum.Runing;
            }
            catch(Exception)
            {
                State = ClientStateEnum.ConnectionFailed;
            }
        }
    }
}
