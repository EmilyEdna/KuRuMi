using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using KuRuMi.Mio.DoMain.Infrastructure.Logger;

namespace KuRuMi.Mio.DoMain.RedisCache.RedisCommon
{
    public class RedisManager
    {
        private static readonly ConfigurationOptions option = RedisConfig.option;
        private static readonly object locker = new object();
        private static ConnectionMultiplexer instance;

        /// <summary>
        /// 单例模式获取redis连接实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = GetManager();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 创建Redis链接
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager(ConfigurationOptions options = null)
        {
            options = options ?? option;
            var connect = ConnectionMultiplexer.Connect(options);

            #region 注册事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;
            #endregion

            return connect;
        }

        #region Redis事件
        /// <summary>
        /// 内部异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            UnitExtension.Log("内部异常：" + e.Exception.Message);
        }

        /// <summary>
        /// 集群更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            UnitExtension.Log("新集群：" + e.NewEndPoint + "旧集群：" + e.OldEndPoint);
        }

        /// <summary>
        /// 配置更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            UnitExtension.Log("配置更改：" + e.EndPoint);
        }

        /// <summary>
        /// 错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            UnitExtension.Log("异常信息：" + e.Message);
        }

        /// <summary>
        /// 重连错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            UnitExtension.Log("重连错误" + e.EndPoint);
        }

        /// <summary>
        /// 连接失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            UnitExtension.Log("连接异常" + e.EndPoint + "，类型为" + e.FailureType + (e.Exception == null ? "" : ("，异常信息是" + e.Exception.Message)));
        }
        #endregion
    }
}
