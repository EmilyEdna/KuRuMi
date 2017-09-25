using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Events.Events
{
    /// <summary>
    /// 标记接口(Marker Interface)，所有领域事件都要实现该接口。
    /// </summary>
    public interface IEvent
    {

        // 获取产生事件的时间
        DateTime Time { get; set; }
        //事件源
        object Source { get; set; }

    }
}
