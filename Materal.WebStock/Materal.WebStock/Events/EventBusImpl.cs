using Materal.WebStock.EventHandlers;
using Materal.WebStock.Events.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Materal.WebStock.Events
{
    public class EventBusImpl<T> : IEventBus<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHandlerHelper _eventHandlerHelper;
        public EventBusImpl(IServiceProvider serviceProvider, IEventHandlerHelper eventHandlerHelper)
        {
            _serviceProvider = serviceProvider;
            _eventHandlerHelper = eventHandlerHelper;
        }
        public void Send(string handlerName, T data)
        {
            GetHandler(handlerName).Excute(data);
        }
        public async Task SendAsync(string handlerName, T data)
        {
            await GetHandler(handlerName).ExcuteAsync(data);
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <returns>处理器对象</returns>
        public IEventHandler<T> GetHandler(string handlerName)
        {
            Type handlerType = _eventHandlerHelper.GetHandlerType(handlerName);
            var handler = (IEventHandler<T>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new EventException("未找到对应处理器");
            return handler;
        }
    }
}
