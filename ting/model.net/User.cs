using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ting.model.net
{
    [Table("user", Schema = "syst")]
    [Serializable]
    public class User : IbaseProperties, IManytoMany<Role>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public User()
        {
            //this.SRoles = new HashSet<Role>();
            Partners = new HashSet<Role>();
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
        /// user account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// salt for generating saltedhashedpassword
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// to generate from salt and password
        /// </summary>
        public string SaltedHashedPassword { get; set; }
        /// <summary>
        /// login password
        /// </summary>
        [NotMapped]
        public string Password { get; set; }
        /// <summary>
        /// wether to stop the account
        /// </summary>
        public bool IsStop { get; set; }
        /// <summary>
        /// role collection
        /// </summary>
        public virtual ICollection<Role> Partners { get; set; }

        /// <summary>
        /// partner's name collection
        /// </summary>
        [NotMapped]
        public string[] Roles
        {
            get
            {
                return Partners.Select(r => r.Name).ToArray();
            }
        }
        /// <summary>
        /// partner's name collection string
        /// </summary>
        [NotMapped]
        public string DisplayRoles
        {
            get
            {
                string result = "";
                if (Partners != null)
                {
                    foreach (var item in Partners)
                    {
                        result += string.Concat(item.Name, ",");
                    }
                    result = result.TrimEnd(',');
                }
                return result;
            }
        }
        /// <summary>
        /// unique tag of every record
        /// </summary>
        [NotMapped]
        public string Unique
        {
            get
            {
                return $"Account: {Account}";
            }
        }
        /// <summary>
        /// display value
        /// </summary>
        [NotMapped]
        public string DisplayValue
        {
            get
            {
                return $"Id: {Id}, Name: {Name}, Account: {Account}, " +
                      $"IsStop: {IsStop}, DisplayRoles: {DisplayRoles}," +
                      $"Salt: {Salt},SaltedHashedPassword: {SaltedHashedPassword}";
            }
        }
        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.DependentAndBeDependentOn;
    }
}
