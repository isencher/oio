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

namespace ting.pal.levelone
{
    public partial class dialog : EditDialog<LevelOne>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
            {
                InputLimits();
                BindingControls(EditEntity);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidity())
            {
                Save?.Invoke(EditEntity, Original);
                UpdateList?.Invoke();
                if (EditStatus == EditStatus.Alter) { Close(); }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void InputLimits()
        {
            txtName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
        }
        protected override void BindingControls(LevelOne entity)
        {
            var DataSource = repo.GetAll<sysContext,Module>();
            ComboBoxBinding(cboModule, entity, DataSource, "ModuleId");
            TextBoxBinding(txtName, entity, "Name");
        }
        protected override bool CheckValidity()
        {
            #region validate
            if (EditStatus == EditStatus.Add)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("一级菜单不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("一级菜单已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                return true;
            }
            else
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("一级菜单不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("一级菜单已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                return true;
            }
            #endregion
        }
    }
}
