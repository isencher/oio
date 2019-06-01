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
using ting.lib;
using ting.pal;

namespace ting.cash.unit
{
    public partial class dialog :  EditDialog<Unit>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
              {
                  Text = "新增";
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
            if (txtName.Text == null || txtName.Text == "")
            {
                withInfo.DisplayStatus("单位名称不能为空！", statusLabelInfo, false);
                return false;
            }
            if (EditStatus == EditStatus.Add)
            {
                if (IsExist(EditEntity, true))
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
            return true;
        }

        protected override void InputLimits()
        {
            txtName.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
            txtShortName.KeyPress += (sender, e) => { withInput.OnlyLetter(e); };
        }

        protected override void BindingControls(Unit entity)
        {
            TextBoxBinding(txtName, entity, "Name");
            TextBoxBinding(txtShortName, entity, "shortName");
        }

    }
}
