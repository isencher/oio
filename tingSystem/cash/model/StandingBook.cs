using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.cash.model
{
    [Table("standingbook",Schema ="cash")]
    [Serializable]
    public class StandingBook : IbaseProperties
    {
        public int Id { get; set; }
        [NotMapped]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [ForeignKey("InOutCategory")]
        public int InOutCategoryId { get; set; }
        public InOutCategory InOutCategory { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool Important { get; set; }
        public string Describe { get; set; }
        public decimal Debit { get; set; }
        [NotMapped]
        public decimal? DisplayDebit
        {
            get
            {
                if(Debit==0)
                { return null; }
                else
                { return Debit; }
            }
        }
        public decimal Credit { get; set; }
        [NotMapped]
        public decimal? DisplayCredit
        {
            get
            {
                if (Credit == 0)
                { return null; }
                else
                { return Credit; }
            }
        }
        public string VoucherNumber { get; set; }
        [NotMapped]
        public string AccountName { get =>Account ==null? $"It's Id: {AccountId}" : 
                $"{Account.Bank.Trim()}{Account.Branch.Trim()}{Account.Category.Trim()}";set { } }
        [NotMapped]
        public string InOutCategoryName { get => InOutCategory==null? $"It's Id: {InOutCategoryId}" : 
                                $"{InOutCategory.Name}"; set { }
        }
        [NotMapped]
        public string UnitName { get => Unit == null? null : Unit.Name; set { } }
        [NotMapped]
        public string ProjcetName { get => Project==null? $"It's Id : {ProjectId}" : Project.Name; set { } }
        [NotMapped]
        public string Unique { get => $"Date: {Date.Year}-{Date.Month}-{Date.Day}, AccountId: {AccountId}, " +
                $"InOutCategoryId: {InOutCategoryId}, " +
                $"UnitId: {UnitId}, ProjeectId: {ProjectId}, Important: {Important}, " +
                $"Describe: {Describe}, Debit: {Debit}, Credit: {Credit}";
        }
        [NotMapped]
        public string DisplayValue { get => $"Id: {Id}, {Unique}, VoucherNumber: {VoucherNumber}"; }
        [NotMapped]
        public Dependencies Relation { get => Dependencies.Dependent; }
    }
}
