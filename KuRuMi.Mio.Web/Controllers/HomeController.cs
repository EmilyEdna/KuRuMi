using KuRuMi.Mio.Web.Common;
using KuRuMi.Mio.Web.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuRuMi.Mio.Web.Controllers
{
    /// <summary>
    /// Home控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 后台管理
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Admin(string Id)
        {
            TempData["UserId"] = Id;
            return View();
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FileInput()
        {
            string Path = Server.MapPath("~/Images");
            HttpFileCollectionBase files = Request.Files;
            NameValueCollection param = Request.Form;
            string Folder = DateTime.Now.ToString("yyyy-MM-dd");
            //保存文件的绝对路劲
            string Road = Path + @"\" + Folder + @"\";
            //保存在数据库的映射路劲
            string SqlMapPath = @"~/Images/" + Folder + @"/";
            //文件名称
            string FileName = string.Empty;
            //数据库的完整路径
            string SqlTotalPath = string.Empty;
            if (!Directory.Exists(Road))
            {
                Directory.CreateDirectory(Road);
            }
            //遍历文件
            if (files.Count > 0 && files != null)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    FileName = files[i].FileName;
                    files[i].SaveAs(Road + FileName);
                }
            }
            SqlTotalPath = SqlMapPath + FileName;
            string data = JsonConvert.SerializeObject(
                  new
                  {
                      imgId = param["UserId"],
                      imgName = FileName,
                      imgUrl = SqlTotalPath
                  });
            string result = SignApiPostExtension.PostAsyc<string>("http://localhost:13292/Api/ImgFiles/SaveFileToSql", data, ConfigManagerExtension.AppId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 博客内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult BlogsContent(string content)
        {
            var form = Request.Form;
            var data = JsonConvert.SerializeObject(
             new
             {
                 blogsId = form["blogsId"],
                 title = form["title"],
                 content= content,
                 category=form["category"],
                 tips = form["tips"],
                 date=form["date"]
             });
            var result = SignApiPostExtension.PostAsyc<string>("http://localhost:13292/Api/Blogs/SaveBlogs", data, ConfigManagerExtension.AppId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 文章内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AtricleContent(string content) {
            var form = Request.Form;
            var data = JsonConvert.SerializeObject(
             new
             {
                 atricleId = form["atricleId"],
                 title = form["title"],
                 content = content,
                 category = form["category"],
                 tips = form["tips"],
                 date = form["date"]
             });
            var result = SignApiPostExtension.PostAsyc<string>("http://localhost:13292/Api/Atricle/SaveAtricle", data, ConfigManagerExtension.AppId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}