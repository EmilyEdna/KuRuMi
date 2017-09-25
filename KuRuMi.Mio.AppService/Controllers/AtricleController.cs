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
    /// 文章控制器
    /// </summary>
    public class AtricleController : ApiController
    {
        protected AtricleServiceImpl service = null;
        public AtricleController() {
            service = IocManager.Instance.Resolve<AtricleServiceImpl>();
            service.GetValues();
        }

        /// <summary>
        /// 保存文章内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveAtricle(AtricleDTO dto)
        {
            string result = service.SaveAtricle(dto);
            return HttpResponseExtension.toJson(result);
        }
    }
}
