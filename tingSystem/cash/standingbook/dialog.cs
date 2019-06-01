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

namespace ting.cash.standingbook
{
    public partial class dialog : /*Form//*/ EditDialog<StandingBook>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) => {
                txtUnitFilter.Text = "";
                if (EditEntity.Date == DateTime.MinValue) { EditEntity.Date = DateTime.Today; }
                InputLimits();
                BindingControls(EditEntity);
                statusLabelInfo.Text = "";
                txtUnitFilter.TextChanged += TxtUnitFilter_TextChanged;
                txtUnitFilter.LostFocus += TxtUnitFilter_LostFocus;
            };
        }

        private void TxtUnitFilter_LostFocus(object sender, EventArgs e)
        {
            cboUnit.DroppedDown = false;
            cboUnit.Focus();
        }

        private void TxtUnitFilter_TextChanged(object sender, EventArgs e)
        {
            cboUnit.DroppedDown = true;
            var filter = repo.GetAll<cashContext, Unit>().Where(u => u.Name.Contains(txtUnitFilter.Text)).ToList();
            cboUnit.DataSource = filter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidity())
            {
                EditEntity.Account = null;
                EditEntity.InOutCategory = null;
                EditEntity.Project = null;
                EditEntity.Unit = null;

                Save?.Invoke(EditEntity, Original);
                UpdateList?.Invoke();
                if (EditStatus == EditStatus.Alter) { this.Close(); }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool CheckValidity()
        {
            if (cboAccount.Text == null || cboAccount.Text == "")
            { withInfo.DisplayStatus("账号不能为空!", statusLabelInfo, false); return false; }
            if (cboCategory.Text == null || cboCategory.Text == "")
            { withInfo.DisplayStatus("收支类型不能为空!", statusLabelInfo, false); return false; }
            if (cboProject.Text == null || cboProject.Text == "")
            { withInfo.DisplayStatus("项目不能为空！", statusLabelInfo, false); return false; }
            if (txtDescribe.Text == null || txtDescribe.Text == "")
            { withInfo.DisplayStatus("摘要不能为空", false);return false; }
            if ((txtDebit.Text == null) && (txtCredit.Text == null))
            { withInfo.DisplayStatus("借贷方不能同时为空！", statusLabelInfo, false);return false; }
            if (Convert.ToDecimal(txtDebit.Text) == 0 && Convert.ToDecimal(txtCredit.Text) == 0)
            {
                withInfo.DisplayStatus("借贷方不能同时为0！", statusLabelInfo, false); return false;
            }
            if (Convert.ToDecimal(txtDebit.Text) != 0 && Convert.ToDecimal(txtCredit.Text) != 0)
            { withInfo.DisplayStatus("借贷方不能同时发生！", statusLabelInfo, false);return false; }
                #region validate
                if (EditStatus == EditStatus.Add)
            {
                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("该记录已经存在！", statusLabelInfo, false);
                    dtpDate.Focus();
                    return false;
                }
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该记录已经存在！", statusLabelInfo, false);
                    dtpDate.Focus();
                    return false;
                }
            }
            #endregion

            return true;
        }

        protected override void InputLimits()
        {

            txtDebit.KeyPress += (sender, e) => { withInput.OnlyNumeric(e); };
            txtCredit.KeyPress += (sender, e) => { withInput.OnlyNumeric(e); };
            txtVoucherNumber.KeyPress += (sender, e) => { withInput.OnlyLetterOrDigit(e); };
        }

        protected override void BindingControls(StandingBook entity)
        {
            ComboBoxBinding(cboAccount, entity, repo.GetAll<cashContext, Account>(), "AccountId");
            ComboBoxBinding(cboCategory, entity, repo.GetAll<cashContext, InOutCategory>(), "InOutCategoryId");
            ComboBoxBinding(cboUnit, entity, repo.GetAll<cashContext, Unit>(), "UnitId");
            //cboUnit.DataBindings.Clear();
            //cboUnit.DataSource = repo.GetAll<cashContext, Unit>();
            //cboUnit.DisplayMember = "Name";
            //cboUnit.ValueMember = "Id";
            //cboUnit.DataBindings.Add(
            //    new Binding("SelectedValue", entity, "UnitId", false, DataSourceUpdateMode.OnPropertyChanged, string.Empty)
            //    );
            ComboBoxBinding(cboProject, entity, repo.GetAll<cashContext, Project>(), "ProjectId");
            TextBoxBinding(txtDescribe, entity, "Describe");
            TextBoxBinding(txtDebit, entity, "Debit");
            //txtDebit.DataBindings.Clear();
            //txtDebit.DataBindings.Add(
            //    new Binding("Text", entity, "Debit", false, DataSourceUpdateMode.Never, string.Empty)
            //    );
            TextBoxBinding(txtCredit, entity, "Credit");
            //txtCredit.DataBindings.Clear();
            //txtCredit.DataBindings.Add(
            //    new Binding("Text", entity, "Credit", false, DataSourceUpdateMode.Never, string.Empty)
            //    );
            TextBoxBinding(txtVoucherNumber, entity, "VoucherNumber");
            CheckBoxBinding(cbkTag, entity, "Important");
            // datetimepicker binding
            dtpDate.DataBindings.Clear();
            dtpDate.DataBindings.Add(
                new Binding("Value", entity, "Date")
            );
        }
    }
}
