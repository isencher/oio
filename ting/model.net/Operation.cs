using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    [Table("operation", Schema = "syst")]
    [Serializable]
    public class Operation : IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// entity name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// unique tag of every record
        /// </summary>
        public string Unique
        {
            get { return $"Name: {Name}"; }
        }

        /// <summary>
        /// display value
        /// </summary>
        public string DisplayValue
        {
            get { return $"Id: {Id}; Name: {Name}; MethodName: {MethodName}"; }
        }

        /// <summary>
        /// dependence relationship
        /// </summary>
        public Dependencies Relation => Dependencies.BeDependentOn;
    }
}
