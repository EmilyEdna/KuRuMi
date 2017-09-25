using KuRuMi.Mio.DoMain.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCommon
{
    public class MongoDbConfig
    {
        private static readonly string host = ConfigurationManager.AppSettings["MongoDbConfig"];
        private static readonly string mdb = ConfigurationManager.AppSettings["DataBase"];

        public static string Host
        {
            get { return host; }
        }

        public static string DataBase
        {
            get { return mdb; }
        }
    }
}
