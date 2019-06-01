using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    public class DaybookTotal : IbaseProperties
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
        public string AccountName { get; set; }
        [NotMapped]
        public decimal? OpenBalance { get; set; }
        [NotMapped]
        public decimal? Debit { get; set; }
        [NotMapped]
        public decimal? Credit { get; set; }
        [NotMapped]
        public decimal? Balance { get; set; }
    }


}
