using KuRuMi.Mio.DoMain.Events.Handler;
using KuRuMi.Mio.DoMain.Infrastructure.Common;
using KuRuMi.Mio.DoMain.Model.ModelEvent.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.ModelEvent.Handler
{
    public class UserHandler :IEventHandler<UserEvent>
    {
        public void Handle(UserEvent Event)
        {
            Event.Source = Event.info;
            MailExtension.SendEmail(Event.info.Email,"用户名:"+Event.info.UserName+",密码:"+Event.info.PassWord);
        }
    }
}
