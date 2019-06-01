using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.dal;
using ting.pal;

namespace ting.cash.daybooktotal
{
    public class work : generalwork<cashContext, DaybookTotal>
    {
        public work()
        {
            queryDialog = new query();

            CustomGetAll += ()=>{
                var ac = repo.GetAll<cashContext, Account>();
                var sb = repo.GetAll<cashContext, StandingBook>();
                var acsb = ac.GroupJoin(sb, a => a.Id, s => s.AccountId,
                    (a, s) => new { a.Id, a.Name, a.OpeningBalance, s });

                var tols = acsb.Select(a => new DaybookTotal
                {
                    AccountName = a.Name,
                    OpenBalance = a.OpeningBalance,
                    Debit = a.s.Sum(x => x.Debit),
                    Credit = a.s.Sum(x => x.Credit),
                    Balance = a.OpeningBalance + a.s.Sum(x => x.Debit) - a.s.Sum(x => x.Credit),
                }).ToList();
                var tol = new DaybookTotal
                {
                    OpenBalance = tols.Sum(x => x.OpenBalance),
                    Debit = tols.Sum(x => x.Debit),
                    Credit = tols.Sum(x => x.Credit),
                    Balance = tols.Sum(x => x.Balance)
                };
                tols.Add(tol);
                return tols;
            };
        }
    }
}
