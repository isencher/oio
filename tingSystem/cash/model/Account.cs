using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    [Table("account",Schema ="cash")]
    [Serializable]
    public class Account : IbaseProperties
    {
        public int Id { get; set; }
        public string CodeId { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Category { get; set; }
        public string Number { get; set; }
        public decimal OpeningBalance { get; set; }
        public bool IsStop { get; set; }
        [NotMapped]
        public string Name { get=>$"{Bank?.Trim()}{Branch?.Trim()}{Category?.Trim()}{Number?.Substring(Number.Length-4,4)}"; set { } }
        [NotMapped]
        public string Unique { get=>$"Number: {Number}";  }
        [NotMapped]
        public string DisplayValue { get=>$"Id: {Id}, CodeId: {CodeId}, Bank: {Bank.Trim()}, Branch: {Branch.Trim()}, Category: {Category.Trim()}, Number: {Number}, OpeningBalance: {OpeningBalance}";  }
        [NotMapped]
        public Dependencies Relation { get=>Dependencies.BeDependentOn; }

        public virtual List<StandingBook> StandingBooks { get; set; }
    }
}
