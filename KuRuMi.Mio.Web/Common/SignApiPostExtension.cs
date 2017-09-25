using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KuRuMi.Mio.Web.Common
{
    /// <summary>
    /// 签名验证请求
    /// </summary>
    public class SignApiPostExtension
    {

        private static readonly string keys = "Kurumi";

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="timeSpan"></param>
        /// <param name="nonce"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetSignature()
        {
            string sign = ConfigManagerExtension.AppId + keys;//拼接参数
            byte[] val = Encoding.UTF8.GetBytes(string.Concat(sign.OrderBy(c => c)));//排序
            byte[] bytes = Encoding.UTF8.GetBytes(ConfigManagerExtension.SecretKey);//秘钥的bytes
            using (HMACSHA512 SecretKey = new HMACSHA512(bytes))
            {
                var SecretKeyBytes = SecretKey.ComputeHash(val);
                string signkey = Convert.ToBase64String(SecretKeyBytes); //返回加密后的key
                return signkey;
            }

        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">地址</param>
        /// <param name="data">数据</param>
        /// <param name="appid">标识码</param>
        /// <returns></returns>
        public static T Post<T>(string url, string data, string appid)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            //加入到头信息中
            request.Headers.Add("appid", appid);//当前的用户的请求id
            request.Headers.Add("sign", GetSignature());//签名验证

            //写数据
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);

            //读数据
            request.Timeout = 30000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();

            //关闭流
            reqstream.Close();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();

            return JsonConvert.DeserializeObject<T>(strResult);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T PostAsyc<T>(string url, string data, string appid)
        {
            HttpContent httpContent = new StringContent(data);
            httpContent.Headers.Add("appid", appid);//当前的用户的请求id
            httpContent.Headers.Add("sign", GetSignature());//签名验证
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            string dataJson = client.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(dataJson);
        }
    }
}