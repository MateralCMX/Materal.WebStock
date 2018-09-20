using Materal.WebStock;
using Materal.WebStock.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestClient.Events;
using TestClient.WebStockClient.Model;

namespace TestClient.WebStockClient
{
    public class TestClientClientImpl : ClientImpl, ITestClientClient
    {
        private readonly IServiceProvider _serviceProvider;
        public TestClientClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task HandleEventAsync(Event eventM)
         {
            try
            {
                var commandBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                await commandBus.SendAsync(eventM);
            }
            catch (Exception ex)
            {
                throw new TestClientClientException("未能解析事件", ex);
            }
        }
    }
}
