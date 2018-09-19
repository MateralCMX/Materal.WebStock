using Materal.WebStock.Commands;
using Materal.WebStock.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TestClient.WebStockClient;

namespace TestClient.UI
{
    public class TestClientHelper
    {
        public static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterCustomerService()
        {
            Services = new ServiceCollection();
            Services.AddCommandBus<string>(Assembly.Load("TestClient.CommandHandlers"));
            Services.AddEventBus<string>(Assembly.Load("TestClient.EventHandlers"));
            Services.AddSingleton<ITestClientWebStockClient, TestClientWebStockClientImpl>();
        }
        /// <summary>
        /// Bulid服务
        /// </summary>
        public static void BulidService()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
