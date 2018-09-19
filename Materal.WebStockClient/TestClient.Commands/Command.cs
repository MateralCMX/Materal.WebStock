using Materal.WebStockClient.Commands;
using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;
using System;

namespace TestClient.Commands
{
    public abstract class Command : IWebStockClientCommand
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        /// <param name="command"></param>
        protected Command(string command)
        {
            cmd = command;
        }
#pragma warning disable IDE1006 // 命名样式
        /// <summary>
        /// 命令文本
        /// </summary>
        public string cmd { get; private set; }
#pragma warning restore IDE1006 // 命名样式
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
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
