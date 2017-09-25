using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.BootStarp.IIServce
{
    /// <summary>
    /// 博客服务接口
    /// </summary>
    public interface IBlogsService: IService
    {
        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string SaveBlogs(BlogsDTO dto);
        /// <summary>
        /// 获取部分博客信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<BlogsDTO> GetBlogsInfo(Guid? Key);
    }
}
