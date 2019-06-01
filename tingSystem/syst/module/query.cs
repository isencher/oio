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

namespace ting.pal.module
{
    public partial class query : QueryDialog<Module>
    {
        public query()
        {
            InitializeComponent();
            Text = "查询";
            txtName.TextChanged += (sender, e) =>
            {
                var db = DataSource.Where(d => d.Name.Contains(txtName.Text)).ToList();
                UpdateByFilter(db);
            };
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
