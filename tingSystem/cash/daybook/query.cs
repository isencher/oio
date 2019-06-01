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

namespace ting.cash.daybook
{
    public partial class query :  QueryDialog<Daybook>
    {
        public query()
        {
            InitializeComponent();
            this.Text = "Filter for day book";
            Load += (sender, e) => {
                // set account down drop list
                var accounts = new List<Account>();
                var account = new Account() { Id = 0, Bank = "all" };
                accounts.Add(account);
                accounts.AddRange(repo.GetAll<cashContext, Account>());
                cboAccount.DataSource = accounts;
                cboAccount.SelectedIndex = 0;
                cboAccount.DisplayMember = "Name";
                cboAccount.ValueMember = "Id";

                dtpStart.Text = DataSource.Select(d => d.Date).Min().ToString();
                dtpEnd.Text = DateTime.Now.ToString();

                //cboAccount.SelectedIndexChanged += (sic, sice) => { Response(); };
                //dtpStart.LostFocus += (tc, tce) => {  Response(); };
                //dtpEnd.LostFocus += (tc, tce) => { Response(); };

            };
        }

        private void Response()
        {
            decimal? prebalance = 0;
            List<Daybook> all = null;
            withInfo.DisplayStatus("计算上期结余......", statusLabelInfo);
            var pre = DataSource.Where(d => d.Date == null || d.Date < Convert.ToDateTime(dtpStart.Text));
            if (Convert.ToInt32(cboAccount.SelectedValue) != 0)
            {
                pre = pre.Where(d => d.AccountId == Convert.ToInt32(cboAccount.SelectedValue));
            }
            if (pre != null)
            {
                prebalance = (from p in pre select (p.Debit == null ? 0 : p.Debit) - (p.Credit == null ? 0 : p.Credit)).Sum();
            }
            var fi = new List<Daybook>{
                    new Daybook() { Date = null, Describe = "上期结余", Debit = null, Credit = null, Balance = prebalance }
                };
            withInfo.DisplayStatus("加载指定期间记录......", statusLabelInfo);
            var ds = DataSource.Where(d => d.Date >= Convert.ToDateTime(dtpStart.Text) &&
                    d.Date <= Convert.ToDateTime(dtpEnd.Text)).ToList();
            if (Convert.ToInt32(cboAccount.SelectedValue) != 0)
            {
                ds = ds.Where(d=>d.AccountId == Convert.ToInt32(cboAccount.SelectedValue)).ToList();
            }
            all = fi.Union(ds).ToList();
            foreach (var item in all)
            {
                item.Balance = prebalance + (item.Debit == null ? 0 : item.Debit) - (item.Credit == null ? 0 : item.Credit);
                item.Debit = item.Debit == 0 ? null : item.Debit;
                item.Credit = item.Credit == 0 ? null : item.Credit;
                prebalance = item.Balance;
            }
            withInfo.DisplayStatus("计算汇总合计......", statusLabelInfo);
            var debittotal = all.Sum(a => a.Debit);
            var credittotal = all.Sum(a => a.Credit);
            var total = new Daybook { Date = null, Describe = "Total", Debit = debittotal, Credit = credittotal };
            all.Add(total);
            withInfo.DisplayStatus("查询完成。",statusLabelInfo);
            UpdateByFilter(all);
            withInfo.DisplayStatus("",statusLabelInfo);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if(Convert.ToDateTime(dtpEnd.Text)>=Convert.ToDateTime(dtpStart.Text))
            {
                Response();
            }
        }
    }
}
