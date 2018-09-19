using System;
using System.Threading.Tasks;
using Materal.WebStockClient;
using Materal.WebStockClient.Commands;
using Materal.WebStockClient.Events;
using Materal.WebStockClient.Model;
using MateralTools.MConvert.Manager;
using MateralTools.MConvert.Model;
using Microsoft.Extensions.DependencyInjection;
using TestClient.Commands;
using TestClient.Events;

namespace TestClient.WebStockClient
{
    public class TestClientWebStockClientImpl : WebStockClientImpl, ITestClientWebStockClient
    {
        private readonly IServiceProvider _serviceProvider;
        public event MessageEvent OnOutputTestClientMessage;
        public TestClientWebStockClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task SendMessageByCommandAsync(Command commandM)
        {
            try
            {
                var data = commandM.ToString();
                var commandBus = (IWebStockClientCommandBus<string>)_serviceProvider.GetRequiredService(typeof(IWebStockClientCommandBus<string>));
                await commandBus.SendAsync(commandM.GetHandelerName(), data);
            }
            catch (MConvertException)
            {
                OnOutputTestClientMessage?.Invoke(new MessageEventArgs
                {
                    Message = "未能解析服务器推送的消息"
                });
            }
        }
        public async Task HandleMessageAsync(string message)
         {
            try
            {
                var model = message.MJsonToObject<Event>();
                var commandBus = (IWebStockClientEventBus<string>)_serviceProvider.GetRequiredService(typeof(IWebStockClientEventBus<string>));
                await commandBus.SendAsync(model.HandlerName, message);
            }
            catch (MConvertException)
            {
                OnOutputTestClientMessage?.Invoke(new MessageEventArgs
                {
                    Message = "未能解析服务器推送的消息"
                });
            }
        }
    }
}
