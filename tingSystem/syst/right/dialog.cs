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

namespace ting.pal.right
{
    public partial class dialog : EditDialog<Right>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
            {
                BindingControls(EditEntity);
                InputLimits();
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
        protected override void BindingControls(Right entity)
        {
            // cboRole
            ComboBoxBinding(cboRole, entity, repo.GetAll<sysContext,Role>(), "RoleId");
            // cboSetofBooks
            ComboBoxBinding(cboSetofBook, entity, repo.GetAll<sysContext,SetofBook>(), "SetofBookId");
            // cboTwo
            ComboBoxBinding(cboTwo, entity, repo.GetAll<sysContext,LevelTwo>(), "LevelTwoId");
            // ckbEnabled
            ckbEnabled.DataBindings.Clear();
            Binding e = new Binding("Checked", entity, "Enabled");
            ckbEnabled.DataBindings.Add(e);
        }
        protected override void InputLimits()
        {
        }
        protected override bool CheckValidity()
        {
            if (cboRole.SelectedIndex == -1)
            {
                withInfo.DisplayStatus("角色名称不能为空！", statusInfoLabel, false);
                return false;
            }
            else if (cboSetofBook.SelectedIndex == -1)
            {
                withInfo.DisplayStatus("账套名称不能为空！", statusInfoLabel, false);
                return false;
            }
            else if (cboTwo.SelectedIndex == -1)
            {
                withInfo.DisplayStatus("二级菜单不能为空！", statusInfoLabel, false);
                return false;
            }
            if (EditStatus == EditStatus.Add)
            {
                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("该权限记录已经存在！", statusInfoLabel, false);
                    return false;
                }
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该权限记录已经存在！", statusInfoLabel, false);
                    return false;
                }
            }
            else { return false; }
            return true;
        }

    }
}
