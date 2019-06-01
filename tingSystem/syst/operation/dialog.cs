using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.lib;
using ting.model;

namespace ting.pal.operation
{
    public partial class dialog : EditDialog<Operation>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
            {
                InputLimits();
                BindingControls(EditEntity);
                statusInfoLabel.Text = "";
            };
        }
        protected override bool CheckValidity()
        {
            #region validate
            if (EditStatus == EditStatus.Add)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("操作名称不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("操作名称已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                return true;
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("操作名称不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("操作名称已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                return true;
            }
            return false;
            #endregion
        }
        protected override void InputLimits()
        {
            txtName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
            txtMethodName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
        }
        protected override void BindingControls(Operation entity)
        {
            TextBoxBinding(txtName, EditEntity, "Name");
            TextBoxBinding(txtMethodName, EditEntity, "MethodName");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidity())
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

    }
}
