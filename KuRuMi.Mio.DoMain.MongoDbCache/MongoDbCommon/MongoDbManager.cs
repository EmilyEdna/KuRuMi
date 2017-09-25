using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCommon
{
    public class MongoDbManager
    {
        private static IMongoDatabase db = null;
        private static readonly object locker = new object();
        /// <summary>
        /// 使用单列模式创建连接
        /// </summary>
        /// <returns></returns>
        public static IMongoDatabase CreateDb()
        {
            if (db == null)
            {
                lock (locker)
                {
                    if (db == null)
                    {
                        MongoClient Client = new MongoClient(MongoDbConfig.Host);
                        db = Client.GetDatabase(MongoDbConfig.DataBase);
                    }
                }
            }
            return db;
        }
    }
}
