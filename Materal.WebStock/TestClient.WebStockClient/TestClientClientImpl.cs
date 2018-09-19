using Materal.WebStock;
using Materal.WebStock.Events;
using Materal.WebStock.Model;
using MateralTools.MConvert.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestClient.Commands;
using TestClient.Events;

namespace TestClient.WebStockClient
{
    public class TestClientClientImpl : ClientImpl<string>, ITestClientClient
    {
        private readonly IServiceProvider _serviceProvider;
        public event MessageEvent OnOutputTestClientMessage;
        public TestClientClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task SendCommandAsync(Command commandM)
        {
            try
            {
                await SendCommandAsync(commandM.Data,commandM.Message);
            }
            catch (MConvertException)
            {
                OnOutputTestClientMessage?.Invoke(new MessageEventArgs
                {
                    Message = "未能解析服务器推送的消息"
                });
            }
        }

        public async Task HandleEventAsync(Event eventM)
         {
            try
            {
                var commandBus = (IEventBus<string>)_serviceProvider.GetRequiredService(typeof(IEventBus<string>));
                await commandBus.SendAsync(eventM.HandlerName, eventM.Data);
            }
            catch (MConvertException)
            {
                OnOutputTestClientMessage?.Invoke(new MessageEventArgs
                {
                    Message = "未能解析服务器推送的消息"
                });
            }
        }

        public override async Task SendCommandAsync(string data, string message)
        {
            await SendCommandByStringAsync(data, message);
        }
    }
}
