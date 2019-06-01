using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.cash.model;
using ting.dal;
using ting.lib;
using ting.pal;

namespace ting.cash.daybooktotal
{
    public partial class query :  QueryDialog<DaybookTotal>
    {
        private List<Account> all;
        private DateTime startdate
        {
            get=> Convert.ToDateTime(dtpStart.Text);
        }
        private DateTime enddate { get=>Convert.ToDateTime(dtpEnd.Text); }

        public query()
        {
            InitializeComponent();
            Load += (sender, e) => {
                this.Text = "查询";
                this.statusLabelInfo.Text = "";
                this.dtpStart.Text = repo.GetAll<cashContext, StandingBook>().Min(s => s.Date).ToString();
                all = GetAll();
            };
        }

        private List<Account> GetAll()
        {
            var ac = repo.GetAllwithInclude<cashContext, Account>();
            
            return ac.ToList();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (startdate > enddate) { withInfo.DisplayStatus("开始日期不能大于终止日期", statusLabelInfo, false);return; }
            if(startdate ==null || enddate == null) { withInfo.DisplayStatus("日期不能为空", statusLabelInfo, false); return; }

            var filter = all.Select(a => new DaybookTotal
            {
                AccountName = a.Name,
                OpenBalance = a.OpeningBalance +
                    a.StandingBooks.Where(x => x.Date < startdate).Sum(x => x.Debit) -
                    a.StandingBooks.Where(x => x.Date < startdate).Sum(x => x.Credit),
                Debit = a.StandingBooks.Where(x => x.Date >= startdate && x.Date <= enddate).Sum(x => x.Debit),
                Credit = a.StandingBooks.Where(x => x.Date >= startdate && x.Date <= enddate).Sum(x => x.Credit),
                Balance = a.OpeningBalance +
                    a.StandingBooks.Where(x => x.Date < startdate).Sum(x => x.Debit) -
                    a.StandingBooks.Where(x => x.Date < startdate).Sum(x => x.Credit) +
                    a.StandingBooks.Where(x => x.Date >= startdate && x.Date <= enddate).Sum(x => x.Debit) -
                    a.StandingBooks.Where(x => x.Date >= startdate && x.Date <= enddate).Sum(x => x.Credit)
            }).ToList();
            filter = filter.Where(f => !(f.OpenBalance == 0 && f.Debit == 0 && f.Credit == 0 && f.Balance == 0)).ToList();
            var tol = new DaybookTotal { AccountName = "Total",
                OpenBalance = filter.Sum(f=>f.OpenBalance),
                Debit = filter.Sum(f=>f.Debit),
                Credit = filter.Sum(f=>f.Credit),
                Balance = filter.Sum(f=>f.Balance),
            };
            filter.Add(tol);

            UpdateByFilter(filter);
        }
    }
}
