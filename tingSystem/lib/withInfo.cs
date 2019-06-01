using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.lib
{
    public class withInfo
    {
        /// <summary>
        /// 在Label控件上显示状态信息
        /// </summary>
        /// <param name="Info">文字信息</param>
        /// <param name="label"></param>
        /// <param name="flag">蓝色或红色显示</param>
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
