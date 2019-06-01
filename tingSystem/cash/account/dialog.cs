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

namespace ting.cash.account
{
    public partial class dialog :  EditDialog<Account>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) => {
                InputLimits();
                BindingControls(EditEntity);
                statusLabelInfo.Text = "";
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckValidity())
            {
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
            if (txtCodeId.Text == "" || txtCodeId.Text == null)
            {
                withInfo.DisplayStatus("科目编码不能为空，请与会计确认填列！", statusLabelInfo, false);
                return false;
            }
            if(cboBank.Text==""||cboBank.Text==null)
            {
                withInfo.DisplayStatus("银行名称不能为空！", statusLabelInfo, false);
                return false;
            }
            if(txtBranch.Text==""||txtBranch.Text==null)
            {
                withInfo.DisplayStatus("银行网点不能为空!", statusLabelInfo, false);
                return false;
            }
            if(cboCategory.Text==""||cboCategory.Text==null)
            {
                withInfo.DisplayStatus("账户类型不能为空!", statusLabelInfo, false);
                return false;
            }
            if(txtNumber.Text==""||txtNumber.Text==null)
            {
                withInfo.DisplayStatus("银行账户不能为空!", statusLabelInfo, false);
                return false;
            }

            var repeatcount = repo.GetAll<cashContext, Account>().Where(a => a.CodeId == txtCodeId.Text.Trim()&&a.Id!=EditEntity.Id).Count();
            if (repeatcount > 1)
            {
                withInfo.DisplayStatus("科目编码不能重复！", statusLabelInfo, false);
                return false;
            }

            if(EditStatus==EditStatus.Add)
            {
                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("该记录在数据库中已经存在！", statusLabelInfo, false);
                    return false;
                }
            }
            if (EditStatus == EditStatus.Alter)
            {
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该记录在数据库中已经存在！", statusLabelInfo, false);
                    return false;
                }
            }

            return true;
        }

        protected override void InputLimits()
        {
            txtCodeId.KeyPress += (sender, e) => { withInput.OnlyDigit(e); };
            cboBank.DropDownStyle = ComboBoxStyle.DropDownList;
            txtBranch.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            txtNumber.KeyPress += (sender, e) => { withInput.OnlyNumeric(e); };
            txtOpeningBalance.KeyPress += (sender, e) => { withInput.OnlyDigit(e); };
        }

        protected override void BindingControls(Account entity)
        {
            TextBoxBinding(txtCodeId, entity, "CodeId");
            ComboBoxBinding(cboBank, entity, repo.GetAll<cashContext, Bank>(), 
                "Bank","SelectedValue","Name","Name");
            TextBoxBinding(txtBranch, entity, "Branch");
            ComboBoxBinding(cboCategory, entity, repo.GetAll<cashContext, AccountCategory>(), 
                "Category","SelectedValue", "Name","Name");
            TextBoxBinding(txtNumber, entity, "Number");
            TextBoxBinding(txtOpeningBalance, entity, "OpeningBalance");
            CheckBoxBinding(ckbIsStop, entity, "IsStop");
        }
    }
}
