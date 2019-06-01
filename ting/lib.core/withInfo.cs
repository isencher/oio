using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.lib.core
{
    public class withInfo
    {
        /// <summary>
        /// to show info in some screen
        /// </summary>
        /// <param name="Info">info</param>
        /// <param name="label">screen</param>
        /// <param name="flag">blue when normal, and red when abnormal</param>
        public static void DisplayStatus(string Info = "", dynamic label = null, bool flag = true)
        {
            if (label != null)
            {
                if (flag) { label.ForeColor = Color.Blue; }
                else { label.ForeColor = Color.Red; }
                label.Text = Info;
            }
        }
    }
}
