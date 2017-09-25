using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.AppService.Models
{
    /// <summary>
    /// 返回的状态消息
    /// </summary>
    public class ResultMsg
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}