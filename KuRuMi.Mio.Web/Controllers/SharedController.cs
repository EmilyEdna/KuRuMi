using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using KuRuMi.Mio.Web.Common;

namespace KuRuMi.Mio.Web.Controllers
{
    /// <summary>
    /// 模板控制器
    /// </summary>
    public class SharedController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult LoginView(string email, string password)
        {
            var data = JsonConvert.SerializeObject(new { email = email, passWord = password });
            //var name = SignApiPostExtension.Post<string>("http://localhost:13292/Api/User/UserLogin", data, ConfigManagerExtension.AppId);
            var Dto = SignApiPostExtension.PostAsyc<List<string>>("http://localhost:13292/Api/User/UserLogin", data, ConfigManagerExtension.AppId);
            try
            {
                if (Dto.Count != 0)
                {
                    Session["username"] = Dto[0].ToString();
                    TempData["Id"] = Dto[1].ToString();
                    return Json(new { info = "登录成功！", Id = Dto[1].ToString() }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { info = "登录失败！" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception)
            {

               return Json(new { info = "登录失败！" }, JsonRequestBehavior.DenyGet);
            }
         
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}