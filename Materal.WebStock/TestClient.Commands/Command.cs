using Materal.WebStock.Commands;
using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;
using System;

namespace TestClient.Commands
{
    public abstract class Command : IWebStockClientCommand
    {
        /// <summary>
        /// 转换为文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return this.MToJson();
            }
            catch (MException ex)
            {
                throw new CommandException("转换为命令Json出错", ex);
            }
        }
    }
}
