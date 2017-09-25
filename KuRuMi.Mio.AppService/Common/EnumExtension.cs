using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// 枚举拓展
    /// </summary>
    public static class EnumExtension
    {
        private static Dictionary<string, Dictionary<string, string>> enumCache;

        private static Dictionary<string, Dictionary<string, string>> EnumCache
        {
            get
            {
                if (enumCache == null)
                {
                    enumCache = new Dictionary<string, Dictionary<string, string>>();
                }
                return enumCache;
            }
            set { enumCache = value; }
        }
        /// <summary>
        /// 获取枚举上的特性名称
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumText(this Enum en)
        {
            string enString = string.Empty;
            if (null == en) return enString;
            var type = en.GetType();
            enString = en.ToString();
            if (!EnumCache.ContainsKey(type.FullName))
            {
                var fields = type.GetFields();
                Dictionary<string, string> temp = new Dictionary<string, string>();
                foreach (var item in fields)
                {
                    var attrs = item.GetCustomAttributes(typeof(TextAttributeExtension), false);
                    if (attrs.Length == 1)
                    {
                        var v = ((TextAttributeExtension)attrs[0]).Value;
                        temp.Add(item.Name, v);
                    }
                }
                EnumCache.Add(type.FullName, temp);
            }
            if (EnumCache[type.FullName].ContainsKey(enString))
            {
                return EnumCache[type.FullName][enString];
            }
            return enString;
        }
    }
}