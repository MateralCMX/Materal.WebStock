﻿using System;

namespace Materal.WebStock.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 配置事件参数
    /// </summary>
    public class ConfigEventArgs : EventArgs
    {
        /// <summary>
        /// 配置消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 配置对象
        /// </summary>
        public ClientConfigModel Config { get; set; }
    }
}
