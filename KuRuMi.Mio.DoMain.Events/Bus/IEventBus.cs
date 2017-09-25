using KuRuMi.Mio.DoMain.Events.Events;
using KuRuMi.Mio.DoMain.Events.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Events.Bus
{
    public interface IEventBus
    {
        //注册事件
        void RegisterAllHandler(IEnumerable<Assembly> assembles);
        void Register<THandle>(IHandler handle);
        void Register(Type eventType, Type handler);
        void Register<THandle>(Action<THandle> action) where THandle : IEvent;

        //反注册事件
        void UnRegisiter<THandle>(Type handleType) where THandle : IEvent;
        void UnRegisterAllHandler<THandle>();

        //触发事件
        void TiggerEvent<THandle>(THandle eventData) where THandle : IEvent;
        void TiggerEvent<THandle>(Type eventHandlerType, THandle eventData) where THandle : IEvent;

        Task TiggerEventAsync<THandle>(THandle eventData) where THandle : IEvent;

        Task TiggerEventAsycn<THandle>(Type eventHandlerType, THandle eventData) where THandle : IEvent;
    }
}
