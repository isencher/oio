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

namespace ting.cash.inoutcategory
{
    public partial class dialog : EditDialog<InOutCategory>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
              {
                  if (EditStatus == EditStatus.Add) { Text = "新增";txtCodeId.Enabled = true; }
                  else { Text = "修改"; txtCodeId.Enabled = false; }
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
                statusLabelInfo.Text = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool CheckValidity()
        {
            if(txtCodeId.Text==""||txtParent.Text==""||
                txtBranch.Text==""||txtSubBranch.Text==""||
                txtDescribe.Text=="")
            {
                withInfo.DisplayStatus("所有项都必须填写！", statusLabelInfo, false);
                return false;
            }
            if(EditStatus==EditStatus.Add)
            {
                if(IsExist(EditEntity,true))
                {
                    withInfo.DisplayStatus("该记录已经存在！", statusLabelInfo, false);
                    return false;
                }
            }
            if (EditStatus == EditStatus.Alter)
            {
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该记录已经存在！", statusLabelInfo, false);
                    return false;
                }
            }

            if(repo.GetAll<cashContext,InOutCategory>().Where(o=>o.Id!=EditEntity.Id&&o.CodeId==txtCodeId.Text).Count()>0)
            {
                withInfo.DisplayStatus("编码不能重复！", statusLabelInfo, false);
                return false;
            }
            if(repo.GetAll<cashContext,InOutCategory>().Where(o=>o.Id!=EditEntity.Id &&
                (o.Parent==txtParent.Text&&o.Branch==txtBranch.Text&&o.SubBranch==txtSubBranch.Text)).Count()>0)
            {
                withInfo.DisplayStatus("该记录已经存在！", statusLabelInfo, false);
                return false;
            }
            return true;
        }

        protected override void InputLimits()
        {
            txtCodeId.KeyPress += (sender, e) => { withInput.OnlyNumeric(e); };
            txtParent.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
            txtBranch.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
            txtSubBranch.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
            txtDescribe.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
        }

        protected override void BindingControls(InOutCategory entity)
        {
            TextBoxBinding(txtCodeId, entity, "CodeId");
            TextBoxBinding(txtParent, entity, "Parent");
            TextBoxBinding(txtBranch, entity, "Branch");
            TextBoxBinding(txtSubBranch, entity, "SubBranch");
            TextBoxBinding(txtDescribe, entity, "Describe");
        }
    }
}
