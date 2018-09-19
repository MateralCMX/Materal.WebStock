using System;
using System.Collections.Generic;
using Materal.WebStockClient.EventHandlers.Model;

namespace Materal.WebStockClient.EventHandlers
{
    public class WebStockClientEventHandlerHelper: IWebStockClientEventHandlerHelper
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        private readonly List<Type> _commandTypes;

        public WebStockClientEventHandlerHelper(List<Type> types) => _commandTypes = types;

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
            throw new WebStockClientEventHandlerException($"未找到处理器{handlerName}");
        }
    }
}
