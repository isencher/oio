using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.dal;
using ting.lib;
using ting.model;

namespace ting.pal.leveltwo
{
    public partial class dialog : EditDialog<LevelTwo>
    {
        public dialog()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                InputLimits();
                BindingControls(EditEntity);
                statusInfoLabel.Text = "";
            };
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
        protected override bool CheckValidity()
        {
            #region validate
            if (EditStatus == EditStatus.Add)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("二级菜单不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("二级菜单已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                return true;
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("二级菜单不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("二级菜单已经存在！", statusInfoLabel, false);
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
            txtClassName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetterOrDot(e);

            };
        }
        protected override void BindingControls(LevelTwo entity)
        {
            ComboBoxBinding(cboLevelOne, EditEntity, repo.GetAll<sysContext,LevelOne>(), "LevelOneId");
            TextBoxBinding(txtName, EditEntity, "Name");
            TextBoxBinding(txtClassName, EditEntity, "ClassName");
        }
    }
}
