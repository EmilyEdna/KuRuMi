using KuRuMi.Mio.AppService.Common;
using KuRuMi.Mio.BootStarp.IServiceImpl;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KuRuMi.Mio.AppService.Controllers
{
    /// <summary>
    /// 博客控制器
    /// </summary>
    public class BlogsController : ApiController
    {
       protected BlogsServiceImpl service = null;

        public BlogsController() {
            service = IocManager.Instance.Resolve<BlogsServiceImpl>();
            service.GetValues();
        }

        /// <summary>
        /// 保存博客内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveBlogs(BlogsDTO dto)
        {
          string result =  service.SaveBlogs(dto);
          return HttpResponseExtension.toJson(result);
        }
        /// <summary>
        /// 获取博客信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetBlogsInfo([FromBody]string Id)
        {
            try
            {
                var result = service.GetBlogsInfo(Guid.Parse(Id));
                return HttpResponseExtension.toJson(result);
            }
            catch (Exception)
            {
              return HttpResponseExtension.toJson("请先登录!");
            }
        }
    }
}
