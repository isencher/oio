using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.dal;
using ting.pal;

namespace ting.cash.daybook
{
    public class work : generalwork<cashContext,Daybook>
    {
        public work()
        {
            queryDialog = new query();
            CustomGetAll += () =>
              {
                  using (var sc = new cashContext())
                  {
                      var sb = repo.GetAll<cashContext, StandingBook>().OrderBy(s=>s.Date);
                      var ac = repo.GetAll<cashContext, Account>().Where(a=>a.OpeningBalance!=0);
                      var ob = from a in ac select new Daybook() { Date=null, Describe = "上期结余", Debit = a.OpeningBalance, AccountId = a.Id };
                      var re = from s in sb select new Daybook() { Date = s.Date, Describe = s.Describe, Debit = s.Debit, Credit = s.Credit, AccountId=s.AccountId, ProjectId=s.ProjectId,UnitId=s.UnitId, InOutCategoryId=s.InOutCategoryId,Tag=s.Important };
                      var all = ob.Union(re).ToList();
                      decimal? prebalance = 0;
                      foreach (var item in all)
                      {
                          item.Balance = prebalance + (item.Debit==null?0:item.Debit) - (item.Credit==null?0:item.Credit);
                          item.Debit = item.Debit == 0 ? null : item.Debit;
                          item.Credit = item.Credit == 0 ? null : item.Credit;
                          prebalance = item.Balance;
                      }
                      return all;
                  }
              };
        }
    }
}
