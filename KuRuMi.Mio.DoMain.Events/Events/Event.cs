using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Events.Events
{
    public class Event : IEvent
    {
        public DateTime Time { get; set; }
        public object Source { get; set; }
        public Event() {
            Time = DateTime.Now;
        }
        
    }
}
