using KuRuMi.Mio.DoMain.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// 判断签名算法
    /// </summary>
    public class SignExtension
    {
        private static readonly string keys = "Kurumi";
        /// <summary>
        /// 判断签名算法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="url"></param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static bool Validate(string appId, string sign)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ConfigManagerExtension.SecretKey);
            string signs = string.Empty;
            if (appId != "1000")
                signs = CryptographyExtension.Decrypts(appId) + keys;
            else
                signs = appId + keys;
            byte[] val = Encoding.UTF8.GetBytes(string.Concat(signs.OrderBy(c => c)));//排序
            string key = null;
            using (HMACSHA512 SecretKey = new HMACSHA512(bytes))
            {
                var SecretKeyBytes = SecretKey.ComputeHash(val);
                key = Convert.ToBase64String(SecretKeyBytes);
            }
            return (sign.Equals(key, StringComparison.Ordinal));
        }
    }
}