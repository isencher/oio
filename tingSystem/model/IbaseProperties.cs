using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model
{
    /// <summary>
    /// 实体基本属性
    /// </summary>
    public interface IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// entity name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// unique tag of every record
        /// </summary>
        string Unique { get; }
        /// <summary>
        /// display value
        /// </summary>
        string DisplayValue { get; }

        /// <summary>
        /// dependence relationship
        /// </summary>
        Dependencies Relation { get; }

    }
}
