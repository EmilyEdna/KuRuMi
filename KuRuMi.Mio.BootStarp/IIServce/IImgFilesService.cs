using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.BootStarp.IIServce
{
    /// <summary>
    /// 图片文件服务接口
    /// </summary>
    public interface IImgFilesService: IService
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="dto"></param>
        void SaveImg(ImgFilesDTO dto);

    }
}
