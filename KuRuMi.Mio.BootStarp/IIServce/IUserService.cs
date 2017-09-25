using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.BootStarp.IIServce
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService:IService
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool GetAllUserList(Sys_UserDTO dto);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="dto"></param>
        void UserRegist(Sys_UserDTO dto);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        Sys_UserDTO CheckLogin(string Email, string PassWord);
        /// <summary>
        /// 修改账户
        /// </summary>
        /// <param name="dto"></param>
        void Modify(Sys_UserDTO dto);
        /// <summary>
        /// 根据Id返回用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       object  GetUserInfoById(Guid id);
    }
}
