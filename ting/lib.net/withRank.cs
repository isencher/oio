using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ting.lib.net
{
    /// <summary>
    /// block, is Button、TextBox、Panel etc.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// block width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// block height
        /// </summary>
        public int Height { get; set; }
    }

    public partial class withRank
    {


        /// <summary>
        /// to get the point of order block when arrangement on plane
        /// </summary>
        /// <param name="FirstBlockLocation">first block location (0,0）</param>
        /// <param name="block">block object</param>
        /// <param name="linespacing">line interval</param>
        /// <param name="columnspacing">column interval</param>
        /// <param name="columnnumber">the number of block in a line</param>
        /// <param name="order">sequence numbering, start from 0</param>
        /// <returns>point</returns>
        public static Point GetBlockLocation(int order, Point FirstBlockLocation, Block block, 
            int linespacing = 0, int columnspacing = 0, int columnnumber = 4)
        {
            //solve for row and column of order-th block
            int row = order / columnnumber;
            int column = order % columnnumber;

            //solve for point of order-th block
            int X = FirstBlockLocation.X + column * (block.Width + columnspacing);
            int Y = FirstBlockLocation.Y + row * (block.Height + linespacing);
            return new Point(X, Y);
        }

        /// <summary>
        /// to generate button menu on plane
        /// </summary>
        /// <param name="panel">plane</param>
        /// <param name="menus">menus name</param>
        /// <param name="tags">identity tag</param>
        /// <param name="style">top or left menu plane</param>
        /// <returns>buttons</returns>
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
