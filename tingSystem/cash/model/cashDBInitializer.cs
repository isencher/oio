using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.cash.model
{
    public class cashDBInitializer : DropCreateDatabaseAlways<cashContext>
    {
        protected override void Seed(cashContext context)
        {
            var banks = new List<Bank>
            {
                new Bank{Id=1,Name="工商银行"},
                new Bank{Id=2,Name="农业银行"},
                new Bank{Id=3,Name="中国银行"},
                new Bank{Id=4,Name="建设银行"},
                new Bank{Id=5,Name="交通银行"},
                new Bank{Id=6,Name="招商银行"},
                new Bank{Id=7,Name="中信银行"},
                new Bank{Id=8,Name="浦发银行"},
                new Bank{Id=9,Name="广发银行"},
                new Bank{Id=10,Name="平安银行"},
                new Bank{Id=11,Name="光大银行"},
                new Bank{Id=12,Name="民生银行"},
                new Bank{Id=13,Name="兴业银行"},
                new Bank{Id=14,Name="江西银行"},
                new Bank{Id=15,Name="洪都农商银行"},
                new Bank{Id=16,Name="赣昌农商银行"},
                new Bank{Id=17,Name="九江银行"},
                new Bank{Id=18,Name="赣州银行"},
                new Bank{Id=19,Name="上饶银行"},
            };
            context.Banks.AddRange(banks);

            var categories = new List<AccountCategory>
            {
                new AccountCategory{Id=1, Name="现金"},
                new AccountCategory{Id=2, Name="基本户"},
                new AccountCategory{Id=3, Name="一般户"},
                new AccountCategory{Id=4, Name="理财户"},
                new AccountCategory{Id=5, Name="监管户"},
                new AccountCategory{Id=6, Name="保证金"},
                new AccountCategory{Id=7, Name="贷款户"},
                new AccountCategory{Id=8, Name="专户"},
            };
            context.AccountCategories.AddRange(categories);

            var accounts = new List<Account>
            {
                
            };
            context.Accounts.AddRange(accounts);

            var units = new List<Unit> { new Unit { Id = 0, Name = "Ignore",shortName=null } };
            context.Units.AddRange(units);

            base.Seed(context);
        }
    }
}
