using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    [Table("category",Schema ="cash")]
    [Serializable]
    public class AccountCategory : IbaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public string Unique { get => $"Name: {Name}"; }
        [NotMapped]
        public string DisplayValue { get => $"Id: {Id}, {Unique}"; }
        [NotMapped]
        public Dependencies Relation { get => Dependencies.Independent; }
    }
}
