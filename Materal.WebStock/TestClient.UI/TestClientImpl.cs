using Materal.WebStock.Model;
using System;
using System.Text;
using TestClient.Common;
using TestClient.Events;
using TestClient.WebStockClient;
using TestClient.WebStockClient.Model;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        public bool IsAutoReload { get; set; }
        private readonly ITestClientClient _testClientClient;

        public TestClientImpl(ITestClientClient testClientClient)
        {
            _testClientClient = testClientClient;
        }
        public void Dispose()
        {
            Stop();
        }
        public void Init()
        {
            var testClientConfigModel = new ClientConfigModel
            {
                Url = "ws://127.0.0.1:10000",
                EncodingType = Encoding.UTF8,
                ServerMessageMaxLength = 102400
            };
            _testClientClient.OnConfigChange += TestClientClientOnConfigChange;
            _testClientClient.OnMessaging += TestClientClientOnMessaging;
            _testClientClient.OnStateChange += TestClientClientOnStateChange;
            _testClientClient.OnOutputMessage += TestClientClientOnOutputMessage;
            _testClientClient.OnOutputTestClientMessage += TestClientClientOnOutputMessage;
            _testClientClient.SetConfig(testClientConfigModel);
        }
        public void Start()
        {
            StartExternalWebStockClient();
        }
        public void Stop()
        {
            _testClientClient?.Dispose();
        }
        /// <summary>
        /// 状态更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnStateChange(ConnectServerEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("状态更新");
            ConsoleHelper.TestClientWriteLine(args.Message);
            switch (args.State)
            {
                case ClientStateEnum.NotConfigured:
                    break;
                case ClientStateEnum.Ready:
                    break;
                case ClientStateEnum.Runing:
                    _testClientClient.StartListeningMessage();
                    break;
                case ClientStateEnum.ConnectionFailed:
                    if (IsAutoReload)
                    {
                        ConsoleHelper.TestClientWriteLine("重新连接");
                        _testClientClient.ReloadAsync().Wait();
                    }
                    break;
                case ClientStateEnum.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 消息传递事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnMessaging(MessaginEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("消息更新");
            switch (args.Type)
            {
                case MessageingTypeEnum.Command:
                    ConsoleHelper.TestClientWriteLine(args.Message, "发送");
                    break;
                case MessageingTypeEnum.Event:
                    try
                    {
                        ConsoleHelper.TestClientWriteLine(args.Message, "接收");
                        Event @event = new Event
                        {
                            Data = args.Data.ToString()
                        };
                        _testClientClient.HandleEventAsync()
                    }
                    catch (TestClientClientException ex)
                    {
                        ConsoleHelper.TestClientWriteLine(ex.Message, "解析错误");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 配置更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnConfigChange(ConfigEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("配置更新");
            ConsoleHelper.TestClientWriteLine(args.Message);
        }
        /// <summary>
        /// 输出消息事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnOutputMessage(MessageEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message, args.SubTitle);
        }
        /// <summary>
        /// 启动外接WebStock客户端
        /// </summary>
        private async void StartExternalWebStockClient()
        {
            await _testClientClient.StartAsync();
        }
    }
}
