using KuRuMi.Mio.DoMain.RedisCache.RedisCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.RedisCache
{
    /// <summary>
    /// 包装器
    /// </summary>
    public sealed class RedisCachingImpl
    {
        public  Lazy<DoRedisHashCache> RedisHash = null;
        public  Lazy<DoRedisKeyCache> RedisKey = null;
        public  Lazy<DoRedisListCache> RedisList = null;
        public  Lazy<DoRedisSetCache> RedisSet = null;
        public  Lazy<DoRedisSortedSetCache> RedisSortedSet = null;
        public  Lazy<DoRedisStringCache> RedisString = null;
        public RedisCachingImpl() {
            RedisHash = new Lazy<DoRedisHashCache>();
            RedisKey = new Lazy<DoRedisKeyCache>();
            RedisList = new Lazy<DoRedisListCache>();
            RedisSet = new Lazy<DoRedisSetCache>();
            RedisSortedSet = new Lazy<DoRedisSortedSetCache>();
            RedisString = new Lazy<DoRedisStringCache>();
        }
    }
}
