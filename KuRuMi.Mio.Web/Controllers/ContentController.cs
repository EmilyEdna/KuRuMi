using KuRuMi.Mio.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuRuMi.Mio.Web.Controllers
{
    /// <summary>
    /// 内容控制器
    /// </summary>
    public class ContentController : Controller
    {
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Atricle()
        {
            return View();
        }
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetUserInfoById(string id)
        {
            string data = JsonConvert.SerializeObject(new { Id = Guid.Parse(id) });
            string result = SignApiPostExtension.Post<string>("http://localhost:13292/Api/User/GetUserInfoById", data, ConfigManagerExtension.AppId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}