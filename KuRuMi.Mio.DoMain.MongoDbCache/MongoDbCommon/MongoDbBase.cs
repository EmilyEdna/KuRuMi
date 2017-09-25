using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCommon
{
    public class MongoDbBase
    {
        private IMongoDatabase db = null;
        public IMongoDatabase Db { get => db; }
        public MongoDbBase()
        {
            db = MongoDbManager.CreateDb();
        }
    }
}
