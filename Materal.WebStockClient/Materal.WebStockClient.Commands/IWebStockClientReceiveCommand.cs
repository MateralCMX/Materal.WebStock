using System;
using System.Collections.Generic;
using System.Text;

namespace Materal.WebStockClient.Commands
{
    public interface IWebStockClientReceiveCommand
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; }
    }
}
