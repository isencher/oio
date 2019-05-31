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
        /// use it in Control's KeyPress event,
        /// can input decimalism digit, but not dot, not plus or minus sign
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
        /// use it in Control's KeyPress event,
        /// can input letter, china word, not decimalism,not punctuation, not coltrol char
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
        /// use it in Control's KeyPress event,
        /// can input letter include china word, and decimalism digit
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
        /// use it in Control's KeyPress event,
        /// can input decimalism digit and dot
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
        /// use it in Control's KeyPress event,
        /// can input letter include china word and dot
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
        /// to use it in Control's TextChange event,
        /// can convert 15 digit id number into 18 digit
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
