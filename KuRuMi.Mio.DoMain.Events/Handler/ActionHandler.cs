using KuRuMi.Mio.DoMain.Events.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Events.Handler
{
    public class ActionHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        public Action<TEvent> Action { get; private set; }

        public ActionHandler() { }

        public ActionHandler(Action<TEvent> handler) {
            Action = handler;
        }

        public void Handle(TEvent Event)
        {
            throw new NotImplementedException();
        }
    }
}
