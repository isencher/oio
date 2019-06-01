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
    /// 二级菜单
    /// </summary>
    [Table("leveltwo", Schema = "syst")]
    [Serializable]
    public class LevelTwo : IbaseProperties
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
        /// class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// levelone's id
        /// </summary>
        [ForeignKey("LevelOne")]
        public int LevelOneId { get; set; }
        /// <summary>
        /// levelone
        /// </summary>
        public LevelOne LevelOne { get; set; }

        /// <summary>
        /// levelone's name
        /// </summary>
        [NotMapped]
        public string LevelOneName
        {
            get
            {
                if (LevelOne != null) { return LevelOne.Name; }
                else { return $"Its Id is {LevelOneId}"; }
            }
        }
        /// <summary>
        /// unique tag of every record
        /// </summary>
        [NotMapped]
        public string Unique { get { return $"Name: {Name}; LevelOneId: {LevelOneId}"; } }
        /// <summary>
        /// display value
        /// </summary>
        [NotMapped]
        public string DisplayValue { get { return $"Id: {Id}; Name: {Name}; ClassName: {ClassName}; LevelOneName: {LevelOneName}"; } }

        /// <summary>
        /// dependence relationship
        /// </summary>
        public Dependencies Relation => Dependencies.Dependent;

    }
}
