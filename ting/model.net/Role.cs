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
    /// 角色
    /// </summary>
    [Table("role", Schema = "syst")]
    [Serializable]
    public class Role : IbaseProperties
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Role()
        {
            this.Users = new HashSet<User>();
        }
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
        /// user collection
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

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
            get { return $"Id: {Id}, Name: {Name}"; }
        }

        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.DependentAndBeDependentOn;
    }

}
