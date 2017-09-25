using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public static class UnitExtension
    {
        private static readonly ILog log = LogManager.GetLogger("info");
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(string msg) {
            log.Info(msg);
        }
        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(Exception ex)
        {
            log.Error("错误信息", ex);
        }
    }
}
