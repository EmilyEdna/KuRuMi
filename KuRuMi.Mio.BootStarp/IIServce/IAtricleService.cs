using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.BootStarp.IIServce
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IAtricleService :IService
    {
        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string SaveAtricle(AtricleDTO dto);
    }
}
