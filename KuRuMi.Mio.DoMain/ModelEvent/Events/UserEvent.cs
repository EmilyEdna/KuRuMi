using KuRuMi.Mio.DoMain.Events.Events;
using KuRuMi.Mio.DoMain.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.ModelEvent.Events
{
     public class UserEvent :Event
    {
        public Sys_User info { get; set; }
    }
}
