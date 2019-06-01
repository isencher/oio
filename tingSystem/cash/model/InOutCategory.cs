using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    [Table("inoutcategory", Schema ="cash")]
    [Serializable]
    public class InOutCategory : IbaseProperties
    {
        public int Id { get; set; }
        [NotMapped]
        public string Name { get => $"{Parent}|{Branch}|{SubBranch}"; set { } }
        public string CodeId { get; set; }
        public string Parent { get; set; }
        public string Branch { get; set; }
        public string SubBranch { get; set; }
        public string Describe { get; set; }

        [NotMapped]
        public string Unique { get => $" {Name}"; }
        [NotMapped]
        public string DisplayValue { get => $"Id: {Id}, Parent: {Parent}, Branch: {Branch}, SubBranch: {SubBranch}, Describe: {Describe}"; }
        [NotMapped]
        public Dependencies Relation { get => Dependencies.BeDependentOn; }

    }
}
