using KuRuMi.Mio.DoMain.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.Repositories.EntityRepository
{
    /// <summary>
    /// User自定义仓库
    /// </summary>
    public interface IUserRepository
    {
        Sys_User UserRegist(Sys_User info);

        Sys_User CheckLogin(string Email, string PassWord);

        object GetUserInfoById(Guid id);
    }
}
