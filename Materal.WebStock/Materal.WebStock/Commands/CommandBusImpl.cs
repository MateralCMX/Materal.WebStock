using Materal.WebStock.CommandHandlers;
using Materal.WebStock.Commands.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Materal.WebStock.Commands
{
    public class CommandBusImpl<T> : ICommandBus<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandHandlerHelper _handlerHelper;
        public CommandBusImpl(IServiceProvider serviceProvider, ICommandHandlerHelper handlerHelper)
        {
            _serviceProvider = serviceProvider;
            _handlerHelper = handlerHelper;
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
        public ICommandHandler<T> GetHandler(string handlerName)
        {
            Type handlerType = _handlerHelper.GetHandlerType(handlerName);
            var handler = (ICommandHandler<T>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new CommandException("未找到对应处理器");
            return handler;
        }
    }
}
