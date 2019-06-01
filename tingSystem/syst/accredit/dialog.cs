using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using ting.dal;
using ting.lib;
using ting.model;

namespace ting.pal.accredit
{
    public partial class dialog : EditDialog<Accredit>
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

        protected override void BindingControls(Accredit entity)
        {
            //var rights = Repo<sysContext>.GetAll<Right>().Where(r=>r.Enabled==true).ToList();
            List<Right> rights;
            using (var sc = new sysContext())
            {
                rights = sc.Rights.Include(r => r.LevelTwo.LevelOne.Module).Where(r => r.Enabled == true).ToList();
            }
            ComboBoxBinding(cboRight, EditEntity, rights, "RightId");
            var ops = repo.GetAll<sysContext,Operation>();
            ComboBoxBinding(cboOperation, EditEntity, ops, "OPerationId");
            CheckBoxBinding(ckbEnabled, entity, "Enabled");
        }

        protected override bool CheckValidity()
        {
            if (cboRight.Text == "" || cboRight.Text == null)
            {
                withInfo.DisplayStatus("权限名称不能为空!", statusInfoLabel, false);
                return false;
            }
            if (cboOperation.Text == "" || cboOperation.Text == null)
            {
                withInfo.DisplayStatus("操作名称不能为空!", statusInfoLabel, false);
                return false;
            }
            if (EditStatus == EditStatus.Add && IsExist(EditEntity, true))
            {
                withInfo.DisplayStatus("该授权项已经存在!", statusInfoLabel, false);
                return false;
            }
            else if (EditStatus == EditStatus.Alter && IsExist(EditEntity, false))
            {
                withInfo.DisplayStatus("该授权项已经存在!", statusInfoLabel, false);
                return false;
            }
            return true;
        }

        protected override void InputLimits()
        {
            cboRight.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOperation.DropDownStyle = ComboBoxStyle.DropDownList;
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
