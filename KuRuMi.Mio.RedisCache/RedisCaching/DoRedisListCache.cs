using KuRuMi.Mio.DoMain.RedisCache.RedisCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCaching
{
    /// <summary>
    /// 表示list的操作
    /// </summary>
    public sealed class DoRedisListCache : IRedisCaching
    {
        private RedisBase redis = null;
        public DoRedisListCache()
        {
            redis = new RedisBase();
        }

        #region 同步执行
        /// <summary>
        /// 移除List内部指定的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void ListRemove<T>(string key, T val)
        {
            key = redis.AddKey(key);
            redis.DoSave(db => db.ListRemove(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 获取指定Key的List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
           {
               var val = db.ListRange(key);
               return redis.ConvertList<T>(val);
           });
        }

        /// <summary>
        /// 插入（入队）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void ListRightPush<T>(string key, T val)
        {
            key = redis.AddKey(key);
            redis.DoSave(db => db.ListRightPush(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 取出（出队）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
            {
                var val = db.ListRightPop(key);
                return redis.ConvertObj<T>(val);
            });
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void ListLeftPush<T>(string key, T val)
        {
            key = redis.AddKey(key);
            redis.DoSave(db => db.ListLeftPush(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db =>
            {
                var val = db.ListLeftPop(key);
                return redis.ConvertObj<T>(val);
            });
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetListLength(string key)
        {
            key = redis.AddKey(key);
            return redis.DoSave(db => db.ListLength(key));
        }
        #endregion

        #region 异步执行
        /// <summary>
        /// 异步移除List内部指定的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public async Task<long> ListRemoveAsync<T>(string key, T val)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.ListRemoveAsync(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 异步获取指定Key的List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db => db.ListRangeAsync(key));
            return redis.ConvertList<T>(val);
        }

        /// <summary>
        /// 异步插入（入队）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public async Task<long> ListRightPushAsync<T>(string key, T val)
        {
            key = redis.AddKey(key);
           return await redis.DoSave(db => db.ListRightPushAsync(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 异步取出（出队）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db => db.ListRightPopAsync(key));
            return redis.ConvertObj<T>(val);
        }

        /// <summary>
        /// 异步入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public async Task<long> ListLeftPushAsync<T>(string key, T val)
        {
            key = redis.AddKey(key);
            return await  redis.DoSave(db => db.ListLeftPushAsync(key, redis.ConvertJson(val)));
        }

        /// <summary>
        /// 异步出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            key = redis.AddKey(key);
            var val = await redis.DoSave(db => db.ListLeftPopAsync(key));
            return redis.ConvertObj<T>(val);
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> GetListLengthAsync(string key)
        {
            key = redis.AddKey(key);
            return await redis.DoSave(db => db.ListLengthAsync(key));
        }
        #endregion
    }
}
