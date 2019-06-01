using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    /// <summary>
    /// 一级菜单
    /// </summary>
    [Table("levelone", Schema = "syst")]
    [Serializable]
    public class LevelOne : IbaseProperties
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
        /// module's id
        /// </summary>
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        /// <summary>
        /// module
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// module's name
        /// </summary>
        [NotMapped]
        public string ModuleName
        {
            get
            {
                if (Module != null) { return Module.Name; }
                else { return $"Its Id is {ModuleId}"; }
            }
        }
        /// <summary>
        /// unique tag of every record
        /// </summary>
        [NotMapped]
        public string Unique { get { return $"Name: {Name}; ModuleId: {ModuleId}"; } }
        /// <summary>
        /// display value
        /// </summary>
        [NotMapped]
        public string DisplayValue { get { return $"Id: {Id}; Name: {Name}; ModuleId: {ModuleId}"; } }
        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.DependentAndBeDependentOn;

    }
}
