using System.Threading.Tasks;

namespace Materal.WebStock.EventHandlers
{
    /// <summary>
    /// 命令处理器接口
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IEventHandler<in T>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        Task ExcuteAsync(T data);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="data">数据</param>
        void Excute(T data);
    }
}
