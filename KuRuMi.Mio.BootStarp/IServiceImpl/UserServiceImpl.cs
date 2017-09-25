using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Repository.ModelRepository;
using KuRuMi.Mio.BootStarp.IIServce;
using KuRuMi.Mio.DoMain.Model.Model;
using AutoMapper;

namespace KuRuMi.Mio.BootStarp.IServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        #region 领域仓储
        protected UserRepositoryImpl user;
        #endregion

        #region  赋值
        /// <summary>
        /// 赋值实例
        /// </summary>
        public void GetValues()
        {
            user = IocManager.Instance.Resolve<UserRepositoryImpl>();
        }
        #endregion

        /// <summary>
        /// 取所有用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool GetAllUserList(Sys_UserDTO dto)
        {
            var entity = Mapper.Map<Sys_UserDTO, Sys_User>(dto);
            var isnull = user.UserRegist(entity);
            if (isnull != null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="dto"></param>
        public void UserRegist(Sys_UserDTO dto)
        {
            var entity = Mapper.Map<Sys_UserDTO, Sys_User>(dto);
            user.Add(entity);
        }

        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public Sys_UserDTO CheckLogin(string Email, string PassWord)
        {
            var entity = user.CheckLogin(Email, PassWord);
            var dto = Mapper.Map<Sys_User, Sys_UserDTO>(entity);
            return dto;
        }
        /// <summary>
        /// 修改账户
        /// </summary>
        /// <param name="dto"></param>
        public void Modify(Sys_UserDTO dto)
        {
            var entity = Mapper.Map<Sys_UserDTO, Sys_User>(dto);
            user.Update(entity);
        }
        /// <summary>
        /// 根据ID获取账户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetUserInfoById(Guid id)
        {
           return user.GetUserInfoById(id);
        }
    }
}
