using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ting.lib
{
    /// <summary>
    /// 控件块，可以是Button、TextBox、Panel等任意可视化窗体控件
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 控件宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 控件高度
        /// </summary>
        public int Height { get; set; }
    }

    public partial class withRank
    {


        /// <summary>
        /// 当多个控件块井字排列时，求第order块的位置
        /// </summary>
        /// <param name="FirstBlockLocation">第一个控件块的位置（即0行0列）</param>
        /// <param name="block">控件块的尺寸</param>
        /// <param name="linespacing">行间距</param>
        /// <param name="columnspacing">列间距</param>
        /// <param name="columnnumber">每行块数</param>
        /// <param name="order">第order块（从0块开始计）</param>
        /// <returns></returns>
        public static Point GetBlockLocation(int order, Point FirstBlockLocation, Block block, int linespacing = 0, int columnspacing = 0, int columnnumber = 4)
        {
            //求第order块所行列
            int row = order / columnnumber;
            int column = order % columnnumber;

            //求第order块位置
            int X = FirstBlockLocation.X + column * (block.Width + columnspacing);
            int Y = FirstBlockLocation.Y + row * (block.Height + linespacing);
            return new Point(X, Y);
        }

        /// <summary>
        /// 在面板上生成菜单按钮组
        /// </summary>
        public static List<Button> generatebuttons(Control panel, List<string> menus, int[] tags, string style)
        {
            var btns = new List<Button>();
            if (menus == null) { return null; }

            //withRank rank = new withRank();
            Block block = new Block();
            int startx = 0, starty = 0, linespacing = 0, columnspacing = 0, columnnumber = 0;

            switch (style)
            {
                case "topmenu":
                    block = new Block { Width = 80, Height = 32 };
                    startx = 3; starty = 3;
                    linespacing = 0; columnspacing = 0;
                    columnnumber = menus.Count() + 1;
                    break;
                case "leftmenu":
                    block = new Block { Width = 100, Height = 32 };
                    startx = 3; starty = 3;
                    linespacing = 0; columnspacing = 0;
                    columnnumber = 1;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < menus.Count(); i++)
            {
                var btn = new Button();
                btn.Width = block.Width; btn.Height = block.Height;
                btn.Location = withRank.GetBlockLocation(i, new Point(startx, starty),
                    block, linespacing, columnspacing, columnnumber);
                btn.Text = menus[i];
                btn.Tag = tags[i];
                btns.Add(btn);
            }
            return btns;
        }

    }
}
