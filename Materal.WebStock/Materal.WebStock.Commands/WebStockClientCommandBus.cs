using Materal.WebStock.CommandHandlers;
using Materal.WebStock.Commands.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Materal.WebStock.Commands
{
    public class WebStockClientCommandBus<T> : IWebStockClientCommandBus<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebStockClientCommandHandlerHelper _handlerHelper;
        public WebStockClientCommandBus(IServiceProvider serviceProvider, IWebStockClientCommandHandlerHelper handlerHelper)
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
        public IWebStockClientCommandHandler<T> GetHandler(string handlerName)
        {
            var handlerType = _handlerHelper.GetHandlerType(handlerName);
            var handler = (IWebStockClientCommandHandler<T>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new WebStockClientCommandException("未找到对应处理器");
            return handler;
        }
    }
}
