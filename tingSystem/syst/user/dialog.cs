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

namespace ting.pal.user
{
    public partial class dialog : EditDialog<User>
    {
        public dialog()
        {
            InitializeComponent();
            Load += (sender, e) =>
            {
                if (EditStatus == EditStatus.Alter)
                {
                    if (EditEntity.Account == "0000")
                    {
                        MessageBox.Show(null, "系统内置账户不允许修改！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                    using (var sc = new sysContext())
                    {
                        Original = sc.Users.Include("Partners").FirstOrDefault(u => u.Id == Original.Id);
                    }
                    txtLoginNumber.Enabled = false;
                }
                else if (EditStatus == EditStatus.Add)
                {
                    txtLoginNumber.Enabled = true;
                }
                txtLoginNumber.MaxLength = 4;
                InputLimits();
                BindingControls(EditEntity);
                LoadListBoxItems();
                LoadListBoxCheckedItems();
                statusInfoLabel.Text = "";
            };
        }

        protected override void BindingControls(User entity)
        {
            TextBoxBinding(txtName, entity, "Name");
            TextBoxBinding(txtLoginNumber, entity, "Account");
            TextBoxBinding(txtPassword, entity, "Password");
            CheckBoxBinding(ckbIsStop, entity, "IsStop");
        }
        protected override void InputLimits()
        {
            txtName.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetter(e);
            };
            txtLoginNumber.KeyPress += (sender, e) =>
            {
                withInput.OnlyDigit(e);
            };
            txtPassword.KeyPress += (sender, e) =>
            {
                withInput.OnlyLetterOrDigit(e);
            };
            txtPassword.PasswordChar = '*';
        }
        protected override bool CheckValidity()
        {
            #region validate
            if (EditStatus == EditStatus.Add)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("用户名称不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (txtLoginNumber.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("登录账号不能为空！", statusInfoLabel, false);
                    txtLoginNumber.Focus();
                    return false;
                }

                if (IsExist(EditEntity, true))
                {
                    withInfo.DisplayStatus("该用户已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                return true;
            }
            else if (EditStatus == EditStatus.Alter)
            {
                if (txtName.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("用户名称不能为空！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                if (txtLoginNumber.Text.Trim() == "")
                {
                    withInfo.DisplayStatus("登录账号不能为空！", statusInfoLabel, false);
                    txtLoginNumber.Focus();
                    return false;
                }

                if (IsExist(EditEntity, false))
                {
                    withInfo.DisplayStatus("该用户已经存在！", statusInfoLabel, false);
                    txtName.Focus();
                    return false;
                }

                return true;
            }
            return false;
            #endregion
        }
        private void LoadListBoxCheckedItems()
        {
            clbRoles.ClearSelected();
            foreach (string role in EditEntity.Roles)
            {
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    if (clbRoles.Items[i].ToString() == role)
                    {
                        clbRoles.SetItemChecked(i, true);
                    }
                }

            }
        }
        private void LoadListBoxItems()
        {
            clbRoles.Items.Clear();
            clbRoles.Items.AddRange(
                repo.GetAll<sysContext,Role>().Select(r => r.Name).ToArray()
                );
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidity())
            {
                #region generate sroles by checked item
                string[] roles = new string[clbRoles.CheckedItems.Count];
                int i = 0;
                foreach (var item in clbRoles.CheckedItems)
                {
                    roles[i] = item.ToString(); i++;
                }
                List<Role> sroles = null;
                if (roles.Length > 0)
                {
                    sroles = new List<Role>();
                    foreach (var role in roles)
                    {
                        var srole = repo.GetAll<sysContext,Role>().First(r => r.Name == role);
                        if (srole != null)
                        {
                            sroles.Add(srole);
                        }
                    }
                }
                #endregion

                #region to assemble a user instance from editdialog interaction
                //var user = LoadByRight.Register(EditEntity.Name, EditEntity.Account, EditEntity.Password, sroles);
                //EditEntity.SRoles = user.SRoles;
                EditEntity.Salt = withSecure.GenerateSalt();
                EditEntity.SaltedHashedPassword = withSecure.GenerateSaltedHashedPassword(EditEntity.Salt, txtPassword.Text.Trim());
                EditEntity.Partners = sroles;
                #endregion
                //int result = 0;
                //if (EditStatus == EditStatus.Add)
                //{
                //    //result = Repo<sysContext>.AddUserWithRole(EditEntity);
                //    result = Repo<sysContext>.m2mAdd<User, Role>(EditEntity);
                //    if (result > 0)
                //    {
                //        var logger = new sqlLogger();
                //        logger.Write(new ting.model.syst.Log()
                //        {
                //            UserAcount = null,
                //            Action = "Add",
                //            Date = DateTime.Now,
                //            Entity = EditEntity.GetType().FullName,
                //            Content = EditEntity.DisplayValue,
                //            Original = null,
                //        });
                //    }
                //}
                //else if (EditStatus == EditStatus.Alter)
                //{
                //    //result = Repo<sysContext>.AlterUserWithRole(EditEntity);
                //    result = Repo<sysContext>.M2MAlter<User, Role>(EditEntity);
                //    if (result > 0)
                //    {
                //        var logger = new sqlLogger();
                //        logger.Write(new ting.model.syst.Log()
                //        {
                //            UserAcount = null,
                //            Action = "Add",
                //            Date = DateTime.Now,
                //            Entity = EditEntity.GetType().FullName,
                //            Content = EditEntity.DisplayValue,
                //            Original = Original.DisplayValue,
                //        });
                //    }

                //}

                Save?.Invoke(EditEntity, Original);
                UpdateList?.Invoke();
                if (EditStatus == EditStatus.Alter) { this.Close(); }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateList?.Invoke();
            this.Close();
        }
    }
}
