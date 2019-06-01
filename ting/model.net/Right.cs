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
    /// 功能
    /// </summary>
    [Table("right",Schema ="syst")]
    [Serializable]
    public class Right : IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// entity name
        /// </summary>
        [NotMapped]
        public string Name
        {
            get
            {
                return $"【{RoleId}.{SetofBookId}." +
                    $"{LevelTwo.LevelOne.Module.Id}." +
                    $"{LevelTwo.LevelOne.Id}." +
                    $"{LevelTwo.Id}】";
            }
            set { }
        }

        /// <summary>
        /// role's id
        /// </summary>
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        /// <summary>
        /// role
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// setofbook's id
        /// </summary>
        [ForeignKey("SetofBook")]
        public int SetofBookId { get; set; }
        /// <summary>
        /// setofbook
        /// </summary>
        public SetofBook SetofBook { get; set; }
        /// <summary>
        /// leveltwo's id
        /// </summary>
        [ForeignKey("LevelTwo")]
        public int LevelTwoId { get; set; }
        /// <summary>
        /// leveltwo
        /// </summary>
        public LevelTwo LevelTwo { get; set; }
        /// <summary>
        /// whether to enable
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// role's name
        /// </summary>
        public string RoleName { get => Role.Name; }
        /// <summary>
        /// setofbook's name
        /// </summary>
        public string SetofBooksName { get => SetofBook.Name; }
        /// <summary>
        /// module's name
        /// </summary>
        public string ModuleName { get => LevelTwo.LevelOne.ModuleName; }
        /// <summary>
        /// levelone's name
        /// </summary>
        public string LevelOneName { get => LevelTwo.LevelOne == null ? null : LevelTwo.LevelOne.Name; }
        /// <summary>
        /// leveltwo's name
        /// </summary>
        public string LevelTwoName { get => LevelTwo.Name; }

        /// <summary>
        /// unique tag of every record
        /// </summary>
        public string Unique
        {
            get { return $"SetofBookId: {SetofBookId}; LevelTwoId: {LevelTwoId}; "; }
        }
        /// <summary>
        /// display value
        /// </summary>
        public string DisplayValue
        {
            get
            {
                return $"Id: {Id}; SetofBookId: {SetofBookId}; LevelTwoId: {LevelTwoId}; Enabled: {Enabled}";
            }
        }
        /// <summary>
        /// dependence relationship
        /// </summary>
        public Dependencies Relation => Dependencies.DependentAndBeDependentOn;
    }
}
