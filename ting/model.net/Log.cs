using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.model.net
{
    [Table("Log", Schema = "syst")]
    public class Log : IbaseProperties
    {
        /// <summary>
        /// indentity
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// operate time
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// operation user
        /// </summary>
        public string UserAcount { get; set; }
        /// <summary>
        /// operate action
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// operate object
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// operate content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// content before operation
        /// </summary>
        public string Original { get; set; }
        public string Name { get ; set; }
        public string Unique { get; }
        public string DisplayValue { get; }
        public Dependencies Relation { get; }
    }
}
