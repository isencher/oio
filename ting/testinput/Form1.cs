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

namespace testinput
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += (sender, e) =>
              {
                  textBox1.KeyPress += TextBox1_KeyPress;
                  textBox1.TextChanged += TextBox1_TextChanged;
              };
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            withInput.IdentityNumber15to18(sender);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
