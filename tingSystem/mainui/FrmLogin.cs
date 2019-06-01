using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using ting.dal;
using ting.lib;
using System.Collections.Generic;
using ting.model;

namespace ting.pal
{
    public partial class FrmLogin : Form
    {
        public User CurrentUser { get; set; }
        public SetofBook CurrentBook { get; set; }
        #region 表单初始化
        public FrmLogin()
        {
            InitializeComponent();
            txtLoginId.MaxLength = 4;
            txtLoginId.KeyPress +=(sender,e)=>{ withInput.OnlyDigit(e); };
            txtLoginPwd.KeyPress += (sender, e) => { withInput.OnlyLetterOrDigit(e); };
            txtLoginPwd.LostFocus += (sender, e) =>
            {
                var users = repo.GetAll<sysContext, User>();
                var user = users.FirstOrDefault(u => u.Account == txtLoginId.Text.Trim());
                if (user == null) { withInfo.DisplayStatus("用户名或密码有误!", statusInfoLabel, false); return; }
                var result = withSecure.CheckPassword(user.Salt,user.SaltedHashedPassword, txtLoginPwd.Text.Trim());
                List<SetofBook> books = null;
                if(result)
                {
                    using (var sc = new sysContext())
                    {
                        var roleids = sc.Users.Include(u => u.Partners)
                            .FirstOrDefault(u => u.Id == user.Id)
                            .Partners.Select(r => r.Id);

                        books = sc.Rights.Where(r => roleids.Contains(r.RoleId))
                        .Select(r => r.SetofBook).Distinct().ToList();
                    }
                    cboSetofBooks.DataSource = books;
                    cboSetofBooks.DisplayMember = "Name";
                    cboSetofBooks.ValueMember = "Id";
                    btnLogin.Enabled = true;
                }else
                {
                    withInfo.DisplayStatus("用户名或密码有误！", statusInfoLabel, false);
                    cboSetofBooks.DataSource = null;
                    btnLogin.Enabled = false;
                }
            };
            txtLoginPwd.GotFocus += (sender, e) =>
            {
                statusInfoLabel.Text = "";
            };
        }
        #endregion

        #region 登录按钮        
        private void btnLogin_Click(object sender, EventArgs e)     //登录
        {
            var user = repo.GetAll<sysContext,User>().FirstOrDefault(u => u.Account == txtLoginId.Text.Trim());
            var result = withSecure.CheckPassword(user.Salt, user.SaltedHashedPassword,  txtLoginPwd.Text.Trim());

            if(result)
            {
                CurrentUser = repo.GetAll<sysContext, User>().FirstOrDefault(u => u.Account == txtLoginId.Text.Trim());
                CurrentBook = repo.GetAll<sysContext, SetofBook>().First(s => s.Id == Convert.ToInt32(cboSetofBooks.SelectedValue));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                withInfo.DisplayStatus("用户名或密码错误！", statusInfoLabel, false);
                txtLoginId.Focus();
            }

        }
        #endregion

        #region 取消按钮        
        private void btnCancle_Click(object sender, EventArgs e)    //取消
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
        #endregion


        private void pnlSetParameter_DoubleClick(object sender, EventArgs e)
        {
            //this.Visible = false;
            //FrmSetConnectionParameter objSetConnectionParameter = new FrmSetConnectionParameter();
            //DialogResult result = objSetConnectionParameter.ShowDialog();
            //if (result == DialogResult.Cancel)  //退出参数设置界面
            //{
            //    this.Visible = true;
            //}
            //else
            //{

            //}
        }

    }
}
