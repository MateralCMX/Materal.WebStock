using System.Windows.Input;
using Materal.WebStock.Commands;
using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;

namespace TestClient.Commands
{
    public abstract class Command : ICommand<string>
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

        public string HandelerName { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
