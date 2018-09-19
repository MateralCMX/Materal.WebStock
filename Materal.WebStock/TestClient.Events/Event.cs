using Materal.WebStock.Events;
using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;
using MateralTools.MVerify;

namespace TestClient.Events
{
    public class Event: IEvent<string>
    {
        /// <summary>
        /// 命令名
        /// </summary>
        public string orginalCmd { get; set; }
        /// <summary>
        /// 事件名
        /// </summary>
        public string @event{ get;set; }
        /// <summary>
        /// 命令名
        /// </summary>
        public string OrginalCmd => char.ToUpper(orginalCmd[0]) + orginalCmd.Substring(1);
        /// <summary>
        /// 命令名
        /// </summary>
        public string EventName => (orginalCmd.MIsNullOrEmpty() ? (char.ToUpper(@event[0]) + @event.Substring(1)) : OrginalCmd);

        /// <summary>
        /// 处理器名称
        /// </summary>
        public string HandlerName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
