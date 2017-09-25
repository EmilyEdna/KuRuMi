using KuRuMi.Mio.DoMain.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// 配置拓展
    /// </summary>
    public class ConfigManagerExtension
    {
        private static readonly string hash = ConfigurationManager.AppSettings["AppSession"];
        /// <summary>
        /// 解密密钥
        /// </summary>
        public static string SecretKey
        {
            get { return CryptographyExtension.Decrypts(hash); }
        }
    }
}