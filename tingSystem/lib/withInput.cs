using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ting.lib
{
    public class withInput
    {
        /// <summary>
        /// to use it in Control's KeyPress event, for inputting decimalism digit, not point
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyDigit(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// to use it in Control's KeyPress event, for inputting letter
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyLetter(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// to use it in Control's KeyPress event
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyLetterOrDigit(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// to use it in Control's KeyPress event, for inputting decimalism digit and point
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyNumeric(KeyPressEventArgs e)
        {
            if (((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))) &&
                    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// to use it in Control's KeyPress event
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyLetterOrDot(KeyPressEventArgs e)
        {
            if (((!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }
        /// <summary>
        /// to use it in Control's TextChange event
        /// </summary>
        /// <param name="sender"></param>
        public static void IdentityNumber15to18(object sender)
        {
            var input = (Control)sender;
            if (input.Text.Length == 15)
            {
                if (withIdentity.ValidateIdNumber(input.Text))
                {
                    input.Text = withIdentity.Id15To18(input.Text);
                }
            }
        }

    }
}
