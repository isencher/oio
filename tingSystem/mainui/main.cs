using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ting.lib;
using ting.log;
using ting.model;
using WeifenLuo.WinFormsUI.Docking;

namespace ting.pal
{
    public class mainui : Form
    {
        #region ui lay controls
        private Panel topPanel = new Panel();
        private DockPanel bottomPanel = new DockPanel();
        private DockContent levelOnePanel;
        private DockContent levelTwoPanel;
        private List<DockContent> works = new List<DockContent>();
        private StatusStrip statusBar;
        private ToolStripLabel statusInfoLabel;
        private List<Button> topBtns = new List<Button>();
        private List<Button> oneBtns = new List<Button>();
        #endregion
        #region data object
        private LoadByRight loadByRight = new LoadByRight();
        private User user;
        private SetofBook set;
        /// <summary>
        /// 是否注销 true时，可重新打开登录窗
        /// </summary>
        public bool IsCancel { get; set; } = false;
        public Logger logger { get; set; } = new sqlLogger();
        #endregion
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="set"></param>
        public mainui(User user, SetofBook set)
        {
            this.user = user;
            this.set = set;
            laybottom();
            laytop();
            laystatusbar();
            init();
            this.Load += (sender, e) =>
            {
                #region write log
                var loginstance = new Log()
                {
                    UserAcount = user.Account,
                    Date = DateTime.Now,
                    Action = "Login",
                    Entity = null,
                    Content = null,
                    Original = null
                };
                logger.Write(loginstance);
                #endregion
            };
            this.Disposed += (sender, e) =>
            {
                #region write log
                var loginstance = new Log()
                {
                    UserAcount = user.Account,
                    Date = DateTime.Now,
                    Action = "Loginout",
                    Entity = null,
                    Content = null,
                    Original = null
                };
                logger.Write(loginstance);
                #endregion
            };
        }
        /// <summary>
        /// initialize main
        /// </summary>
        private void init()
        {
            this.Text = "ting私人定制系统";
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
            withInfo.DisplayStatus($"    用户：{user.Name}      账套: {set.Name}", statusInfoLabel);
        }
        /// <summary>
        /// lay status
        /// </summary>
        private void laystatusbar()
        {
            statusBar = new StatusStrip();
            statusInfoLabel = new ToolStripLabel();
            statusBar.Items.AddRange(new ToolStripItem[] { statusInfoLabel });
            statusBar.Location = new Point(0, 163);
            statusBar.Size = new Size(100, 22);
            statusBar.BackColor = Color.Azure;

            statusInfoLabel.Size = new Size(0, 1);

            Controls.Add(statusBar);
        }
        /// <summary>
        /// lay top panel
        /// </summary>
        private void laytop()
        {
            topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Size = new Size(800, 38);
            topPanel.TabIndex = 0;
            //topMenu.BackColor = Color.Gray;
            Controls.Add(topPanel);

            fillButtonsInTopPanel(topPanel);
        }
        /// <summary>
        /// lay bottom panel
        /// </summary>
        private void laybottom()
        {
            bottomPanel = new DockPanel();
            bottomPanel.BackColor = SystemColors.AppWorkspace;
            bottomPanel.Dock = DockStyle.Fill;
            bottomPanel.Location = new Point(0, 61);
            bottomPanel.Size = new Size(800, 164);
            bottomPanel.TabIndex = 1;
            //bottomContainer.BackColor = Color.Red;

            Controls.Add(bottomPanel);
        }
        /// <summary>
        /// fill buttons on top panel
        /// </summary>
        /// <param name="panel"></param>
        private void fillButtonsInTopPanel(Panel panel)
        {
            var mods = loadByRight.GetModules(user, set);
            mods.Add(new Module() { Id = 0, Name = "注销" });
            mods.Add(new Module() { Id = -1, Name = "退出" });
            if(mods == null) { topPanel.Visible = false; return; }
            //在上部面板装载模块按钮组
            topBtns = withRank.generatebuttons(topPanel,
                mods.Select(m => m.Name).ToList(), mods.Select(m => m.Id).ToArray(), "topmenu");
            foreach (var btn in topBtns)
            {
                btn.Click += (sender, e) =>
                {
                    var mod = mods.FirstOrDefault(m => m.Id == Convert.ToInt32(btn.Tag));
                    if (mod.Id == 0)
                    {
                        this.IsCancel = true;
                        this.Close();
                        return;
                    }
                    if(mod.Id == -1)
                    {
                        this.IsCancel = false;
                        this.Close();
                        return;
                    }
                    var ones = loadByRight.GetLevelOnes(user, set, mod);
                    levelOnePanel = withType.IsExist(levelOnePanel, mod.Id);
                    //if (levelOnePanel == null) { levelOnePanel = new DockContent(); levelOnePanel.Tag = mod.Id; }
                    //else if (Convert.ToInt32(levelOnePanel.Tag) != mod.Id) { levelOnePanel.Dispose(); levelOnePanel = new DockContent(); levelOnePanel.Tag = mod.Id; }
                    //else if (levelOnePanel.IsDisposed == true) { levelOnePanel = new DockContent(); levelOnePanel.Tag = mod.Id; }
                    levelOnePanel.Text = btn.Text;
                    levelOnePanel.SizeChanged += (onesender, onee) =>
                    {
                        Form frm = (Form)onesender;
                        foreach (Control item in frm.Controls)
                        {
                            if (item.GetType() == typeof(Button))
                            {
                                item.Width = frm.Width - 6;
                            }
                        }
                    };
                    fillButtonsInLevelOnePanel(levelOnePanel, mod);
                    levelOnePanel.Show(bottomPanel, DockState.DockLeft);
                };
                topPanel.Controls.Add(btn);
            }
        }
        /// <summary>
        /// fill buttons on onelevel panel
        /// </summary>
        /// <param name="onepanel"></param>
        /// <param name="module"></param>
        private void fillButtonsInLevelOnePanel(DockContent onepanel, Module module)
        {
            var ones = loadByRight.GetLevelOnes(user, set, module);
            oneBtns = withRank.generatebuttons(onepanel,
                ones.Select(o => o.Name).ToList(), ones.Select(o => o.Id).ToArray(), "leftmenu");
            foreach (var btn in oneBtns)
            {
                btn.Click += (sender, e) =>
                {
                    var one = ones.FirstOrDefault(o => o.Id == Convert.ToInt32(btn.Tag));
                    //DockContent twopanel = null;
                    //DockContent existed = null;
                    //if (levelTwoPanels != null && levelTwoPanels.Count > 0)
                    //{
                    //existed = levelTwoPanels.FirstOrDefault(p => Convert.ToInt32(p.Tag) == one.Id);
                    levelTwoPanel = withType.IsExist(levelTwoPanel, one.Id);
                    //if (existed == null) { twopanel = new DockContent(); twopanel.Tag = one.Id;  }
                    //else if(Convert.ToInt32(existed.Tag) == one.Id && existed.IsDisposed == false) { existed.Show(); return; }
                    //else if(existed.IsDisposed == true) { levelTwoPanels.Remove(existed); twopanel = new DockContent(); twopanel.Tag = one.Id; }
                    //else { }
                    //}
                    //else { twopanel = new DockContent();twopanel.Tag = one.Id; }

                    levelTwoPanel.Text = btn.Text;
                    levelTwoPanel.SizeChanged += (twosender, twoe) =>
                    {
                        Form frm = (Form)twosender;
                        foreach (Control item in frm.Controls)
                        {
                            if (item.GetType() == typeof(Button))
                            {
                                item.Width = frm.Width - 6;
                            }
                        }

                    };
                    fillButtonsInTwoPanel(levelTwoPanel, one, module);
                    //if(existed == null) levelTwoPanels.Add(twopanel);
                    levelTwoPanel.Show(bottomPanel, DockState.DockLeft);
                };
                onepanel.Controls.Add(btn);
            }

        }
        /// <summary>
        /// fill buttons on twolevel panel
        /// </summary>
        /// <param name="twopanel"></param>
        /// <param name="one"></param>
        private void fillButtonsInTwoPanel(DockContent twopanel, LevelOne one, Module module)
        {
            var twos = loadByRight.GetLevelTwos(user, set, one);
            var twoBtns = withRank.generatebuttons(twopanel,
                twos.Select(t => t.Name).ToList(), twos.Select(t => t.Id).ToArray(), "leftmenu");
            foreach (var btn in twoBtns)
            {
                var two = twos.FirstOrDefault(t => t.Id == Convert.ToInt32(btn.Tag));
                //DockContent wo;
                btn.Click += (sender, e) =>
                {
                    if (works != null && works.Count > 0)
                    {
                        for (int i = 0; i < works.Count; i++)
                        {
                            if (works[i].IsDisposed) works.Remove(works[i]);
                        }
                        var existed = works.FirstOrDefault(w => Convert.ToInt32(w.Tag) == two.Id);
                        if (existed != null)
                        {
                            if (existed.IsDisposed == false) { existed.Show(); return; }
                            //else { works.Remove(existed); }
                        }
                    }

                    if(two.ClassName==null || two.ClassName == "")
                    {
                        MessageBox.Show($"未在LevelTwo中，为【{two.Name}】配置ClassName的值。");
                        return;
                    }
                    //MessageBox.Show($"use [{two.ClassName}] class to create work.");
                    var work = withType.InstanceOfType(module.AssemblyName, two.ClassName);
                    if (work == null)
                    {
                        MessageBox.Show($"类【{two.ClassName}】不存在！");
                        return;
                    }
                    if (work != null)
                    {
                        work.CurrentUser = user;
                        work.WorkText = two.Name;
                        work.Tag = two.Id;
                        work.ColumnConfigXML = string.Concat(module.AssemblyName, ".xml");
                        work.ButtonTexts = loadByRight.GetActionButtonText(user, set, two).OrderBy(r=>r.Id)
                                                                .ToDictionary(x => x.Name, x => x.MethodName);
                        works.Add(work);
                        work.Show(bottomPanel, DockState.Document);

                    }
                };
                twopanel.Controls.Add(btn);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainui));
            this.SuspendLayout();
            // 
            // mainui
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainui";
            this.ResumeLayout(false);

        }
    }
}
