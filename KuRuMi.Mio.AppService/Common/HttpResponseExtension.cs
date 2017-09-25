using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// HTTP响应消息
    /// </summary>
    public class HttpResponseExtension
    {
        public static HttpResponseMessage toJson(object obj)
        {
            string json;
            if (obj is string || obj is Char)
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(obj.ToString());
            }
            else
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
    }
}