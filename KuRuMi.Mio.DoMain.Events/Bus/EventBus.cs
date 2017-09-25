using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuRuMi.Mio.DoMain.Events.Events;
using KuRuMi.Mio.DoMain.Events.Handler;
using System.Reflection;
using System.Collections.Concurrent;

namespace KuRuMi.Mio.DoMain.Events.Bus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus : IEventBus
    {
        private object locker = new object();
        public static EventBus bus => new EventBus();
        private static IEnumerable<Assembly> assemly { get; set; }

        private static readonly ConcurrentDictionary<Type, List<Type>> EventMapping = new ConcurrentDictionary<Type, List<Type>>();
        /// <summary>
        /// 注册所有事件
        /// </summary>
        /// <param name="assembles"></param>
        public void RegisterAllHandler(IEnumerable<Assembly> assembles)
        {
            assemly = assembles;
            foreach (Assembly assembly in assembles)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    Type handlerInterfaceType = type.GetInterface("IEventHandler`1");
                    if (handlerInterfaceType != null)
                    {
                        Type eventType = handlerInterfaceType.GetGenericArguments()[0];
                        if (!EventMapping.Keys.Contains(eventType))
                        {
                            Register(eventType, type);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 注册到事件总线
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private List<Type> GetOrCreateHandlers(Type eventType)
        {
            return EventMapping.GetOrAdd(eventType, (type) => new List<Type>());
        }

        #region 注册事件
        /// <summary>
        /// 手动绑定事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="handle"></param>
        public void Register<THandle>(IHandler handle)
        {
            Register(typeof(THandle), handle.GetType());
        }
        public void Register(Type eventType, Type handler)
        {
            lock (locker)
            {
                GetOrCreateHandlers(eventType).Add(handler);
            }
        }
        /// <summary>
        /// 通过委托注册
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="action"></param>
        public void Register<THandle>(Action<THandle> action) where THandle : IEvent
        {
            ActionHandler<THandle> ActionHandler = new ActionHandler<THandle>(action);
            Register<THandle>(ActionHandler);
        }

        #endregion

        #region 卸载事件
        /// <summary>
        /// 手动卸载单个事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="handleType"></param>
        public void UnRegisiter<THandle>(Type handleType) where THandle : IEvent
        {
            lock (locker)
            {
                GetOrCreateHandlers(typeof(THandle)).RemoveAll(t => t == handleType);
            }
        }

        /// <summary>
        /// 卸载所有事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        public void UnRegisterAllHandler<THandle>()
        {
            lock (locker)
            {
                GetOrCreateHandlers(typeof(THandle)).Clear();
            }
        }
        #endregion

        #region 触发事件
        /// <summary>
        /// 根据事件源触发事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="eventData"></param>
        public void TiggerEvent<THandle>(THandle eventData) where THandle : IEvent
        {
            //获取所有的事件处理
            List<Type> handlerTypes = GetOrCreateHandlers(typeof(THandle));
            if (handlerTypes != null && handlerTypes.Count > 0)
            {
                foreach (var handlerType in handlerTypes)
                {
                    var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                    foreach (Assembly assembly in assemly)
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                        {
                            Type handlerInterfaceType = type.GetInterface("IEventHandler`1");
                            if (handlerInterfaceType != null)
                            {
                                //判断两个类型是否相等
                                if (handlerInterface == handlerInterfaceType)
                                {
                                   var eventType = handlerInterfaceType.GenericTypeArguments[0];
                                    EventMapping[eventType].ForEach(s=> {
                                        var obj = Activator.CreateInstance(s) as IEventHandler<THandle>;
                                        obj?.Handle(eventData);
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 指定handler触发事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public void TiggerEvent<THandle>(Type eventHandlerType, THandle eventData) where THandle : IEvent
        {
            var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");
            foreach (Assembly assembly in assemly)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    Type handlerInterfaceType = type.GetInterface("IEventHandler`1");
                    if (handlerInterfaceType != null)
                    {
                        //判断两个类型是否相等
                        if (handlerInterface == handlerInterfaceType)
                        {
                            var eventType = handlerInterfaceType.GenericTypeArguments[0];
                            EventMapping[eventType].ForEach(s => {
                                var obj = Activator.CreateInstance(s) as IEventHandler<THandle>;
                                obj?.Handle(eventData);
                            });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 根据事件源触发事件（异步）
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public Task TiggerEventAsync<THandle>(THandle eventData) where THandle : IEvent
        {
            return Task.Run(() => TiggerEvent<THandle>(eventData));
        }
        /// <summary>
        /// 指定handler触发事件（异步）
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="eventHandlerType"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public Task TiggerEventAsycn<THandle>(Type eventHandlerType, THandle eventData) where THandle : IEvent
        {
            return Task.Run(() => TiggerEvent<THandle>(eventHandlerType, eventData));
        }
        #endregion
    }
}
