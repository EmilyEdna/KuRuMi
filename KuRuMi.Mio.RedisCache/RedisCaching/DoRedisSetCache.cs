using KuRuMi.Mio.DoMain.RedisCache.RedisCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCaching
{
    /// <summary>
    /// 表示set的操作
    /// </summary>
    public sealed class DoRedisSetCache : IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisSetCache()
        {
            redis = new RedisBase();
        }
        #region 同步执行
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetAdd(string key, string val)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SetAdd(key, val));
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SetLength(string key) {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SetLength(key));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetContains(string key, string val) {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SetContains(key, val));
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetRemove(string key, string val) {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SetRemove(key, val));
        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> SetAddAsync(string key, string val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SetAddAsync(key, val));
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string key)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SetLengthAsync(key));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> SetContainsAsync(string key, string val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SetContainsAsync(key, val));
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> SetRemoveAsync(string key, string val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SetRemoveAsync(key, val));
        }
        #endregion
    }
}
