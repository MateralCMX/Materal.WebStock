﻿using MateralTools.MVerify;
using System.Collections.Generic;
using System.Text;

namespace Materal.WebStock.Model
{
    /// <summary>
    /// 配置模型
    /// </summary>
    public class ClientConfigModel
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 编码类型
        /// </summary>
        public Encoding EncodingType { get; set; }
        /// <summary>
        /// 服务器消息最大长度
        /// </summary>
        public int ServerMessageMaxLength { get; set; }
        /// <summary>
        /// 验证合法性
        /// </summary>
        /// <param name="messages">验证消息</param>
        /// <returns>验证结果</returns>
        public bool Verification(out List<string> messages)
        {
            messages = new List<string>();
            var isOk = true;
            if (EncodingType == null)
            {
                isOk = false;
                messages.Add("未指定编码类型");
            }
            if (Url.MIsNullOrEmpty())
            {
                isOk = false;
                messages.Add("未指定URL地址");
            }
            if (!Url.MIsURL())
            {
                isOk = false;
                messages.Add("URL地址格式错误");
            }
            return isOk;
        }
    }
}
