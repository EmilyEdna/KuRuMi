using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCommon
{
    public class RedisBase
    {
        private static ConnectionMultiplexer db = null;
        private static string key = string.Empty;
        public RedisBase()
        {
            db = RedisManager.Instance;
        }

        #region 辅助方法
        /// <summary>
        /// 添加名称
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public string AddKey(string old)
        {
            var fixkey = key ?? RedisConfig.Key();
            return fixkey + old;
        }
        /// <summary>
        /// 执行保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T DoSave<T>(Func<IDatabase, T> func)
        {
            return func(db.GetDatabase());
        }
        /// <summary>
        /// 对象转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public string ConvertJson<T>(T val)
        {
            return val is string ? val.ToString() : JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// 值转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public T ConvertObj<T>(RedisValue val)
        {
            return JsonConvert.DeserializeObject<T>(val);
        }
        /// <summary>
        /// 集合值转集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public List<T> ConvertList<T>(RedisValue[] val)
        {
            List<T> result = new List<T>();
            foreach (var item in val)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 集合转key
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public RedisKey[] ConvertRedisKeys(List<string> val)
        {
            return val.Select(k => (RedisKey)k).ToArray();
        }
        #endregion
    }
}
