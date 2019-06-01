using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    /// <summary>
    /// 多对多 - one end
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public interface IManytoMany<S>
        where S: class, new()
    {
        /// <summary>
        /// many-to-many other end
        /// </summary>
        ICollection<S> Partners { get; set; }
    }
}
