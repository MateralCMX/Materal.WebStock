using Materal.WebStock.Model;
using System;
using System.Text;
using MateralTools.MVerify;
using TestClient.Common;
using TestClient.WebStockClient;
using TestClient.WebStockClient.Model;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        public bool IsAutoReload { get; set; }
        private readonly ITestClientWebStockClient _testClientWebStockClient;

        public TestClientImpl(ITestClientWebStockClient testClientWebStockClient)
        {
            _testClientWebStockClient = testClientWebStockClient;
        }
        public void Dispose()
        {
            Stop();
        }
        public void Init()
        {
            var testClientConfigModel = new WebStockClientConfigModel
            {
                Url = "ws://127.0.0.1:10000",
                EncodingType = Encoding.UTF8,
                ServerMessageMaxLength = 102400
            };
            _testClientWebStockClient.OnConfigChange += _testClientWebStockClient_OnConfigChange;
            _testClientWebStockClient.OnMessaging += _testClientWebStockClient_OnMessaging;
            _testClientWebStockClient.OnStateChange += _testClientWebStockClient_OnStateChange;
            _testClientWebStockClient.OnOutputMessage += _testClientWebStockClient_OnOutputMessage;
            _testClientWebStockClient.OnOutputTestClientMessage += _testClientWebStockClient_OnOutputMessage;
            _testClientWebStockClient.SetConfig(testClientConfigModel);
        }
        public void Start()
        {
            StartExternalWebStockClient();
        }
        public void Stop()
        {
            _testClientWebStockClient?.Dispose();
        }
        /// <summary>
        /// 状态更改事件
        /// </summary>
        /// <param name="args"></param>
        private void _testClientWebStockClient_OnStateChange(ConnectServerEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("状态更新");
            ConsoleHelper.TestClientWriteLine(args.Message);
            switch (args.State)
            {
                case WebStockClientStateEnum.NotConfigured:
                    break;
                case WebStockClientStateEnum.Ready:
                    break;
                case WebStockClientStateEnum.Runing:
                    _testClientWebStockClient.StartListeningMessage();
                    break;
                case WebStockClientStateEnum.ConnectionFailed:
                    if (IsAutoReload)
                    {
                        ConsoleHelper.TestClientWriteLine("重新连接");
                        _testClientWebStockClient.ReloadAsync().Wait();
                    }
                    break;
                case WebStockClientStateEnum.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 消息传递事件
        /// </summary>
        /// <param name="args"></param>
        private void _testClientWebStockClient_OnMessaging(MessaginEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("消息更新");
            switch (args.Type)
            {
                case MessageingTypeEnum.Send:
                    ConsoleHelper.TestClientWriteLine(args.Message, "发送");
                    break;
                case MessageingTypeEnum.Receive:
                    try
                    {
                        if (args.Message.MIsNullOrEmpty())
                        {
                            args.Message = args.Encoding.GetString(args.ByteArray);
                        }
                        ConsoleHelper.TestClientWriteLine(args.Message, "接收");
                    }
                    catch (TestClientWebStockClientException ex)
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
        private void _testClientWebStockClient_OnConfigChange(ConfigEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine("配置更新");
            ConsoleHelper.TestClientWriteLine(args.Message);
        }
        /// <summary>
        /// 输出消息事件
        /// </summary>
        /// <param name="args"></param>
        private void _testClientWebStockClient_OnOutputMessage(MessageEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message, args.SubTitle);
        }
        /// <summary>
        /// 启动外接WebStock客户端
        /// </summary>
        private async void StartExternalWebStockClient()
        {
            await _testClientWebStockClient.StartAsync();
        }
    }
}
