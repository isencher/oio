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

namespace ting.lib
{
    public partial class cnnSet : Form
    {

        private cnnString _cnn = new cnnString();

        public cnnSet()
        {
            InitializeComponent();
            Load += (sender, e) => { cboIntegratedSecurity.SelectedIndex = 0; };
            cboIntegratedSecurity.SelectedIndexChanged += (sender, e) => {
                if (this.cboIntegratedSecurity.Text == this.cboIntegratedSecurity.Items[1].ToString()) //选择SQL Server身份验证
                {
                    this.txtUserId.Enabled = true; this.txtPwd.Enabled = true; this.txtUserId.Focus();
                }
                else
                {
                    this.txtUserId.Enabled = false; this.txtPwd.Enabled = false;
                }

            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ToValidate())
            {
                #region 生成cnnString实体
                _cnn.DataSource = txtServer.Text.Trim();
                _cnn.InitialCatalog = txtDatabase.Text.Trim();
                if (cboIntegratedSecurity.SelectedIndex == 0)
                {
                    _cnn.IntegratedSecurity = "True";
                    _cnn.UserId = null;
                    _cnn.Password = null;
                }
                else
                {
                    _cnn.IntegratedSecurity = "False";
                    _cnn.UserId = txtUserId.Text.Trim();
                    _cnn.Password = txtPwd.Text.Trim();
                }

                #endregion

                #region 保存到本地磁盘
                string name = (txtName.Text.ToString() == "") ? "default" : txtName.Text.ToString();
                bool result = withCnn.UpdateConnectionString(name, new cnnString().ConvertcnnString(_cnn));
                if (result)
                {
                    MessageBox.Show("保存成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败！请向管理员反馈，以查明原因。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion

                //退出对话框
                this.Close();
                DialogResult = DialogResult.OK;

            }
        }

        private bool ToValidate()
        {
            #region 有效性验证
            if(this.txtName.Text.Trim()=="")
            {
                MessageBox.Show("必须输入连接串名称！", "验证提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.txtName.Focus(); return false;
            }
            if (this.txtServer.Text.Trim() == "")
            {
                MessageBox.Show("必须输入服务器名或服务器的IP地址！", "验证提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.txtServer.Focus(); return false;
            }
            if (this.txtDatabase.Text.Trim() == "")
            {
                MessageBox.Show("必须输入数据库名！", "验证提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.txtDatabase.Focus(); return false;
            }
            if (this.cboIntegratedSecurity.SelectedIndex == 1)
            {
                if (this.txtUserId.Text.Trim() == "")
                {
                    MessageBox.Show("必须输入用户名！", "验证提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.txtUserId.Focus(); return false;
                }
                if (this.txtPwd.Text.Trim() == "")
                {
                    if (MessageBox.Show("确定使用空密码吗？", "验证提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.txtPwd.Focus(); return false;
                    }
                }
            }

            #endregion
            return true;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
