using KuRuMi.Mio.BootStarp.IIServce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using KuRuMi.Mio.DoMain.Repository.ModelRepository;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Model.Model;
using AutoMapper;

namespace KuRuMi.Mio.BootStarp.IServiceImpl
{
    /// <summary>
    /// 文章服务接口实现类
    /// </summary>
    public class AtricleServiceImpl : IAtricleService
    {
        #region 领域仓储
        protected AtricleRepositoryImpl Atricle;
        #endregion
        #region  赋值
        /// <summary>
        /// 赋值实例
        /// </summary>
        public void GetValues()
        {
            Atricle = IocManager.Instance.Resolve<AtricleRepositoryImpl>();
        }
        #endregion
        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string SaveAtricle(AtricleDTO dto)
        {
            var entity = Mapper.Map<AtricleDTO, Atricle>(dto);
            try
            {
                Atricle.Add(entity);
                return "操作成功";
            }
            catch (Exception e)
            {
                return "内部出现错误";
            }
        }
    }
}
