using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    /// <summary>
    /// 依赖关系
    /// </summary>
    public enum Dependencies
    {
        /// <summary>
        /// 依赖
        /// </summary>
        Dependent = 0,
        /// <summary>
        /// 被依赖
        /// </summary>
        BeDependentOn = 1,
        /// <summary>
        /// 即依赖，又被依赖
        /// </summary>
        DependentAndBeDependentOn = 2,
        /// <summary>
        /// 无依赖关系
        /// </summary>
        Independent = 3
    }
}
