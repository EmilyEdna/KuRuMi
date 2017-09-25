using KuRuMi.Mio.AppService.Common;
using KuRuMi.Mio.BootStarp.IServiceImpl;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KuRuMi.Mio.AppService.Controllers
{
    /// <summary>
    /// 图片上传API
    /// </summary>
    public class ImgFilesController : ApiController
    {
        protected ImgFilesServiceImpl service = null;

        public ImgFilesController()
        {
            service = IocManager.Instance.Resolve<ImgFilesServiceImpl>();
            service.GetValues();
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveFileToSql(dynamic obj)
        {
            ImgFilesDTO dto = new ImgFilesDTO();
            dto.imgId = Guid.Parse(Convert.ToString(obj.imgId));
            dto.imgName = Convert.ToString(obj.imgName);
            dto.imgUrl = Convert.ToString(obj.imgUrl);
            service.SaveImg(dto);
            return HttpResponseExtension.toJson("上传成功！");
        }
    }
}
