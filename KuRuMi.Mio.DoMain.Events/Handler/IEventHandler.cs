using KuRuMi.Mio.DoMain.Events.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Events.Handler
{
    /// <summary>
    /// 标志接口
    /// </summary>
    public interface IHandler
    {

    }

    /// <summary>
    /// 事件处理器接口，所有事件处理器都要实现该接口。
    /// </summary>
    public interface IEventHandler<TEvent> : IHandler where TEvent:IEvent
    {
        // 处理给定的事件
        void Handle(TEvent Event);
    }
}
