using KuRuMi.Mio.BootStarp.IIServce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuRuMi.Mio.DoMain.Repository.ModelRepository;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using KuRuMi.Mio.DoMain.Model.Model;
using AutoMapper;

namespace KuRuMi.Mio.BootStarp.IServiceImpl
{
    /// <summary>
    /// 图片文件服务
    /// </summary>
    public class ImgFilesServiceImpl: IImgFilesService
    {

        #region 领域仓储
        protected ImgFilesRepositoryImpl img;
        #endregion

        #region  赋值
        /// <summary>
        /// 赋值实例
        /// </summary>
        public void GetValues()
        {
            img = IocManager.Instance.Resolve<ImgFilesRepositoryImpl>();
        }
        #endregion

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="idto"></param>
        public void SaveImg(ImgFilesDTO dto)
        {
            var entity = Mapper.Map<ImgFilesDTO, ImgFiles>(dto);
            img.Add(entity);
        }
    }
}
