using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace KuRuMi.Mio.Web.Filter
{
    /// <summary>
    /// 验证用户
    /// </summary>
    public class LoginFilter : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// 在action方法执行执行之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var session = filterContext.HttpContext.Session["username"];
            if (session == null)
            {
                var header = new UrlHelper(filterContext.RequestContext);
                var url = header.Action("Index","Home");
                filterContext.Result = new RedirectResult(url);
            }
        }

        /// <summary>
        /// 在action方法执行执行之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}