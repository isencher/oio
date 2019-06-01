using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model
{
    [Table("accredit",Schema ="syst")]
    [Serializable]
    public class Accredit : IbaseProperties
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
            //get => $"角色【{Role.Name}】的【{Right.Name}】功能的【{Operation.Name}】操作授权";
            get => $"【{RightId}.{OperationId}】";
            set { }
        }

        /// <summary>
        /// right's name
        /// </summary>
        [NotMapped]
        public string RightName { get => Right.Name; }
        /// <summary>
        /// operation's name
        /// </summary>
        [NotMapped]
        public string OperationName { get => Operation.Name; }

        /// <summary>
        /// right's id
        /// </summary>
        [ForeignKey("Right")]
        public int RightId { get; set; }
        /// <summary>
        /// right
        /// </summary>
        public Right Right { get; set; }
        /// <summary>
        /// operation's id
        /// </summary>
        [ForeignKey("Operation")]
        public int OperationId { get; set; }
        /// <summary>
        /// operation
        /// </summary>
        public Operation Operation { get; set; }
        /// <summary>
        /// whether to enable someone action
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// unique tag of every record
        /// </summary>
        [NotMapped]
        public string Unique => $"RightId: {RightId}; OperationId: {OperationId}";
        /// <summary>
        /// display value
        /// </summary>
        [NotMapped]
        public string DisplayValue => $"Id: {Id}; RightId: {RightId}; OperationId: {OperationId}; Enabled: {Enabled}";
        /// <summary>
        /// dependence relationship
        /// </summary>
        [NotMapped]
        public Dependencies Relation => Dependencies.BeDependentOn;

    }
}
