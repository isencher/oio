using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    /// <summary>
    /// 账套
    /// </summary>
    [Table("setofbook",Schema ="syst")]
    [Serializable]
    public class SetofBook : IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// entity name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// database name
        /// </summary>
        [Required]
        //[Index(IsUnique = true)]
        public string DbName { get; set; }

        /// <summary>
        /// unique tag of every record
        /// </summary>
        public string Unique => $"Name: {Name}";
        /// <summary>
        /// display value
        /// </summary>
        public string DisplayValue => $"Id: {Id}; Name: {Name}; DbName: {DbName}";
        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.BeDependentOn;
    }
}
