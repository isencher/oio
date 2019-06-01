using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.lib;
using ting.model;

namespace ting.pal.setofbook
{
    public partial class dialog : EditDialog<SetofBook>
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
            if (txtName.Text.Trim() == "" || txtName.Text.Trim() == null)
            {
                withInfo.DisplayStatus("账套名称不能为空！", statusInfoLabel, false);
                return false;
            }
            if (txtDbName.Text.Trim() == "" || txtDbName.Text.Trim() == null)

            {
                withInfo.DisplayStatus("数据库名不能为空！", statusInfoLabel, false);
                return false;
            }
            if (EditStatus == EditStatus.Add && IsExist(EditEntity, true))
            {
                withInfo.DisplayStatus("该账套已经存在！", statusInfoLabel, false);
                return false;
            }
            if (EditStatus == EditStatus.Alter && IsExist(EditEntity, false))
            {
                withInfo.DisplayStatus("该账套已经存在！", statusInfoLabel, false);
                return false;
            }
            if (!(EditStatus == EditStatus.Add || EditStatus == EditStatus.Alter))
            { return false; }
            return true;
        }
        protected override void InputLimits()
        {
            txtName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
            txtDbName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
        }
        protected override void BindingControls(SetofBook entity)
        {
            TextBoxBinding(txtName, EditEntity, "Name");
            TextBoxBinding(txtDbName, EditEntity, "DbName");
        }
    }
}
