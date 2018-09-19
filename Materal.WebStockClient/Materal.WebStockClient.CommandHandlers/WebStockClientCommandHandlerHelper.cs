using System;
using System.Collections.Generic;
using Materal.WebStockClient.CommandHandlers.Model;

namespace Materal.WebStockClient.CommandHandlers
{
    public class WebStockClientCommandHandlerHelper: IWebStockClientCommandHandlerHelper
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        private readonly List<Type> _commandTypes;

        public WebStockClientCommandHandlerHelper(List<Type> types) => _commandTypes = types;

        public IEnumerable<Type> GetAllHandlerTypes()
        {
            return _commandTypes;
        }
        public Type GetHandlerType(string handlerName)
        {
            var allHandler = GetAllHandlerTypes();
            foreach (var item in allHandler)
            {
                if (item.Name.Equals(handlerName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new WebStockClientCommandHandlerException($"未找到处理器{handlerName}");
        }
    }
}
