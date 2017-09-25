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
    /// 表示string的操作
    /// </summary>
    public sealed class DoRedisStringCache : IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisStringCache()
        {
            redis = new RedisBase();
        }

        #region 同步执行
        /// <summary>
        /// 单个保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">值</param>
        /// <param name="exp">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string val, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.StringSet(key, val, exp));
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> KeyVal)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newkey = KeyVal.Select(k => new KeyValuePair<RedisKey, RedisValue>(redis.AddKey(k.Key), k.Value)).ToList();
            return redis.DoSave(db => db.StringSet(newkey.ToArray()));
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            string json = redis.ConvertJson(obj);
            return redis.DoSave(db => db.StringSet(key, json, exp));
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.StringGet(key));
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            key = redis.AddKey(key);
            var val = redis.DoSave(db => db.StringGet(key));
            return redis.ConvertObj<T>(val);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负数</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.StringIncrement(key, val));
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负数</param>
        /// <returns>增长后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.StringDecrement(key, val));
        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 异步保存单个
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string val, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.StringSetAsync(key, val, exp));
        }
        /// <summary>
        /// 异步保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> KeyVal)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newkey = KeyVal.Select(k => new KeyValuePair<RedisKey, RedisValue>(redis.AddKey(k.Key), k.Value)).ToList();
            return await redis.DoSave(db => db.StringSetAsync(newkey.ToArray()));
        }

        /// <summary>
        /// 异步保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            string json = redis.ConvertJson(obj);
            return await redis.DoSave(db => db.StringSetAsync(key, json, exp));
        }

        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.StringGetAsync(key));
        }

        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db => db.StringGetAsync(key));
            return redis.ConvertObj<T>(val);
        }

        /// <summary>
        /// 异步为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负数</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.StringIncrementAsync(key, val));
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负数</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.StringDecrementAsync(key, val));
        }
        #endregion
    }
}
