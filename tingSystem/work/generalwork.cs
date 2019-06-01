using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ting.bll;
using ting.lib;
using ting.log;
using ting.model;

namespace ting.pal
{
    public class generalwork<dbContext, T> : work<dbContext, T>
    where dbContext : DbContext, new()
    where T : class, IbaseProperties, new()
    {
        #region ui lay controls
        //private List<Button> topBtns;
        private Panel toppanel;
        private Panel bottompanel;
        private StatusStrip statusstrip;
        private ToolStripStatusLabel statusinfolabel;
        private DataGridView dgv;
        #endregion
        #region ui pop controls
        public EditDialog<T> editDialog;
        public QueryDialog<T> queryDialog;
        #endregion
        #region ui background control
        private BindingSource dgvsource;
        #endregion
        /// <summary>
        /// Operation Button Text dictionary
        /// Button's Text-->Operation method name
        /// </summary>
        public Dictionary<string, string> ButtonTexts { get; set; }
        /// <summary>
        /// work text/tag
        /// </summary>
        public string WorkText { get; set; }
        public string ColumnConfigXML { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        public generalwork()
        {
            tingmodelinstance = new tingmodel<dbContext, T>();
            #region move to constructor from Load event
            initwork();
            laybottom();
            laytop();
            laystatus();
            #endregion

            this.Load += (sender, e) =>
            {
                logger = new sqlLogger();
                this.Text = WorkText;
                LoadActionButton(toppanel,ButtonTexts);

                initdgvlist();
                initdgvsource();
                UpdateList();
            };
        }

        /// <summary>
        /// initializecontrols
        /// </summary>
        private void initwork()
        {
            toppanel = new Panel();
            bottompanel = new Panel();
            statusstrip = new StatusStrip();
            dgv = new DataGridView();
            dgvsource = new BindingSource();
            statusinfolabel = new ToolStripStatusLabel();
        }
        /// <summary>
        /// lay  bottom panel
        /// </summary>
        private void laybottom()
        {
            bottompanel.BackColor = SystemColors.AppWorkspace;
            bottompanel.Dock = DockStyle.Fill;
            bottompanel.Location = new Point(0, 61);
            bottompanel.Size = new Size(800, 164);
            bottompanel.BackColor = Color.Gray;
            bottompanel.TabIndex = 1;

            this.Controls.Add(bottompanel);
        }
        /// <summary>
        /// lay top panel
        /// </summary>
        private void laytop()
        {
            toppanel.Dock = DockStyle.Top;
            toppanel.Location = new Point(0, 0);
            toppanel.Size = new Size(800, 38);
            toppanel.TabIndex = 0;
            //topMenu.BackColor = Color.Gray;

            this.Controls.Add(toppanel);
        }
        /// <summary>
        /// add operation button on top panel
        /// </summary>
        private void LoadActionButton(Panel panel, Dictionary<string,string> texts)
        {
            List<Button> btns = new List<Button>();
            List<string> names = null;
            if (texts.Count == 0)
            {
                panel.Visible = false;
            }
            else
            {
                names = texts.Keys.ToList();
                var tags = new int[names.Count];
                btns = withRank.generatebuttons(panel, names, tags, "topmenu");
                if (btns != null)
                {
                    foreach (var item in btns)
                    {
                        loadclick(item, texts);
                        toppanel.Controls.Add(item);
                    }

                }
            }
        }
        /// <summary>
        /// add click code to a button
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="texts"></param>
        public void loadclick(Button btn, Dictionary<string,string> texts)
        {
            var clickActions = new ClickActions<dbContext, T>();

            btn.Click += (sender, e) =>
            {
                if (texts != null)
                {
                    typeof(ClickActions<dbContext, T>).GetMethod(texts[btn.Text]).Invoke(obj: clickActions, parameters: new object[] { this });
                }
                else
                {
                    MessageBox.Show("错误或正在开发中...");
                }
            };

        }
        /// <summary>
        /// lay status
        /// </summary>
        private void laystatus()
        {
            statusstrip.Items.AddRange(new ToolStripItem[] { statusinfolabel });
            statusstrip.Location = new Point(0, 163);
            statusstrip.Size = new Size(100, 22);
            statusstrip.BackColor = Color.Azure;

            statusinfolabel.Size = new Size(0, 1);

            this.Controls.Add(statusstrip);
        }
        /// <summary>
        /// initialize datagridview
        /// </summary>
        private void initdgvlist()
        {
            dgv.Dock = DockStyle.Fill;
            withDgv.SetDataGridViewAppearance(dgv);
            dgv.DataSource = dgvsource;
            dgv.CellClick += (sender, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    withInfo.DisplayStatus($"第{dgv.CurrentRow.Index + 1}行 共{dgv.Rows.Count}行", statusinfolabel);
                }
            };
            loadcolumns(dgv);
            bottompanel.Controls.Add(dgv);
        }
        /// <summary>
        /// load columns for datagridview
        /// </summary>
        private void loadcolumns(DataGridView dgv)
        {
            var filtered = GetGridSet(ColumnConfigXML);
            foreach (var column in filtered)
            {
                DataGridViewTextBoxColumn dgvColumn = new DataGridViewTextBoxColumn();
                dgvColumn.HeaderText = column.HeaderText;
                dgvColumn.Width = column.Width;
                dgvColumn.Name = column.Name;
                dgvColumn.DataPropertyName = column.DataPropertyName;
                dgvColumn.DefaultCellStyle.Format = column.Format;
                switch (column.Alignment)
                {
                    case "MiddleRight":
                        dgvColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "MiddleCenter":
                        dgvColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    default:
                        dgvColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }
                dgv.Columns.Add(dgvColumn);
            }
        }
        /// <summary>
        /// get column info of datagridview  from config file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private IEnumerable<ColumnInfo> GetGridSet(string file)
        {
            XDocument doc = XDocument.Load(file);
            string TName = typeof(T).Name;
            string TFullName = typeof(T).FullName;
            var filtered = from c in doc.Descendants("ColumnInfo")
                           where (string)c.Attribute("Table") == TName ||
                                       (string)c.Attribute("Table") == TFullName
                           select new ColumnInfo()
                           {
                               Name = (string)c.Attribute("Name"),
                               DataPropertyName = (string)c.Attribute("DataPropertyName"),
                               HeaderText = (string)c.Attribute("HeaderText"),
                               Width = (int)c.Attribute("Width"),
                               Format = (string)c.Attribute("Format"),
                               Alignment = (string)c.Attribute("Alignment")
                           };

            return filtered;
        }
        /// <summary>
        /// initialize BindingSource
        /// </summary>
        private void initdgvsource()
        {
            dgvsource.RaiseListChangedEvents = true;
        }
        #region Common Action
        /// <summary>
        /// Set top panel visibility
        /// </summary>
        /// <param name="visible"></param>
        public void TopPanelVisibility(bool visible)
        {
            toppanel.Visible = visible;
        }
        /// <summary>
        /// current editting entity
        /// </summary>
        public override T Current => (T)dgvsource.Current;
        /// <summary>
        /// open add dialog
        /// </summary>
        /// <param name="entity"></param>
        public override void OpenAdd(T entity)
        {
            if (editDialog != null)
            {
                editDialog.Text = "新增";
                editDialog.EditStatus = EditStatus.Add;
                editDialog.EditEntity = new T();
                editDialog.Original = null;
                editDialog.Save = null;
                editDialog.Save += Add;
                editDialog.IsExist = null;
                editDialog.IsExist = IsExist;
                editDialog.UpdateList = null;
                editDialog.UpdateList += UpdateList;
                editDialog.ShowDialog();
            }
            else { MessageBox.Show("EditDialog没有实例化"); }
        }
        /// <summary>
        /// open alter dialog
        /// </summary>
        /// <param name="entity"></param>
        public override void OpenAlter(T entity)
        {
            if (entity == null) { return; }
            if (editDialog != null)
            {
                editDialog.Text = "修改";
                editDialog.EditStatus = EditStatus.Alter;
                editDialog.EditEntity = Current;
                editDialog.Original = Current == null ? null : Alone(Current);
                editDialog.Save = null;
                editDialog.Save += Alter;
                editDialog.IsExist = null;
                editDialog.IsExist = IsExist;
                editDialog.UpdateList = null;
                editDialog.UpdateList += UpdateList;
                editDialog.ShowDialog();
            }
            else { MessageBox.Show("EditDialog没有实例化"); }
        }
        /// <summary>
        /// perform to delete a selected entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult ToDelete(T entity)
        {
            if (entity == null) { return ActionResult.None; }

            bool cannot = false;
            if (entity.Relation == Dependencies.BeDependentOn || entity.Relation == Dependencies.DependentAndBeDependentOn)
            {
                if (ValidateBeforeDelete != null) { cannot = ValidateBeforeDelete(entity); }
                else { MessageBox.Show("请添加依赖项验证器！"); }
            }
            else
            { cannot = true; }
            if (cannot)
            {
                var question = MessageBox.Show(null, $"确定要删除Id号为【{entity.Id}】的记录吗？", "确认",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (question == DialogResult.Yes)
                {
                    if (MessageBox.Show(null, "删除之后无法找回，依赖它的数据也将被删除！\n请确认是否仍要删除？", "再确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        var original = entity.DisplayValue;
                        var result = tingmodelinstance.Delete(entity);
                        if (result == ActionResult.Success)
                        {
                            logger.Write(CreateLog("Delete", null, original));
                            UpdateList();
                        }
                        return result;
                    }
                }

            }
            else { MessageBox.Show("被依赖数据不能被删除！"); }

            return ActionResult.None;
        }
        /// <summary>
        /// perform to delete all entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public override ActionResult ToDeletes(List<T> entities)
        {
            //if (entities.Count == 0) { return ActionResult.None; }
            //var question = MessageBox.Show(null, $"该功能仅面向数据管理员开放！", "警告",
            //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //if (question == DialogResult.OK)
            //{
            //    if (MessageBox.Show(null, "删除之后无法找回，依赖它的数据也将被删除！\n请确认是否仍要删除？", "再确认", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            //    {
            //        var result = tingmodelinstance.Deletes(entities);
            //        UpdateList();
            //        return result;
            //    }
            //}
            return ActionResult.None;
        }
        /// <summary>
        /// perform to import  records from a .xls file
        /// </summary>
        public override void ToImport()
        {
            List<T> Importing = null;
            int originalcount = 0;
            int importedcount = 0;
            Importing = new withExcel().ImportData<T>();
            originalcount = Importing == null ? 0 : Importing.Count;
            try
            {
                if (Importing != null)
                {
                    foreach (var item in Importing)
                    {
                        if (!IsExist(item, true))
                        {
                            if (tingmodelinstance.Add(item) == ActionResult.Success)
                            {
                                importedcount++;
                                WriteLog(CreateLog("Import", item.DisplayValue));
                            };
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            if (originalcount != 0)
            {
                MessageBox.Show($"申请导入【{originalcount}】条, \n实际导入【{importedcount}】条。");
                UpdateList();
            }
        }
        /// <summary>
        /// perform to export datagridview records into a .xlsx file
        /// </summary>
        public override void ToExport()
        {
            if (dgv.Rows.Count > 0)
            {
                var withexcel = new withExcel();
                withexcel.ExportData(dgv, statusinfolabel, typeof(T).Name);
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string value = "";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        value += $"{dgv.Columns[cell.ColumnIndex].HeaderText}: {cell.Value}; ";
                    }
                    WriteLog(CreateLog("Export", value));
                }
            }
            else
            {
                withInfo.DisplayStatus("无记录！", statusinfolabel, false);
            }
        }
        /// <summary>
        /// open filter  dialog
        /// </summary>
        public override void ToQuery()
        {
            if (queryDialog != null)
            {
                queryDialog.DataSource = DataSource;
                if (queryDialog.UpdateByFilter == null)
                {
                    queryDialog.UpdateByFilter += UpdateByFilter;
                }
                queryDialog.ShowDialog();
            }

        }
        /// <summary>
        /// update datagridview list by all  records in db  store
        /// </summary>
        public override void UpdateList()
        {
            dgvsource.DataSource = this.DataSource;
        }
        /// <summary>
        /// update  datagridview list by filter
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateByFilter(List<T> entities)
        {
            dgvsource.DataSource = entities;
        }
        #endregion
    }

}
