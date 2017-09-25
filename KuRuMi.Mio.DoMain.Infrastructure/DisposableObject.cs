using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure
{
    /// <summary>
    /// 资源释放
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        /// 析构函数
        /// </summary>
        ~DisposableObject()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            this.ExplicitDispose();
        }
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected abstract void Dispose(bool disposing);

    }
}
