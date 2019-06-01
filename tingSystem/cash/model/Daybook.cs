using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    public class Daybook : IbaseProperties
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Unique { get; }
        [NotMapped]
        public string DisplayValue { get; }
        [NotMapped]
        public Dependencies Relation { get; }
        [NotMapped]
        public DateTime? Date { get; set; }
        [NotMapped]
        public int AccountId { get; set; }
        [NotMapped]
        public int? UnitId { get; set; }
        [NotMapped]
        public int ProjectId { get; set; }
        [NotMapped]
        public int InOutCategoryId { get; set; }
        [NotMapped]
        public bool Tag { get; set; }
        [NotMapped]
        public string Describe { get; set; }
        [NotMapped]
        public decimal? Debit { get; set; }
        [NotMapped]
        public decimal? Credit { get; set; }
        [NotMapped]
        public decimal? Balance { get; set; }
    }
}
