using Materal.WebStockClient.EventHandlers;
using Materal.WebStockClient.Events.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.WebStockClient.Events
{
    public class WebStockClientEventBus<T> : IWebStockClientEventBus<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebStockClientEventHandlerHelper _handlerHelper;
        public WebStockClientEventBus(IServiceProvider serviceProvider, IWebStockClientEventHandlerHelper handlerHelper)
        {
            _serviceProvider = serviceProvider;
            _handlerHelper = handlerHelper;
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <param name="data">数据</param>
        public void Send(string handlerName, T data)
        {
            GetHandler(handlerName).Excute(data);
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public async Task SendAsync(string handlerName, T data)
        {
            await GetHandler(handlerName).ExcuteAsync(data);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <returns>处理器对象</returns>
        public IWebStockClientEventHandler<T> GetHandler(string handlerName)
        {
            var handlerType = _handlerHelper.GetHandlerType(handlerName);
            var handler = (IWebStockClientEventHandler<T>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new WebStockClientEventException("未找到对应处理器");
            return handler;
        }
    }
}
