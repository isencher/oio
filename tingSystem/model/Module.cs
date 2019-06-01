using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model
{
    /// <summary>
    /// 模块
    /// </summary>
    [Table("module", Schema = "syst")]
    [Serializable]
    public class Module : IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// entity name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Assembly name
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// unique tag of every record
        /// </summary>
        [NotMapped]
        public string Unique
        {
            get { return $"Name: {Name}"; }
        }
        /// <summary>
        /// display value
        /// </summary>
        [NotMapped]
        public string DisplayValue
        {
            get { return $"Id: {Id}, Name: {Name}, AssemblyName: {AssemblyName}"; }
        }

        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.BeDependentOn;
    }


}
