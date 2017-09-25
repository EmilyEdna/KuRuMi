using KuRuMi.Mio.DoMain.RedisCache.RedisCommon;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCaching
{
    /// <summary>
    /// 表示hash表的操作
    /// </summary>
    public sealed class DoRedisHashCache : IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisHashCache()
        {
            redis = new RedisBase();
        }
        #region 同步执行
        /// <summary>
        /// 是否被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.HashExists(key, dataKey));
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T val)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
            {
                string json = redis.ConvertJson(val);
                return db.HashSet(key, dataKey, json);
            });
        }

        /// <summary>
        /// 从hash表中移除数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.HashDelete(key, dataKey));
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public long HashRemove(string key, List<RedisValue> dataKey)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.HashDelete(key, dataKey.ToArray()));
        }

        /// <summary>
        /// 从hash表中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
            {
                var val = db.HashGet(key, dataKey);
                return redis.ConvertObj<T>(val);
            });
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.HashIncrement(key, dataKey, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeyKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.HashDecrement(key, dataKey, val));
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> HashKeys<T>(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
            {
                var val = db.HashKeys(key);
                return redis.ConvertList<T>(val);
            });
        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 异步是否被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string dataKey)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.HashExistsAsync(key, dataKey));
        }

        /// <summary>
        /// 异步存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db =>
            {
                string json = redis.ConvertJson(val);
                return db.HashSetAsync(key, dataKey, json);
            });
        }

        /// <summary>
        /// 异步从hash表中移除数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.HashDeleteAsync(key, dataKey));
        }

        /// <summary>
        /// 异步移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<long> HashRemoveAsync(string key, List<RedisValue> dataKey)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.HashDeleteAsync(key, dataKey.ToArray()));
        }

        /// <summary>
        /// 从hash表中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey)
        {
            key = redis.AddKey(key);
            string val = await redis.DoSave(db => db.HashGetAsync(key, dataKey));
            return redis.ConvertObj<T>(val);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<double> HashIncrementAsync(string key, string dataKey, double val = 1)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.HashIncrementAsync(key, dataKey, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeyKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<double> HashDecrementAsync(string key, string dataKey, double val = 1)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.HashDecrementAsync(key, dataKey, val));
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> HashKeysAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db => db.HashKeysAsync(key));
            return redis.ConvertList<T>(val);
        }
        #endregion
    }
}
