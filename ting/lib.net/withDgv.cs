using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ting.lib.net
{
    public class withDgv
    {
        /// <summary>
        /// to initialize datagridview object appearance
        /// </summary>
        /// <param name="gdv">datagridview object</param>
        public static void SetDataGridViewAppearance(DataGridView gdv)
        {
            // 初始化基本的DataGridView属性
            gdv.Dock = DockStyle.Fill;
            gdv.BackgroundColor = Color.White;
            gdv.BorderStyle = BorderStyle.Fixed3D;

            // 禁止通过数据源自动生成列
            gdv.AutoGenerateColumns = false;

            // 只读显示并禁止交互
            gdv.AllowUserToAddRows = false;
            gdv.AllowUserToDeleteRows = false;
            gdv.AllowUserToOrderColumns = false;
            gdv.AllowUserToResizeColumns = false; //禁止调整列宽
            gdv.AllowUserToResizeRows = false;  //禁止调整行高
            gdv.ReadOnly = true;  //只读
            gdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  //选中整行
            gdv.MultiSelect = false;  //不可多选
            gdv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            gdv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;  //列标头高度不允许调整
            gdv.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode.DisableResizing;  //行标头宽度不允许调整

            // 设置选定状态时的前景色与背景色
            gdv.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            gdv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 设置RowHeadersDefaultCellStyle.SelectionBackColor，
            // 以便其默认值不会覆盖DefaultCellStyle.SelectionBackColor值
            gdv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            // 设置所有行和交互行的背景色 
            // 交互行的值会覆盖所有行的值
            gdv.RowsDefaultCellStyle.BackColor = Color.White;
            gdv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(217, 225, 242);

            // 设置行列标头样式
            gdv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gdv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 114, 196);
            gdv.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            gdv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);

            // Set the Format property on the "Last Prepared" column to cause
            // the DateTime to be formatted as "Month, Year".
            //gdv.Columns["Last Prepared"].DefaultCellStyle.Format = "y";

            // Specify a larger font for the "Ratings" column. 
            /* 1.
            using (Font font = new Font(
                gdv.DefaultCellStyle.Font.FontFamily, 11, FontStyle.Regular))
            {
                for (int i = 0; i < gdv.ColumnCount; i++)
                {
                    gdv.Columns[i].DefaultCellStyle.Font = font;
                }
            }
            */
            // 设定列标头字体样式
            //using (Font font = new Font(gdv.DefaultCellStyle.Font.FontFamily, 11, FontStyle.Regular))
            //{
            //    gdv.ColumnHeadersDefaultCellStyle.Font = font;
            //}

            // Specify a set of Styles for every column header
            /* 1.
            using (Font font=new Font("huawenzhongsong",11,FontStyle.Bold))
            {
                for (int i = 0; i < gdv.ColumnCount; i++)
                {
                    gdv.Columns[i].HeaderCell.Style.Font = font;
                    gdv.Columns[i].HeaderCell.Style.BackColor = Color.Yellow;
                }
            }
            */
            // 设定列标头样式
            using (Font font = new Font("weiruanyahei", 11, FontStyle.Bold))
            {
                gdv.ColumnHeadersDefaultCellStyle.Font = font;
                // when EnableHeadersVisualStyles = false,then below 有效
                gdv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                gdv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 114, 196);
                gdv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gdv.ColumnHeadersHeight = 30;
            }
            gdv.EnableHeadersVisualStyles = false;

            // Attach a handler to the CellFormatting event.
            //gdv.CellFormatting += new
            //    DataGridViewCellFormattingEventHandler(gdv_CellFormatting);
        }

        /// <summary>
        /// to get property value of specific object by property string name
        /// </summary>
        public static string GetValueByPropertyName(object property, string propertyName)
        {
            string retValue;

            retValue = "";

            if (property == null) return null;

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;

                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        var propertyValue = propertyInfo.GetValue(property, null);
                        retValue = GetValueByPropertyName(propertyValue, propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }

            return retValue;
        }

    }
}
