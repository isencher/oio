using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.lib;
using ting.model;

namespace ting.pal
{
    public class withExcel
    {

        public List<T> ImportData<T>()
            where T : class, IbaseProperties, new()
        {
            List<T> results = new List<T>();

            string fName = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xls文件|*.xls";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fName = openFileDialog.FileName;
            }
            else
            { return null; }

            var excelfile = new LinqToExcel.ExcelQueryFactory(fName);
            var tsheet = excelfile.Worksheet<T>(0);
            //var tsheet = excelfile.Worksheet(0);


            var query = from t in tsheet select t;

            foreach (var q in query)
            {
                //if (Add(q) > 0)
                //{
                results.Add(q);
                //}
            }

            return results;
        }

        //public void ImportData<T>(IValidate validate = null)
        //    where T : class, new()
        //{
        //    string fName = null;
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "xls文件|*.xls";

        //    DialogResult result = openFileDialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        fName = openFileDialog.FileName;
        //    }
        //    else
        //    { return; }

        //    var excelfile = new LinqToExcel.ExcelQueryFactory(fName);
        //    var tsheet = excelfile.Worksheet<T>(0);
        //    //var tsheet = excelfile.Worksheet(0);

        //    if (validate != null)
        //    {
        //        validate.ValidateOperate<T>(tsheet);
        //    }

        //    var query = from t in tsheet select t;
        //    foreach (var q in query)
        //    {
        //        Repo.Add<T>(q);
        //    }

        //}

        public void ExportData(DataGridView dgv, dynamic statusInfo = null, string saveName = "table")
        {
            withInfo.DisplayStatus("正在打开Excel......", statusInfo);
            var excel = new Microsoft.Office.Interop.Excel.Application { Visible = false };
            withInfo.DisplayStatus("正在生成工作簿......", statusInfo);
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add();
            withInfo.DisplayStatus("正在生成工作表......", statusInfo);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = excel.ActiveSheet;

            try
            {
                withInfo.DisplayStatus("正在向工作表填充数据......", statusInfo);

                // 标题行
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgv.Columns[i].HeaderText;
                }
                // 数据行
                for (int row = 0; row < dgv.RowCount; row++)
                {
                    for (int col = 0; col < dgv.ColumnCount; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value =
                            dgv.Rows[row].Cells[col].Value == null ?
                            dgv.Rows[row].Cells[col].FormattedValue :
                            dgv.Rows[row].Cells[col].Value;
                    }
                }
                //workbook.SaveAs(Filename: "demo.xls",
                //                                 FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

                //Getting the location and file name of the excel to save from user.
                withInfo.DisplayStatus("正在打开保存对话框......", statusInfo);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = saveName;
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    withInfo.DisplayStatus("正在打开保存对话框......", statusInfo);
                    workbook.SaveAs(saveDialog.FileName);
                    withInfo.DisplayStatus("保存成功。", statusInfo);
                    MessageBox.Show("导出成功！");
                }
            }
            catch (Exception ex)
            {
                withInfo.DisplayStatus(ex.Message, statusInfo, false);
                throw;
            }
            finally
            {
                withInfo.DisplayStatus("正在关闭工作簿......", statusInfo);
                workbook.Close(false);
                withInfo.DisplayStatus("正在关闭Excel......", statusInfo);
                excel.Application.Quit();
                withInfo.DisplayStatus("", statusInfo);

                workbook = null;
                workbook = null;
                excel = null;
            }

        }
    }
}
