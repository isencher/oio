using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.model;
using ting.lib;

namespace ting.pal.module
{
    public partial class dialog : EditDialog<Module>
    {
        public string text { get { return txtName.Text; } }
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

        //public dialogmodule(Module entity)
        //    : base()
        //{
        //    EditEntity = entity;
        //}

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
        protected override void BindingControls(Module entity)
        {
            TextBoxBinding(txtName, entity, "Name");
            TextBoxBinding(txtAssemblyName, entity, "AssemblyName");
        }
        protected override void InputLimits()
        {
            //txtName.MaxLength = 18;
            txtName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
            txtAssemblyName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
        }
        protected override bool CheckValidity()
        {
            #region validate
            if (EditStatus == EditStatus.Add)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("模块名不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("该模块已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                return true;
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("模块名不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该模块已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }
                return true;
            }
            return false;
            #endregion
        }
    }
}
