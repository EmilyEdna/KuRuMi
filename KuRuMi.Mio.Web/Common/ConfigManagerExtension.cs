using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.Web.Common
{
    /// <summary>
    /// 配置文件拓展
    /// </summary>
    public class ConfigManagerExtension
    {
        private static readonly string Id = ConfigurationManager.AppSettings["AppId"];
        private static readonly string hash = ConfigurationManager.AppSettings["AppSession"];
        /// <summary>
        /// APPID
        /// </summary>
        public static string AppId
        {
            get { return CryptographyExtension.Decrypts(Id); }
        }
        /// <summary>
        /// 解密密钥
        /// </summary>
        public static string SecretKey
        {
            get { return CryptographyExtension.Decrypts(hash); }
        }
    }
}