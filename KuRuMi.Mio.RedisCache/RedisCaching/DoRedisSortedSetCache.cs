using KuRuMi.Mio.DoMain.RedisCache.RedisCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCaching
{
    /// <summary>
    /// 表示SortedSet的操作
    /// </summary>
    public sealed class DoRedisSortedSetCache : IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisSortedSetCache()
        {
            redis = new RedisBase();
        }
        #region 同步执行
        /// <summary>
        /// 无序添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T val, double score)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SortedSetAdd(key, redis.ConvertJson<T>(val), score));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T val)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SortedSetRemove(key, redis.ConvertJson<T>(val)));
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => 
            {
                var val =  db.SortedSetRangeByRank(key);
                return redis.ConvertList<T>(val);
            });
        }

        /// <summary>
        ///  获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.SortedSetLength(key));

        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 异步无序添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T val, double score)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SortedSetAddAsync(key, redis.ConvertJson<T>(val), score));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SortedSetRemoveAsync(key, redis.ConvertJson<T>(val)));
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByRankAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db=>db.SortedSetRangeByRankAsync(key));
            return redis.ConvertList<T>(val);
        }

        /// <summary>
        ///  获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.SortedSetLengthAsync(key));

        }
        #endregion
    }
}
