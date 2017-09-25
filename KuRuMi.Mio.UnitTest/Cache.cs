using KuRuMi.Mio.DoMain.Model.Model;
using KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCache;
using KuRuMi.Mio.DoMain.RedisCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.UnitTest
{
    [TestClass]
    public class Cache
    {
        [TestMethod]
        public void TestMethod() {
            MongoDbCacheService mdb = new MongoDbCacheService();
            Sys_User l = new Sys_User();
            l.PassWord = "1";
            l.UserName = "1";
            l.Email = "21";
          var s =  mdb.UpdateMany(a => a.Id == Guid.Parse("f8254fcd-bc4c-e711-89d0-00e04c3a7a18"), l);
            
        }
    }
}
