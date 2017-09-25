using KuRuMi.Mio.DoMain.RedisCache.RedisCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCaching
{
    /// <summary>
    /// 表示Key的操作
    /// </summary>
    public sealed class DoRedisKeyCache: IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisKeyCache()
        {
            redis = new RedisBase();
        }

        #region 同步执行
        /// <summary>
        /// 删除单个Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.KeyDelete(key));
        }
        /// <summary>
        /// 删除多个Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long KeyDelete(List<string> key)
        {
            List<string> Keys = key.Select(redis.AddKey).ToList();
            return redis.DoSave(db => db.KeyDelete(redis.ConvertRedisKeys(Keys)));
        }

        /// <summary>
        /// 重命名Key
        /// </summary>
        /// <param name="key">old key name</param>
        /// <param name="newKey">new key name</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.KeyRename(key,newKey));
        }

        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.KeyExpire(key, exp));
        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 异步删除单个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.KeyDeleteAsync(key));
        }

        /// <summary>
        /// 异步删除多个Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(List<string> key)
        {
            List<string> Keys = key.Select(redis.AddKey).ToList();
            return await redis.DoSave(db => db.KeyDeleteAsync(redis.ConvertRedisKeys(Keys)));
        }

        /// <summary>
        ///  异步重命名Key
        /// </summary>
        /// <param name="key">old key name</param>
        /// <param name="newKey">new key name</param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.KeyRenameAsync(key, newKey));
        }

        /// <summary>
        /// 异步设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? exp = default(TimeSpan?))
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.KeyExpireAsync(key, exp));
        }
        #endregion
    }
}
