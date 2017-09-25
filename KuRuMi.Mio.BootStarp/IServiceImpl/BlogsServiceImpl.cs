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
    /// 博客接口实现
    /// </summary>
    public class BlogsServiceImpl : IBlogsService
    {
        #region 领域仓储
        protected BlogsRepositoryImpl blogs;
        #endregion

        #region  赋值
        /// <summary>
        /// 赋值实例
        /// </summary>
        public void GetValues()
        {
            blogs = IocManager.Instance.Resolve<BlogsRepositoryImpl>();
        }
        #endregion

        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string SaveBlogs(BlogsDTO dto)
        {
            var entity = Mapper.Map<BlogsDTO, Blogs>(dto);
            try
            {
                blogs.Add(entity);
                return "操作成功";
            }
            catch (Exception e)
            {
                return "内部出现错误";
            }
        }
        /// <summary>
        /// 获取部分博客信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<BlogsDTO> GetBlogsInfo(Guid? Key) 
        {
            string sql = string.Format("SELECT TOP 10 * FROM dbo.Blogs AS a WHERE a.BlogsId = '{0}'",Key);
            var result = Mapper.Map<List<Blogs>, List<BlogsDTO>>(blogs.LoadForSql(sql).ToList());
            return result;
        }
    }
}
