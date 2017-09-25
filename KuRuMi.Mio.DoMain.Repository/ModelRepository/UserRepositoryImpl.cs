using KuRuMi.Mio.DoMain.Events.Bus;
using KuRuMi.Mio.DoMain.Model.Model;
using KuRuMi.Mio.DoMain.Model.ModelEvent.Events;
using KuRuMi.Mio.DoMain.Model.Repositories.EntityRepository;
using KuRuMi.Mio.DoMain.Repository.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Repository.ModelRepository
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepositoryImpl : RepositoryImpl<Sys_User>, IUserRepository
    {
        public KurumiMioDbContext context => lazy.Context as KurumiMioDbContext;
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public Sys_User CheckLogin(string Email, string PassWord)
        {
            try
            {
                Sys_User entity = null;
               //MongoDb取
               //entity = Mongo.FindSingleIndex<Sys_User>(a => a.Email == Email);
               // if (entity == null)
               // {
               //     //redis取
               //     try
               //     {
               //         entity = Redis.RedisString.Value.StringGet<Sys_User>(Email);
               //     }
               //     catch (Exception)
               //     {
               //     }
               // }
                if (entity == null)
                {
                    //数据库取
                    string sql = "select Id,UserName,Password,email from Sys_User as a where a.Email ='{0}' and  a.PassWord ='{1}'";
                    string select = string.Format(sql, Email, PassWord);
                    entity = context.user.SqlQuery(select).FirstOrDefault();
                }
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public override void Add(Sys_User aggregateRoot)
        {
            base.Add(aggregateRoot);
            //Redis.RedisString.Value.StringSet(aggregateRoot.Email, aggregateRoot);//保存一份到redis
            //Mongo.AddSignleObject(aggregateRoot);//保存一份到mongoDb
            //执行事件总线
            EventBus.bus.TiggerEvent(new UserEvent() { info=aggregateRoot});
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Sys_User UserRegist(Sys_User info)
        {
            string sql = "select Id,UserName,Password,email from Sys_User as a where a.UserName ='{0}' and email ='{1}'";
            string select = string.Format(sql, info.UserName, info.Email);
            return context.user.SqlQuery(select).FirstOrDefault();
        }
        /// <summary>
        /// 修改账户
        /// </summary>
        /// <param name="aggregateRoot"></param>
        public override void Update(Sys_User aggregateRoot)
        {
            base.Update(aggregateRoot);
        }
        /// <summary>
        /// 根据ID获取账户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetUserInfoById(Guid id)
        {
           var entity = context.Set<Sys_User>().Select(t=> new { t.UserName,t.Id}).FirstOrDefault(t=>t.Id==id);
            return entity;
        }
    }
}
