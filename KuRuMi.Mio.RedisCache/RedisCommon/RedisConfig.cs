using KuRuMi.Mio.DoMain.Infrastructure.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCommon
{
    /// <summary>
    /// redis配置
    /// </summary>
    public sealed class RedisConfig
    {
        public static readonly string config = ConfigurationManager.AppSettings["RedisConfig"];
        public static readonly string pwd = ConfigurationManager.AppSettings["RedisWord"];
        public static readonly string redisKey = ConfigurationManager.AppSettings["RedisKey"] ?? "";
        public static ConfigurationOptions option = new ConfigurationOptions()
        {
            //链接点
            EndPoints = {config},
            Password =pwd
        };
        public static string Key() {
            return redisKey;
        }
    }
}
