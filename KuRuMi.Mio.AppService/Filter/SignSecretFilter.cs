using KuRuMi.Mio.AppService.Common;
using KuRuMi.Mio.AppService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace KuRuMi.Mio.AppService.Filter
{
    /// <summary>
    /// 登陆过滤器
    /// </summary>
    public class SignSecretFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ResultMsg resultMsg = null;
            string appId = string.Empty;
            string sign = string.Empty;
            if (actionContext.Request.Headers.Contains("appid"))
            {
                appId = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("appid").FirstOrDefault());
            }
            if (actionContext.Request.Headers.Contains("sign"))
            {
                sign = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("sign").FirstOrDefault());
            }
            //判断操作的controller名称是否是图片上传
            if (actionContext.ActionDescriptor.ActionName == "SaveFileToSql")
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            //判断请求头是否包含以下参数
            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(sign))
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
                resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
                resultMsg.Data = "";
                actionContext.Response = HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
                base.OnActionExecuting(actionContext);
                return;
            }
            //验证签名算法
            bool result = SignExtension.Validate(appId, sign);
            if (!result)
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeEnum.HttpRequestError;
                resultMsg.Info = StatusCodeEnum.HttpRequestError.GetEnumText();
                resultMsg.Data = "";
                actionContext.Response = HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
                base.OnActionExecuting(actionContext);
                return;
            }
        }
    }
}