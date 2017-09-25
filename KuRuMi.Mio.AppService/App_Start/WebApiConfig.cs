using KuRuMi.Mio.AppService.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KuRuMi.Mio.AppService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //注册过滤器
            config.Filters.Add(new SignSecretFilter());
            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //配置跨域请求
            #region 跨域配置
            var allowedOrigin = ConfigurationManager.AppSettings["Origins"];
            var allowedHeader = ConfigurationManager.AppSettings["Headers"];
            var allowedMethod = ConfigurationManager.AppSettings["Methods"];
            EnableCorsAttribute esa = new EnableCorsAttribute(allowedOrigin, allowedHeader, allowedMethod)
            {
                SupportsCredentials = true
            };
            config.EnableCors(esa);
            #endregion
            //指定返回JSON格式
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("json", "true", "application/json"));
        }
    }
}
